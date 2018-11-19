using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpinWaitForConcurrent
{
    class Program
    {
        /// <summary>
        /// 自旋锁和ThreadSleep
        /// </summary>
        /// <param name="args"></param>

        static int _count = 1000;
        static int _timeout_ms = 10;
        static int _timeout_ms0 = 0;
        static void Main(string[] args)
        {
            NoSleep();
            ThreadSleepInThread();
            SpinWaitInThread();

            Console.Read();
        }
        static void NoSleep()
        {
            Thread thread = new Thread(() =>
            {
                var sw = Stopwatch.StartNew();
                for (int i = 0; i < _count; i++)
                {

                }
                Console.WriteLine("No Sleep consume Time:{0}", sw.Elapsed.ToString());
            });
            thread.IsBackground = true;
            thread.Start();
        }

        static void ThreadSleepInThread()
        {
            Thread thread = new Thread(() =>
            {
                var sw = Stopwatch.StartNew();
                for (int i = 0; i < _count; i++)
                {
                    Thread.Sleep(_timeout_ms0);
                }
                Console.WriteLine("ThreadSleep consume Time:{0}", sw.Elapsed.ToString());
            });
            thread.IsBackground = true;
            thread.Start();
        }
        static void SpinWaitInThread()
        {
            Thread thread = new Thread(() =>
            {
                var sw = Stopwatch.StartNew();
                for (int i = 0; i < _count; i++)
                {
                    //时间过期后自旋-在CPU运转的周期内条件满足之后需不需要进入内核等待
                    //轻型同步类型可用于低级方案，以避免执行内核事件所需的高成本上下文切换和内核切换
                    //包装内核事件
                    //一直空转直到某条件为True
                    SpinWait.SpinUntil(() => true, _timeout_ms0);
                }
                Console.WriteLine("SpinWait Consume Time:{0}",sw.Elapsed.ToString());
            });
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
