using JAVS.Hypnos.Pi.HDMIController.Services;
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

            TestService service = new TestService();
            await service.Init();
            service.Listen((words) =>
            {
                Console.WriteLine("Other: " + words);
            });
            await service.Talk("Hi I'm your friendly neighborhood hdmi controller.");
            while (!cts.IsCancellationRequested)
            {
            }
        }
    }
}
