using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;


namespace AsyncAwait_Exemple_1
{

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

            await Task.Delay(1);

            Console.WriteLine("Finish Method1: Thread" + Thread.CurrentThread.ManagedThreadId);

            return foo;
        }
    }
}
