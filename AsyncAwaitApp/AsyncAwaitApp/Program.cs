using System.Text;

namespace AsyncAwaitApp
{
    class AsyncApp
    {
        public static async Task Main()
        {
            // Ukol 1)
            // Console.WriteLine("Začátek:");
            // var status = await DownloadDataAsync(3000);
            // Console.WriteLine($"{status}");
            // Console.WriteLine("Konec");

            // Ukol 2)
            // var task1 = DownloadDataAsync(1, 2000);
            // var task2 = DownloadDataAsync(2, 1000);
            // var task3 = DownloadDataAsync(3, 3000);

            // var status = await Task.WhenAll(task1, task2, task3);
            // foreach (var s in status)
            // {
            //     Console.WriteLine(s);
            // }

            // Console.WriteLine("Hotovo");

            //ukol 3)

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var task = LongRunningTaskAsync(token);

            // na paralelním vlákně poběží sledování čtení klávesy a případné ukončení cts
            _ = Task.Run(() =>
            {
                Console.ReadKey();
                cts.Cancel();
            });

            try
            {
                Console.WriteLine("Úloha byla spuštěna. Stiskni libovolnou klávesu pro zrušení.");
                await task;
                Console.WriteLine("Úloha byla dokončena.");

            }
            catch (OperationCanceledException)
            {

                Console.WriteLine("Úloha byla zrušena.");
            }

        }

        public static async Task<string> DownloadDataAsync(int id, int delayInMs)
        {
            await Task.Delay(delayInMs);
            return $"Úloha {id} dokončena za {delayInMs} ms";
        }

        public static async Task LongRunningTaskAsync(CancellationToken token)
        {
            for (int i = 0; i <= 10; i++)
            {
                
                // if (token.IsCancellationRequested)
                // {
                //     // Reaguj na požadavek zrušení
                //     throw new OperationCanceledException(token);
                // }
                
                Console.WriteLine($"Pracuji...{i}");
                await Task.Delay(1000,token);
            }
        }
    }
}