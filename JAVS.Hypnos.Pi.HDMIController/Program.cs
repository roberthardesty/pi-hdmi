using JAVS.Hypnos.Pi.Core;
using JAVS.Hypnos.Pi.HDMIController.Services;
using JAVS.Hypnos.Shared.ServiceModels;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace JAVS.Hypnos.Pi.HDMIController
{
    class Program
    {
        private static PiSettings _piSettings;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, a) =>
            {
                a.Cancel = true;
                cts.Cancel();
            };

            string configurationFilePath;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                configurationFilePath = @"/share/JAVS.Hypnos.Pi.HDMIController/PiSettings.json";
            else
                configurationFilePath = @"./PiSettings.json";

            _piSettings = await JSONFile.LoadAsync<PiSettings>(configurationFilePath);

            FaceDetectionService faceDetectionService = new FaceDetectionService(_piSettings);

            await faceDetectionService.Init();

            faceDetectionService.ListenFor<FaceDetectionStats>((stats) =>
            {
                Console.WriteLine("Face Change.");
                string output = "";
                if (stats.IsZeroFaceAlert)
                {
                    output = @"vcgencmd display_power 0".Bash();
                }
                else if (stats.FaceRectangles?.Count > 0)
                {
                    output = @"xset s reset";
                    output += @"vcgencmd display_power 1".Bash();
                }

                Console.WriteLine(output);

                //if (stats.IsZeroFaceAlert)
                //    output = @"echo 'standby 0' | cec-client -s -d 1".Bash();
                //else if(stats.FaceRectangles?.Count > 0)
                //    output = @"echo 'standby 1' | cec-client -s -d 1".Bash();
            });

            await faceDetectionService.Join(new JoinGroupRequest()
            {
                IsHDMIController = true,
                Password = "Not in use"
            });

            while (!cts.IsCancellationRequested)
            {
            }
        }
    }
}
