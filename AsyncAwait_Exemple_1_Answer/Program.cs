using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait_Exemple_1_Answer
{
    /// <summary>
    /// <see cref="https://ecogamedesign.wordpress.com/2013/02/17/code-beauty-c-async-and-threading/"/>
    /// <see cref="https://stackoverflow.com/questions/33821679/async-await-different-thread-id"/>
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var sc = SynchronizationContext.Current;
                Console.WriteLine("Start Main: Thread" + Thread.CurrentThread.ManagedThreadId);
                int i = Method1().GetAwaiter().GetResult();

                Console.WriteLine($"int i = {i} Main: Thread" + Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
            }

            Console.WriteLine("Finish Main: Thread" + Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey(true);
        }

        static async Task<int> Method1()
        {
            var sc = SynchronizationContext.Current;
            int foo = 3;
            Console.WriteLine("Method1 Task.Run: Thread" + Thread.CurrentThread.ManagedThreadId);

            Task.Factory.StartNew(() =>
            {
                foo = 3 + 2;
                Console.WriteLine($"foo = {foo}  Method1: Thread" + Thread.CurrentThread.ManagedThreadId);
            }).GetAwaiter().GetResult();

            Console.WriteLine($"foo = {foo}  Method1- Two: Thread" + Thread.CurrentThread.ManagedThreadId);

            //Task.Delay создает не завершенную таску, т.к он использует внутри себя класс Timer который завязан на TaskScheduler. 
            //По окончанию работы таймера не идет уведомнения о возврате в основной поток, так как нет SynchronizationContext.
            await Task.Delay(1);

            //await Task.Delay(0);
            //Task.Delay(1).Wait();
            //Task.Delay(1).GetAwaiter().GetResult();

            Console.WriteLine("Finish Method1: Thread" + Thread.CurrentThread.ManagedThreadId);

            return foo;
        }
    }
}
