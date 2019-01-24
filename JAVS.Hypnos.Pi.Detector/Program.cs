using JAVS.Hypnos.Pi.Core;
using JAVS.Hypnos.Pi.Detector.Services;
using JAVS.Hypnos.Shared.ServiceModels;
using OpenCvSharp;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace JAVS.Hypnos.Pi.Detector
{
    class Program
    {
        private static ClientGroupStats _clientGroupStats;
        private static PiSettings _piSettings;
        private static FaceDetectionConfiguration _faceDetectionConfiguration;

        static async Task Main(string[] args)
        {
            _faceDetectionConfiguration = new FaceDetectionConfiguration()
            {
                 ScaleFactor = 1.2,
                MinimumNeighbors = 5,
                MinimumFaceWidth = 20,
                MinimumFaceHeight = 20,
                FaceTimeoutInSeconds = 5
            };
            Console.WriteLine("Hello World!");
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, a) =>
            {
                a.Cancel = true;
                cts.Cancel();
            };

            string configurationFilePath;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                configurationFilePath = @"/share/JAVS.Hypnos.Pi.Detector/PiSettings.json";
            else
                configurationFilePath = @"./PiSettings.json";

            _piSettings = await JSONFile.LoadAsync<PiSettings>(configurationFilePath);

            FaceDetectionService faceDetectionService = new FaceDetectionService(_piSettings);

            await faceDetectionService.Init();

            faceDetectionService.ListenFor<ClientGroupStats>((stats) =>
            {
                foreach(var group in stats.ConnectedClientGroupCounts)
                {
                    Console.WriteLine($"{group.Key}: {group.Value} Clients Connected.");
                    _clientGroupStats = stats;
                }
            });

            faceDetectionService.ListenFor<FaceDetectionConfiguration>((config) =>
            {
                _faceDetectionConfiguration = config;
            });

            await faceDetectionService.Join(new JoinGroupRequest()
            {
                IsDetector = true,
                Password = "Not in use"
            });

            RunFacialDetection(cts, faceDetectionService);
        }

        public static void RunFacialDetection(CancellationTokenSource cts, FaceDetectionService service)
        {
            string haarCascadeFile = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? _piSettings.LinuxPathToFaceHaarCascade
                                                                            : _piSettings.DevPathToFaceHaarCascade;
            Console.WriteLine("Path to cascade classifier: " + haarCascadeFile);
            if (!File.Exists(haarCascadeFile))
            {
                Console.WriteLine("NO HAAR FILE FOUND");
                return;
            }

            Mat sourceImg = new Mat();
            DateTime lastFaceTime = DateTime.Now;
            bool wasSearchingForFace = true;

            VideoCapture captureInstance = new VideoCapture(0);
            while (!captureInstance.IsOpened())
            {
                Console.WriteLine("Video Capture being reopened.");
                captureInstance.Open(0);
                Thread.Sleep(500);
            }
            using (CascadeClassifier cascade = new CascadeClassifier(haarCascadeFile))
                //using (Window webCamWindow = new Window("webCamWindow"))
            {
                while (!cts.IsCancellationRequested)
                {
                    captureInstance.Read(sourceImg);
                    if (sourceImg.Empty())
                        break;

                    var grayImage = new Mat();
                    Cv2.CvtColor(sourceImg, grayImage, ColorConversionCodes.BGRA2GRAY);
                    Cv2.EqualizeHist(grayImage, grayImage);

                    var faces = cascade.DetectMultiScale(
                        image: grayImage,
                        scaleFactor: _faceDetectionConfiguration.ScaleFactor,
                        minNeighbors: _faceDetectionConfiguration.MinimumNeighbors,
                        flags: HaarDetectionType.DoRoughSearch | HaarDetectionType.ScaleImage,
                        minSize: new Size(_faceDetectionConfiguration.MinimumFaceWidth, _faceDetectionConfiguration.MinimumFaceHeight)
                        );

                    if (faces.Length > 0)
                    {
                        lastFaceTime = DateTime.Now;
                        if (wasSearchingForFace)
                        {
                            service.PublishFaceDetectionStats(new FaceDetectionStats
                            {
                                FaceRectangles = faces.Select(face => new Shared.FaceRect
                                {
                                    X = face.X,
                                    Y = face.Y,
                                    Width = face.Width,
                                    Height = face.Height
                                }).ToList()
                            });
                            wasSearchingForFace = false;
                        }
                    }
                    else if (DateTime.Now - lastFaceTime >= TimeSpan.FromSeconds(_faceDetectionConfiguration.FaceTimeoutInSeconds))
                    {
                        if (!wasSearchingForFace)
                            service.PublishFaceDetectionStats(new FaceDetectionStats
                            {
                                IsZeroFaceAlert = true
                            });
                        wasSearchingForFace = true;
                    }
                }
            }
        }        
    }
}
