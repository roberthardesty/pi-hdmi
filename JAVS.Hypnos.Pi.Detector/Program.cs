using JAVS.Hypnos.Pi.Detector.Services;
using JAVS.Hypnos.Shared.ServiceModels;
using OpenCvSharp;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JAVS.Hypnos.Pi.Detector
{
    class Program
    {
        private static ClientGroupStats _clientGroupStats;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, a) =>
            {
                a.Cancel = true;
                cts.Cancel();
            };

            FaceDetectionService faceDetectionService = new FaceDetectionService();

            await faceDetectionService.Init();

            faceDetectionService.ListenFor<ClientGroupStats>((stats) =>
            {
                foreach(var group in stats.ConnectedClientGroupCounts)
                {
                    Console.WriteLine($"{group.Key}: {group.Value} Clients Connected.");
                    _clientGroupStats = stats;
                }
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
            if (!File.Exists(@"/share/JAVS.Hypnos.Pi.Detector/Data/haarcascade_frontalface_alt.xml"))
                return;

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
            using (CascadeClassifier cascade = new CascadeClassifier(@"/share/JAVS.Hypnos.Pi.Detector/Data/haarcascade_frontalface_alt.xml"))
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
                        scaleFactor: 1.2,
                        minNeighbors: 7,
                        flags: HaarDetectionType.DoRoughSearch | HaarDetectionType.ScaleImage,
                        minSize: new Size(20, 20)
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
                    else if (DateTime.Now - lastFaceTime >= TimeSpan.FromSeconds(5))
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
