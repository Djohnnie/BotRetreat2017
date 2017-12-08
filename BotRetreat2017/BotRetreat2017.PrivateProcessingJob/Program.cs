using System;
using System.Threading.Tasks;
using BotRetreat2017.Processing;
using BotRetreat2017.Utilities;

namespace BotRetreat2017.PrivateProcessingJob
{
    class Program
    {
        private const Int32 DELAY_MS = 5000;

        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                Processor p = new Processor();

                while (true)
                {
                    var start = DateTime.UtcNow;

                    try
                    {
                        using (var sw = new SimpleStopwatch())
                        {
                            await p.GoPrivate();
                            Console.WriteLine($"[ BR2017 - {sw.ElapsedMilliseconds}ms! ]");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ BR2017 - EXCEPTION - '{ex.Message}'! ]");
                    }

                    var timeTaken = DateTime.UtcNow - start;
                    var delay = (Int32)(timeTaken.TotalMilliseconds < DELAY_MS ? DELAY_MS - timeTaken.TotalMilliseconds : 0);
                    await Task.Delay(delay);
                }
            }).Wait();
        }
    }
}