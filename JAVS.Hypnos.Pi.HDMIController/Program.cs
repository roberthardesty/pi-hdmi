using JAVS.Hypnos.Pi.HDMIController.Services;
using JAVS.Hypnos.Shared.ServiceModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JAVS.Hypnos.Pi.HDMIController
{
    class Program
    {
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

            faceDetectionService.ListenFor<FaceDetectionStats>((stats) =>
            {
                Console.WriteLine("Face Change.");
                string output = "";
                if (stats.IsZeroFaceAlert)
                    output = @"vcgencmd display_power 0".Bash();
                else if (stats.FaceRectangles?.Count > 0)
                    output = @"vcgencmd display_power 1".Bash();

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
