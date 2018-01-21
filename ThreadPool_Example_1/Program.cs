using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPool_Example_1
{
    class Program
    {
        static void Main()
        {
            try
            {
                ThreadPool.GetMaxThreads(out var nWorkerThreads, out var nCompletionThreads);

                Console.WriteLine("Максимальное количество потоков: " + nWorkerThreads
                                  + "\nПотоков ввода-вывода доступно: " + nCompletionThreads);

                Task.Factory.StartNew(() =>
                    {
                        // какие то действия
                    }
                    , new CancellationToken()
                    , TaskCreationOptions.None
                    , TaskScheduler.Default);

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
            }



            Console.ReadLine();
        }
    }
}
