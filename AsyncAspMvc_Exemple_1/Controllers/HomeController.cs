using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AsyncAspMvc_Exemple_1.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var sc = SynchronizationContext.Current;

            Debug.WriteLine("Start Main: Thread" + Thread.CurrentThread.ManagedThreadId);

            int i = await Method1();

            Debug.WriteLine($"int i = {i} Main: Thread" + Thread.CurrentThread.ManagedThreadId);
            Debug.WriteLine("Finish Main: Thread" + Thread.CurrentThread.ManagedThreadId);

            return View();
        }

        static async Task<int> Method1()
        {
            int foo = 3;
            Debug.WriteLine("Method1 Task.Run: Thread" + Thread.CurrentThread.ManagedThreadId);

            await Task.Factory.StartNew(() =>
            {
                foo = 3 + 2;
                Debug.WriteLine($"foo = {foo}  Method1: Thread" + Thread.CurrentThread.ManagedThreadId);
            });

            Debug.WriteLine($"foo = {foo}  Method1- Two: Thread" + Thread.CurrentThread.ManagedThreadId);

           // await Task.Delay(1);

            Debug.WriteLine("Finish Method1: Thread" + Thread.CurrentThread.ManagedThreadId);

            return foo;
        }
    }
}