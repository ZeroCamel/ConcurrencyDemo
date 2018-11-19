using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrencyDemo
{
    class Program
    {

        private const int NUM_AES_KEYS = 100000;
        private const int NUM_MD5_HASHES = 1000000;
        static void Main(string[] args)
        {
            //一、Parallel For
            //ParallelLoopResult result = Parallel.For(0, 10, i =>
            //{
            //    Console.WriteLine("迭代次数：{0},任务ID：{1},线程ID：{2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            //    Thread.Sleep(10);

            //});

            //ParallelLoopResult result1 = Parallel.For(0, 100, (i, state) =>
            //{
            //    Console.WriteLine("迭代次数：{0},任务ID：{1},线程ID：{2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            //    Thread.Sleep(10);

            //    if (i >= 5)
            //    {
            //        state.Break();
            //    }
            //});

            //二、Parallel.ForEach
            //string[] data = { "str1", "str2", "str3", "str4", "str5", "str6", "str7" };
            //ParallelLoopResult result = Parallel.ForEach<string>(data, str =>
            //{
            //    Console.WriteLine(str);
            //});

            //ParallelLoopResult result = Parallel.ForEach(data, (str, state, i) =>
            //{
            //    Console.WriteLine("迭代次数：{0}，{1}", i, str);
            //    Thread.Sleep(10);
            //    if (i > 3)
            //    {
            //        state.Break();
            //    }
            //});

            //Console.WriteLine("是否完成：{0}", result.IsCompleted);
            //Console.WriteLine("最低迭代：{0}", result.LowestBreakIteration);

            //三、Paraller.Invoke
            //Parallel.Invoke(()=> {
            //    Thread.Sleep(1000);
            //    Console.WriteLine("method1");
            //},()=>
            //{
            //    Thread.Sleep(1000);
            //    Console.WriteLine("method2");
            //});
            ////输出结果差异 每次顺序不一样
            //Parallel.Invoke(
            //    () => ConvertEllipses(),
            //    () => ConvertRectangles(),
            //    () => ConvertLines(),
            //    () => CovertText()
            //    );

            ////返回VOID的无参数的方法
            //Parallel.Invoke(
            //    ConvertEllipses,
            //    ConvertRectangles,
            //    ConvertLines,
            //    CovertText
            //    );

            //Parallel.Invoke(
            //    () => ConvertEllipses(),
            //    () => ConvertRectangles(),
            //    delegate ()
            //    {
            //        ConvertLines();
            //    },
            //    delegate ()
            //    {
            //        CovertText();
            //    }
            //    );


            //四、TaskFactory
            //using taskfactory
            //TaskFactory t1 = new TaskFactory();
            //t1.StartNew(TestMethod);

            ////using taskfactory via a task
            //Task t2 = Task.Factory.StartNew(TestMethod);

            ////using task construtor
            //Task t3 = new Task(TestMethod);
            //t3.Start();

            ////Specifies falgs that control 
            //Task t4 = new Task(TestMethod, TaskCreationOptions.PreferFairness);
            //t4.Start();

            //Parallel.Invoke(() => { DoOnFirst(); }, () => { DoOnSecond(t2); });

            //五、简单的串行AES密钥和MD5散列生成器
            //1、串行执行38.2417 36.5376 39.923 22.56  并发分区的串行版本运行时间15.21  根据逻辑内核优化分区13.9143
            //var sw = Stopwatch.StartNew();

            //ConvertLines();
            //ConvertEllipses();
            //GenerateAesKeys();
            //GenerateMD5Hashes();
            //ParallelPartitionGenerateAesKeys();
            //ParallelPartitionGenerateMd5Keys();
            //ParallelPartitionGenerateAesKeys1();
            //ParallelPartitionGenerateMd5Keys1();


            //Debug.WriteLine("ALL RUNNING TIME:" + sw.Elapsed.ToString());

            //2、并行执行 37.97 37.89 44.363  22.61723  并发分区并发调用 14.7250 根据逻辑内核优化分区14.4755
            //var sw2 = Stopwatch.StartNew();
            ////Parallel.Invoke(GenerateAesKeys, GenerateMD5Hashes);
            ////Parallel.Invoke(() => GenerateAesKeys(), () => GenerateMD5Hashes(), () => ConvertEllipses(), () => ConvertLines());
            ////Parallel.Invoke(() => ParallelPartitionGenerateAesKeys(), () => ParallelPartitionGenerateMd5Keys(), () => ConvertEllipses(), () => ConvertLines());
            //Parallel.Invoke(() => ParallelPartitionGenerateAesKeys1(), () => ParallelPartitionGenerateMd5Keys1(), () => ConvertEllipses(), () => ConvertLines());

            //Debug.WriteLine("CONCURRENCY TIME:", sw2.Elapsed.ToString());

            //3、在并行循环中退出
            //ParallelForEachGenerateMD5HahesBreak();

            //4、并行异常处理
            //AggregateException();

            //5、指定最大并行度
            //ParallelGenerateAESKeyMaxDegree(Environment.ProcessorCount - 1);
            //ParallelGenerateMD5HashesMaxDegree(Environment.ProcessorCount - 1);

            //Parallel.Invoke(() => ParallelGenerateAESKeyMaxDegree(2), () => ParallelGenerateMD5HashesMaxDegree(2));

            //Console.WriteLine("Finished");

            //6、传播取消并行操作的通知
            //parallelOptions的两个属性 1、cancellationToken 传播取消并行操作的通知 2、TaskScheduler 协调所有的循环

            //7、使用任务来对代码进行并行化

            //创建实例准备开始运行 委托执行
            //STATUS:task.Created 
            //var t1 = new Task(() => GenerateAesKeys());
            //var t2 = new Task(() => GenerateMD5Hashes());

            //t1.Start();
            //t2.Start();

            //Wait for all the tasks to finish
            //Task.WaitAll(t1, t2);

            //Task.WaitAll(new Task[] { t1, t2 }, 3000);

            //8、cancellationToken
            //Console.WriteLine("started");
            //var cts = new CancellationTokenSource();
            //var ct = cts.Token;

            //var sw = Stopwatch.StartNew();
            //var t1 = Task.Factory.StartNew(() => ParallelGenerateAESKeysCancel(ct));
            //var t2 = Task.Factory.StartNew(() => ParallelGenerateMD5KeysCancel(ct));

            ////Sleep the main thread for 1 second
            ////主线程将会保持挂起状态1秒钟 支持任务执行为其他线程将不会被挂起
            //Thread.Sleep(1000);

            ////挂起一秒钟后向任务窗口发送取消状态
            //cts.Cancel();

            //try
            //{
            //    if (!Task.WaitAll(new Task[] { t1, t2 }, 1000))
            //    {
            //        Console.WriteLine("GenerateAESKeys or GenerateMD5Keys are taking more than 1 second to complete.");
            //        Console.WriteLine(t1.Status);
            //        Console.WriteLine(t2.Status);
            //    }
            //}
            //catch (AggregateException ex)
            //{
            //    foreach (Exception innerEx in ex.InnerExceptions)
            //    {
            //        Debug.WriteLine(innerEx.ToString());
            //    }
            //}

            //if (t1.IsFaulted)
            //{
            //    foreach (Exception innerEx in t1.Exception.InnerExceptions)
            //    {
            //        Debug.WriteLine(innerEx.ToString());
            //    }
            //}
            //if (t1.IsCanceled)
            //{
            //    Console.WriteLine("The task running GenerateAESKeysCancel was cancelled");
            //}
            //if (t2.IsCanceled)
            //{
            //    Console.WriteLine("The task running GenerateMD5KeysCancel was cancelled");
            //}

            //Debug.WriteLine(sw.Elapsed.ToString());
            //Console.WriteLine("Finished");

            //9、从任务返回值  chained tasks 链式任务
            //Console.WriteLine("started");

            //var cts = new CancellationTokenSource();
            //var ct = cts.Token;

            //var sw = Stopwatch.StartNew();
            //var t1 = Task.Factory.StartNew(() => GenerateAESKeysWithCharPrefix(ct, 'a'), ct);

            //try
            //{
            //    t1.Wait();
            //}
            //catch (AggregateException ex)
            //{
            //    foreach (Exception innerEx in ex.InnerExceptions)
            //    {
            //        Debug.WriteLine(innerEx.ToString());
            //    }
            //}

            //if (t1.IsCanceled)
            //{
            //    Console.WriteLine("The task running GenerateAESKeysWithCharPrefix was cancelled!");
            //}
            //if (t1.IsFaulted)
            //{
            //    foreach (Exception innerEx in t1.Exception.InnerExceptions)
            //    {
            //        Debug.WriteLine(innerEx.ToString());
            //    }
            //}

            ////TaskCreationOptions
            ////1、AttachedToParent 该任务与一个父任务相关联，可以在其他任务中创建任务
            ////2、None 该任务可以使用默认的行为
            ////3、LongRunning 该任务需要很长时间运行，调度器可以对这个任务使用粗粒度操作，
            ////   如果任务需要好几秒的时间，那么就可以使用这这个参数，不到一秒钟就不可以使用这个参数
            ////4、PreferFairness 更早被调度的任务可能会更早的运行，反之亦然

            ////可以使用位操作组合使用多个值
            //var t2 = Task.Factory.StartNew(() =>
            //{
            //    for (int i = 0; i < t1.Result.Count; i++)
            //    {
            //        Console.WriteLine(t1.Result[i]);
            //    }
            //}, TaskCreationOptions.LongRunning);

            //Debug.WriteLine(sw.Elapsed.ToString());

            //10、TPL允许通过使用Continuation来串联多个任务
            //    任何实例通过调用ContinueWith方法 调用这个方法可以创建一个延续
            //    通过延续串联两个子任务
            //Console.WriteLine("Started");
            //var cts = new CancellationTokenSource();
            //var ct = cts.Token;

            //var sw = Stopwatch.StartNew();
            //var t1 = Task.Factory.StartNew(() => GenerateAESKeysWithCharPrefix(ct, 'a'), ct);
            ////var t2 = t1.ContinueWith((t) =>
            ////{
            ////    for (int i = 0; i < t.Result.Count; i++)
            ////    {
            ////        Console.WriteLine(t.Result[i]);
            ////    }
            ////});
            //var t2 = t1.ContinueWith((t) =>
            //{
            //    for (int i = 0; i < t.Result.Count; i++)
            //    {
            //        Console.WriteLine(t.Result[i]);
            //    }
            //}, TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.LongRunning);

            //try
            //{
            //    t2.Wait();
            //}
            //catch (AggregateException ex)
            //{
            //    foreach (AggregateException innerEx in ex.InnerExceptions)
            //    {
            //        Console.WriteLine(innerEx.ToString());
            //    }
            //}

            //10.1通过延续混合并行代码和串行代码
            //    通过甘特图找出临界区 可以对任务调度进行分析
            //    临界区只有等待所有之前的任务结束执行之后才能够执行不能并行化的串行代码
            //    TaskContinuationOptions 
            //       1、控制和调度延续下一个任务的调度和执行的可选行为
            //       2、这个初始化选项为发给调度器的指令
            //       3、包含11个成员
            //    在使用多任务延续时，不能使用以下值
            //    NotOnRanToCompletion
            //    NotFaulted
            //    NotOnCanceled
            //    OnlyOnRanToCompletionn
            //    OnlyOnFaulted
            //    OnlyOnCanceled

            //11、通过任务编写复杂的带有临界区的并行算法
            //    优秀的设计可以避免额外的开销，这些额外的开销可能导致性能的下降，还有可能导致异常的结果
            //    延续是一种非常强大的功能
            //    1、简化代码
            //    2、帮助调度器对很快就要启动的任务采取正确的操作
            //    编写适应并发和并行的代码
            //    简化复杂的同步问题加了额外的数据结构
            //      并发集合类
            //      轻量级同步原语
            //      惰性初始化的类型
            //    数据结构在设计上尽量避免使用锁，必要的情况下使用细粒度的锁，锁会产生很多潜在的BUG，而且可能极大的降低可扩展性

            //int count = 0;
            //var queue = new Queue<string>();

            ////生产者线程
            //Task.Factory.StartNew(() =>
            //{
            //    while (true)
            //    {
            //        if (count <= 1000)
            //        {
            //            queue.Enqueue("value" + count);
            //            count++;
            //        }
            //    }
            //});
            ////消费者线程1
            //Task.Factory.StartNew(() =>
            //{
            //    while (true)
            //    {
            //        if (queue.Count > 0)
            //        {
            //            string value = queue.Dequeue();
            //            Console.WriteLine("worker 1:" + value);
            //        }

            //    }
            //});
            ////消费者线程2
            //Task.Factory.StartNew(() =>
            //{
            //    while (true)
            //    {
            //        if (queue.Count > 0)
            //        {
            //            string value = queue.Dequeue();
            //            Console.WriteLine("worker 2:" + value);
            //        }
            //    }
            //});

            //并发线程锁与线程局部存储TLS
            //var sw = Stopwatch.StartNew();
            //_keysList = new List<string>(NUM_AES_KEYS);
            //ParallelPartionGenerateAESKeys();
            //Console.WriteLine("Number of keys in the list:{0}", _keysList.Count);
            //Debug.WriteLine(sw.Elapsed.ToString());

            //12、System.Collections.Concurrent  eg:concurrentQueue
            // 可以访问用于并行化循环和PLINQ的自定义分区器
            // 1、BlockingCollection 与经典的阻塞队列数据结构类似 适用于多个任务添加和删除数据生产者-消费者的情形
            //    是对一个IProducerConsumer<T> 实例的包容器，提供了阻塞和限界的能力

            //var sw = Stopwatch.StartNew();
            //_keysQueue = new ConcurrentQueue<string>();

            ////1、简单执行
            ////ParallelPartitionGenerateAESKesForConcurrentQueue();


            ////2、tAsync 状态接收取值
            //var tAsync = Task.Factory.StartNew(() => ParallelPartitionGenerateAESKesForConcurrentQueue());
            //string lastKey;
            //while ((tAsync.Status == TaskStatus.Running) || (tAsync.Status == TaskStatus.WaitingToRun))
            //{
            //    var countResult = _keysQueue.Count(key => key.ToUpper().Contains("F"));

            //    Console.WriteLine("So far ,the number of keys that contain an F is:{0}", countResult);

            //    if (_keysQueue.TryPeek(out lastKey))
            //    {
            //        Console.WriteLine("The first key in the queue is :{0}", lastKey);
            //    }
            //    else
            //    {
            //        Console.WriteLine("No keys yet.");
            //    }
            //    Thread.Sleep(500);
            //}
            //tAsync.Wait();

            //Console.WriteLine("Nubmer of Keys in the list:{0}", _keysQueue.Count);
            //Debug.WriteLine(sw.Elapsed.ToString());

            //var sw = Stopwatch.StartNew();

            ////生产者队列
            //_keysQueue = new ConcurrentQueue<string>();
            ////消费者队列
            //_byteArrayQueue = new ConcurrentQueue<byte[]>();

            ////生产者
            ////var taskAESKeys = Task.Factory.StartNew(() =>
            ////{
            ////    ParallelPartionGenerateAESKeysForByteQueue(Environment.ProcessorCount - 1);
            ////});
            ////生产者最大并行度
            //int taskAESKeysMax = Environment.ProcessorCount / 2;
            //int taskHexStringMax = Environment.ProcessorCount - taskAESKeysMax;
            //var taskAESKeys = Task.Factory.StartNew(() =>
            //{
            //    ParallelPartionGenerateAESKeysForByteQueue(taskAESKeysMax);
            //});

            ////单个消费者
            ////var taskHexStrings = Task.Factory.StartNew(() => ConvertAESKeysToHex(taskAESKeys));

            ////多个消费者
            //Task[] taskHexStrings = new Task[taskHexStringMax];
            //for (int i = 0; i < taskHexStringMax; i++)
            //{
            //    taskHexStrings[i] = Task.Factory.StartNew(() => ConvertAESKeysToHex(taskAESKeys));
            //}

            //Task.WaitAll(taskHexStrings);

            ////string laskKey;
            ////while ((taskHexStrings.Status == TaskStatus.Running) || (taskHexStrings.Status == TaskStatus.WaitingToRun))
            ////{
            ////    var countResult = _keysQueue.Count(key => key.Contains("f"));
            ////    Console.WriteLine("So far,the number of keys that contain an F is:{0}", countResult);

            ////    if (_keysQueue.TryPeek(out laskKey))
            ////    {
            ////        Console.WriteLine("The first key in the queue is:{0}", laskKey);
            ////    }
            ////    else
            ////    {
            ////        Console.WriteLine("No keys yet");
            ////    }

            ////    Thread.Sleep(500);
            ////}

            ////Task.WaitAll(taskAESKeys, taskHexStrings);

            //Console.WriteLine("Nubmer of Keys in the list:{0}", _keysQueue.Count);
            //Debug.WriteLine(sw.Elapsed.ToString());

            //13、多个生产者和多个消费者
            //var sw = Stopwatch.StartNew();
            //_byteArrayQueue = new ConcurrentQueue<byte[]>();
            //_keysQueue = new ConcurrentQueue<string>();
            //_validKeys = new ConcurrentQueue<string>();

            ////逻辑内核/2 超线程 HIT
            //int taskAESKeysMax = Environment.ProcessorCount / 2;
            //int taskHexStringsMax = Environment.ProcessorCount - taskAESKeysMax - 1;

            ////生产者
            //var taskAESKeys = Task.Factory.StartNew(() =>
            //{
            //    ParallelPartionGenerateAESKeysForByteQueue(taskHexStringsMax);
            //});

            ////生产者也是消费者
            //Task[] taskHexString = new Task[taskAESKeysMax];
            //for (int i = 0; i < taskHexStringsMax; i++)
            //{
            //    Interlocked.Increment(ref tasksHexStringsRunning);
            //    taskHexString[i] = Task.Factory.StartNew(() =>
            //    {
            //        try
            //        {
            //            ConvertAESKeysToHex(taskAESKeys);
            //        }
            //        finally
            //        {
            //            Interlocked.Decrement(ref tasksHexStringsRunning);
            //        }
            //    });
            //}

            ////消费者
            //var taskValidateKeys = Task.Factory.StartNew(() =>
            //{
            //    ValidateKeys();
            //});

            //taskValidateKeys.Wait();

            //Console.WriteLine("Nubmer of Keys in the list:{0}", _keysQueue.Count);
            //Console.WriteLine("Nubmer of Valid keys:{0}", _validKeys.Count);
            //Debug.WriteLine(sw.Elapsed.ToString());

            //14、ConcurrentStack实现生产者和消费者流水线
            var sw = Stopwatch.StartNew();


            _byteArraysStack = new ConcurrentStack<byte[]>();
            _keyStack = new ConcurrentStack<string>();
            _validKeyStack = new ConcurrentStack<string>();

            var taskAESMax = Environment.ProcessorCount / 2;
            var taskHexStringMax = Environment.ProcessorCount - taskAESMax - 1;

            var taskAESTask = Task.Factory.StartNew(() =>
            {
                PallelPatitionGenerateAESKeys(taskAESMax);
            });

            Task[] taskHexString = new Task[taskHexStringMax];
            for (int i = 0; i < taskHexStringMax; i++)
            {
                Interlocked.Increment(ref taskHexStringRunning);
                taskHexString[i] = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        ConvertAESKeysToHexForConcurr(taskAESTask);
                    }
                    finally
                    {
                        Interlocked.Decrement(ref taskHexStringRunning);
                    }
                });
            }

            var validTaskKeys = Task.Factory.StartNew(()=>
            {
                ValidateKeysForStackRange();
            });

            validTaskKeys.Wait();

            //12、ConcurrentBag
            //优势 
            //    同一线程添加元素（生产者）或者删除元素（消费者）的场合下效率较高
            //    无序的对象集合支持对象重复不考察顺序的时候存储和访问对象非常有用

            


            Console.WriteLine("Nubmer of Keys in the list:{0}", _keysQueue.Count);
            Console.WriteLine("Nubmer of Valid keys:{0}", _validKeys.Count);
            Debug.WriteLine(sw.Elapsed.ToString());


            #region TLS 本地存储槽 
            //JAVA中 ThreadLocal 机制 
            //为每一个使用该变量的线程提供一个变量值的副本，是每一个线程都可以独立的改变自己的副本，而不会和其他线程副本冲突
            //就像每一个线程都拥有该变量

            //1、使用ThreadStatic特性
            //Thread th = new Thread(() =>
            //{
            //    str = "Mgen"; Display();
            //});
            //th.Start();
            //th.Join();
            //Display();

            //2、使用命名的LocalDataStoreSlot类型提供更好的TLS支持
            //LocalDataStoreSlot slot = Thread.AllocateNamedDataSlot("slot");
            //Thread.SetData(slot, "hehe");
            //Thread th = new Thread(() =>
            //{
            //    Thread.SetData(slot, "mert");
            //    Display1();
            //});

            //th.Start();
            //th.Join();
            //Display();
            ////清除slot
            //Thread.FreeNamedDataSlot("slot");

            //3、使用未命名的LocalDataStoreSlot类型
            //未命名的LocalDataStoreSlot不需要手动清除
            //slot = Thread.AllocateDataSlot();
            //Thread.SetData(slot, "hehe");
            //Thread th = new Thread(() =>
            //  {             
            //      Thread.SetData(slot, "haha");
            //      Display2();
            //  });

            //th.Start();
            ////Join让父线程等待子线程结束之后才运行
            ////阻止后面的代码并发执行
            //th.Join();
            //Display2();

            #endregion

            Console.WriteLine("Finished!");

            Console.Read();
        }
        #region 使用并发栈实例实现的流水线
        private static ConcurrentStack<byte[]> _byteArraysStack;
        private static ConcurrentStack<string> _keyStack;
        private static ConcurrentStack<string> _validKeyStack;
        private static int taskHexStringRunning = 0;

        private static void PallelPatitionGenerateAESKeys(int maxDegree)
        {
            var paraOptions = new ParallelOptions();
            paraOptions.MaxDegreeOfParallelism = maxDegree;

            var sw = Stopwatch.StartNew();
            Parallel.ForEach(Partitioner.Create(1, NUM_AES_KEYS + 1), paraOptions, range =>
            {
                var aesm = new AesManaged();
                byte[] result;
                Debug.WriteLine("ASE RANGE({0},{1}.TIME:{2})", range.Item1, range.Item2, DateTime.Now.TimeOfDay);
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    aesm.GenerateKey();
                    result = aesm.Key;
                    _byteArraysStack.Push(result);
                }
            });

            Debug.WriteLine("AES:" + sw.Elapsed.ToString());
        }

        private static void ConvertAESKeysToHexForConcurr(Task taskProducer)
        {
            var sw = Stopwatch.StartNew();
            while ((taskProducer.Status == TaskStatus.Running) || (taskProducer.Status == TaskStatus.WaitingToRun) || (!_byteArraysStack.IsEmpty))
            {
                Byte[] result;
                if (_byteArraysStack.TryPop(out result))
                {
                    string hexString = ConvertToHexString(result);
                    _keyStack.Push(hexString);
                }
            }
            Debug.WriteLine("HEX:" + sw.Elapsed.ToString());
        }
        private static void ValidateKeysForStack()
        {
            var sw = Stopwatch.StartNew();
            while ((taskHexStringRunning > 0) || (!_keyStack.IsEmpty))
            {
                string hexString;
                if (_keyStack.TryPop(out hexString))
                {
                    if (IsKeyValid(hexString))
                    {
                        _validKeyStack.Push(hexString);
                    }
                }
            }
            Debug.WriteLine("VALIDATE:" + sw.Elapsed.ToString());
        }

        private static void ValidateKeysForStackRange()
        {
            var sw = Stopwatch.StartNew();

            const int bufferSize = 100;
            string[] hexString = new string[bufferSize];
            string[] validHexString = new string[bufferSize];

            while ((taskHexStringRunning > 0) || (!_keyStack.IsEmpty))
            {
                int numItems = _keyStack.TryPopRange(hexString, 0, bufferSize);
                int numValidKeys = 0;
                for (int i = 0; i < numItems; i++)
                {
                    if (IsKeyValid(hexString[i]))
                    {
                        validHexString[numValidKeys] = hexString[i];
                        numValidKeys++;
                    }
                }
                if (numValidKeys > 0)
                {
                    _keyStack.PushRange(validHexString, 0, numValidKeys);
                }
            }
            Debug.WriteLine("AES:" + sw.Elapsed.ToString());
        }

        #endregion

        #region 生产者和消费者-三阶段的线性流水线-至少需要三个逻辑内核
        private static string[] _invalidHexValues = { "af", "bd", "bf", "cf", "da", "fa", "fe", "ff" };
        private static int MAX_INVALID_HEX_VALUES = 3;
        private static int tasksHexStringsRunning = 0;

        private static ConcurrentQueue<string> _validKeys;

        private static bool IsKeyValid(string key)
        {
            var sw = Stopwatch.StartNew();
            int count = 0;
            for (int i = 0; i < _invalidHexValues.Length; i++)
            {
                if (key.Contains(_invalidHexValues[i]))
                {
                    count++;
                    if (count == MAX_INVALID_HEX_VALUES)
                    {
                        return true;
                    }
                    if (((_invalidHexValues.Length - i) + count) < MAX_INVALID_HEX_VALUES)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        private static void ValidateKeys()
        {
            var sw = Stopwatch.StartNew();
            while ((tasksHexStringsRunning > 0) || (_keysQueue.Count > 0))
            {
                string hexString;
                if (_keysQueue.TryDequeue(out hexString))
                {
                    if (IsKeyValid(hexString))
                    {
                        _validKeys.Enqueue(hexString);
                    }
                }
            }
            Debug.WriteLine(sw.Elapsed.ToString());
        }

        #endregion

        private static ConcurrentQueue<string> _keysQueue;

        private static ConcurrentQueue<byte[]> _byteArrayQueue;

        private static void ParallelPartionGenerateAESKeysForByteQueue(int maxDegree)
        {
            var paraOperation = new ParallelOptions();
            paraOperation.MaxDegreeOfParallelism = maxDegree;

            var sw = Stopwatch.StartNew();
            Parallel.ForEach(Partitioner.Create(1, NUM_AES_KEYS + 1), paraOperation, range =>
            {
                var aesm = new AesManaged();
                Debug.WriteLine("AES RANGE ({0},{1}.TIME {2})", range.Item1, range.Item2, DateTime.Now.TimeOfDay);

                for (int i = range.Item1; i < range.Item2; i++)
                {
                    aesm.GenerateKey();
                    byte[] result = aesm.Key;
                    _byteArrayQueue.Enqueue(result);
                }
            });
            Debug.WriteLine(sw.Elapsed.ToString());
        }

        private static void ConvertAESKeysToHex(Task taskProducer)
        {
            var sw = Stopwatch.StartNew();
            while ((taskProducer.Status == TaskStatus.Running) || (taskProducer.Status == TaskStatus.WaitingToRun) || (_byteArrayQueue.Count > 0))
            {
                Byte[] result;
                if (_byteArrayQueue.TryDequeue(out result))
                {
                    string hexString = ConvertToHexString(result);
                    _keysQueue.Enqueue(hexString);
                }
            }
            Debug.WriteLine("HEX: " + sw.Elapsed.ToString());
        }

        private static void ParallelPartitionGenerateAESKesForConcurrentQueue()
        {
            var sw = Stopwatch.StartNew();
            Parallel.ForEach(Partitioner.Create(1, NUM_AES_KEYS + 1), range =>
                {
                    var aesm = new AesManaged();
                    Debug.WriteLine("ASE RANGE ({0},{1},TIMEOF DAY OF INNER LOOP STARTS:{2})", range.Item1, range.Item2, DateTime.Now.TimeOfDay);
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        aesm.GenerateKey();
                        byte[] result = aesm.Key;
                        string hexString = ConvertToHexString(result);
                        _keysQueue.Enqueue(hexString);
                    }

                });
            Debug.WriteLine(sw.Elapsed.ToString());
        }


        //TLS 线程局部存储静态字段 线程不安全 需要创建临界代码访问区
        private static List<string> _keysList;

        /// <summary>
        /// TLS 本地线程同步锁
        /// </summary>
        private static void ParallelPartionGenerateAESKeys()
        {
            //LocalDataStoreSlot线程本地存储 封装内存槽以存储本地数据
            var sw = Stopwatch.StartNew();
            Parallel.ForEach(Partitioner.Create(1, NUM_AES_KEYS + 1), range =>
                {
                    var aesm = new AesManaged();
                    Debug.WriteLine("AES Range({0},{1}.Time:{2})", range.Item1, range.Item2, DateTime.Now.TimeOfDay);

                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        aesm.GenerateKey();
                        byte[] result = aesm.Key;
                        string hexString = ConvertToHexString(result);

                        //确保每次只有一个线程运行_keyList对象里的方法
                        //Lock 关键词创造了一个临界代码区，但是增加了一定的开销，会降低可扩展性
                        //临界代码区可以对_keyList进行排他访问 其他任务将会试图获得该锁，锁在释放之前
                        //会它们将会阻塞并等待进入这个临界区

                        lock (_keysList)
                        {
                            _keysList.Add(hexString);
                        }
                    }
                });
            Debug.WriteLine("AES: " + sw.Elapsed.ToString());
        }

        //线程静态变量
        [ThreadStatic]
        static string str = "hehe";

        static void Display()
        {
            Console.WriteLine("{0} {1}", Thread.CurrentThread.ManagedThreadId, str);
        }

        static void Display1()
        {
            LocalDataStoreSlot slot = Thread.GetNamedDataSlot("slot");
            Console.WriteLine("{0},{1}", Thread.CurrentThread.ManagedThreadId, Thread.GetData(slot));
        }

        static LocalDataStoreSlot slot;

        static void Display2()
        {
            Console.WriteLine("{0},{1}", Thread.CurrentThread.ManagedThreadId, Thread.GetData(slot));
        }

        /// <summary>
        /// 将二进制数组转化为16进制
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        private static String ConvertToHexString(Byte[] byteArray)
        {
            var sb = new StringBuilder(byteArray.Length);

            for (int i = 0; i < byteArray.Length; i++)
            {
                sb.Append(byteArray[i].ToString("x2"));
            }

            return sb.ToString();
        }

        private static List<string> GenerateAESKeysWithCharPrefix(CancellationToken ct, char prefix)
        {
            var sw = Stopwatch.StartNew();
            var aesm = new AesManaged();
            var keysList = new List<string>();
            for (int i = 1; i <= NUM_AES_KEYS; i++)
            {
                aesm.GenerateKey();
                byte[] result = aesm.Key;
                string hexString = ConvertToHexString(result);
                if (hexString[0] == prefix)
                {
                    keysList.Add(hexString);
                }
                //Console.WriteLine("AES KEY:" + hexString.ToString());
                if (ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                }
            }
            Debug.WriteLine("AES:" + sw.Elapsed.ToString());
            return keysList;
        }

        /// <summary>
        /// 传入取消任务标签戳
        /// </summary>
        /// <param name="ct"></param>
        private static void ParallelGenerateAESKeysCancel(System.Threading.CancellationToken ct)
        {
            //判断任务是否取消
            ct.ThrowIfCancellationRequested();
            var sw = Stopwatch.StartNew();
            var aesm = new AesManaged();
            for (int i = 0; i <= NUM_AES_KEYS; i++)
            {
                aesm.GenerateKey();
                byte[] result = aesm.Key;
                string hexString = ConvertToHexString(result);
                //Console.WriteLine("AES KEY:{0}", hexString);
                if (sw.Elapsed.TotalSeconds > 0.5)
                {
                    throw new Exception("GenerateAESKeyCancel is taking more than 0.5 seconds to complete.");
                }
                ct.ThrowIfCancellationRequested();
            }
            Debug.WriteLine("AES: " + sw.Elapsed.ToString());
        }

        private static void ParallelGenerateMD5KeysCancel(System.Threading.CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var sw = Stopwatch.StartNew();
            var md5 = MD5.Create();
            for (int i = 0; i <= NUM_MD5_HASHES; i++)
            {
                byte[] data = Encoding.UTF8.GetBytes(Environment.UserName + i.ToString());
                byte[] result = md5.ComputeHash(data);
                string hexString = ConvertToHexString(result);
                Console.WriteLine("MD5 HASH:{0}", hexString);
                ct.ThrowIfCancellationRequested();
            }

            Debug.WriteLine("MD5 :" + sw.Elapsed.ToString());
        }

        /// <summary>
        /// maxDegree-静态并行度
        /// </summary>
        /// <param name="maxDegree"></param>
        private static void ParallelGenerateAESKeyMaxDegree(int maxDegree)
        {
            var parallelOption = new ParallelOptions();
            parallelOption.MaxDegreeOfParallelism = maxDegree;
            var sw = Stopwatch.StartNew();
            Parallel.For(1, NUM_AES_KEYS + 1, parallelOption, (int i) =>
              {
                  var aesm = new AesManaged();
                  byte[] result = aesm.Key;
                  string hexString = ConvertToHexString(result);

              });
            Debug.WriteLine("AES: " + sw.Elapsed.ToString());
        }

        /// <summary>
        /// 静态并行度
        /// </summary>
        /// <param name="maxDegree"></param>
        private static void ParallelGenerateMD5HashesMaxDegree(int maxDegree)
        {
            var parallelOption = new ParallelOptions();
            parallelOption.MaxDegreeOfParallelism = maxDegree;
            var sw = Stopwatch.StartNew();
            Parallel.For(1, NUM_MD5_HASHES + 1, parallelOption, (int i) =>
            {
                var md5 = MD5.Create();
                byte[] data = Encoding.Unicode.GetBytes(Environment.UserName + i.ToString());
                string hexString = ConvertToHexString(data);
            });

            Debug.WriteLine("MD5:" + sw.Elapsed.ToString());
        }

        private static void GenerateAesKeys()
        {
            var sw = Stopwatch.StartNew();
            ////AES管理类
            //var asem = new AesManaged();
            //for (int i = 1; i < NUM_AES_KEYS; i++)
            //{
            //    asem.GenerateKey();
            //    byte[] result = asem.Key;
            //    string hexString = ConvertToHexString(result);

            //    //Console.WriteLine("AES KEY:{0}", hexString);
            //}

            //for 循环进行重构 
            //!: var aesm = new AesManaged();需要转移到委托内部 保证生成的局部存储Key不会被并行迭代所共享
            Parallel.For(1, NUM_AES_KEYS + 1, (int i) =>
            {
                var aesm = new AesManaged();
                byte[] result = aesm.Key;
                string hexString = ConvertToHexString(result);
                //Console.WriteLine("AES KEY:" + hexString);
            });

            Debug.WriteLine("AES:" + sw.Elapsed.ToString());
        }
        private static void ParallelPartitionGenerateAesKeys()
        {
            var sw = Stopwatch.StartNew();
            //using system.collection.concurrency
            Parallel.ForEach(Partitioner.Create(1, NUM_AES_KEYS + 1), range =>
                {
                    var aesm = new AesManaged();
                    Debug.WriteLine("AES RANGE ({0},{1}.TIMEOF BEFORE INNER LOOP STARS :{2})", range.Item1, range.Item2, DateTime.Now.TimeOfDay);
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        aesm.GenerateKey();
                        byte[] result = aesm.Key;
                        string hexString = ConvertToHexString(result);
                    }
                });
            Debug.WriteLine("AES TIME:" + sw.Elapsed.ToString());
        }

        private static void AggregateException()
        {
            try
            {
                Task pt = new Task(() =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        throw new Exception("ex 1");
                    }, TaskCreationOptions.AttachedToParent);
                    Task.Factory.StartNew(() =>
                    {
                        Task.Factory.StartNew(() =>
                        {
                            throw new Exception("ex 2-1");
                        }, TaskCreationOptions.AttachedToParent);
                    }, TaskCreationOptions.AttachedToParent);
                });

                pt.Start();

                pt.ContinueWith(t =>
                {
                    t.Exception.Handle(ex =>
                    {
                        Console.WriteLine(ex.Message);
                        return true;
                    });
                }, TaskContinuationOptions.OnlyOnFaulted);

            }
            catch (Exception)
            {
                //异常处理

            }

        }


        private static void ParallelPartitionGenerateAesKeys1()
        {
            var sw = Stopwatch.StartNew();
            //using system.collection.concurrency
            Parallel.ForEach(Partitioner.Create(1, NUM_AES_KEYS + 1, ((int)(NUM_AES_KEYS / System.Environment.ProcessorCount) + 1)), range =>
                 {
                     var aesm = new AesManaged();
                     Debug.WriteLine("AES RANGE ({0},{1}.TIMEOF BEFORE INNER LOOP STARS :{2})", range.Item1, range.Item2, DateTime.Now.TimeOfDay);
                     for (int i = range.Item1; i < range.Item2; i++)
                     {
                         aesm.GenerateKey();
                         byte[] result = aesm.Key;
                         string hexString = ConvertToHexString(result);
                     }
                 });
            Debug.WriteLine("AES TIME:" + sw.Elapsed.ToString());
        }

        public static void DisplayParallelLoopResult(ParallelLoopResult paraLoopResult)
        {
            string text = string.Empty;
            if (paraLoopResult.IsCompleted)
            {
                text = "The loop ran to completion";
            }
            else
            {
                if (paraLoopResult.LowestBreakIteration.HasValue)
                {
                    text = "The loop ended by calliing the break statement";
                }
                else
                {
                    text = "The loop ended prematurely with a stop statement";
                }
            }
            Console.WriteLine(text);
        }

        private static void ParallelForEachGenerateMD5HahesBreak()
        {
            var sw = Stopwatch.StartNew();
            var inputData = GenerateMD5InputData();
            var loopResult = Parallel.ForEach(inputData, (int number, ParallelLoopState loopState) =>
             {
                 var md5M = MD5.Create();
                 byte[] data = Encoding.Unicode.GetBytes(Environment.UserName + number.ToString());
                 byte[] result = md5M.ComputeHash(data);
                 string hexString = ConvertToHexString(result);
                 if (sw.Elapsed.Seconds > 3)
                 {
                     //执行完但当前循环之后尽快停止
                     loopState.Break();
                     return;
                 }
             });
            DisplayParallelLoopResult(loopResult);
            Debug.WriteLine("MD5:" + sw.Elapsed.ToString());
        }

        private static IEnumerable<int> GenerateMD5InputData()
        {
            return Enumerable.Range(1, NUM_AES_KEYS);
        }
        private static void ParallelForEachMD5HashesException()
        {
            var sw = Stopwatch.StartNew();
            var inputData = GenerateMD5InputData();
            var loopResult = new ParallelLoopResult();
            try
            {
                loopResult = Parallel.ForEach(inputData, (int number, ParallelLoopState loopState) =>
                {
                    var md5M = MD5.Create();
                    byte[] data = Encoding.Unicode.GetBytes(Environment.UserName + number.ToString());
                    byte[] result = md5M.ComputeHash(data);
                    string hexString = ConvertToHexString(result);

                    if (sw.Elapsed.Seconds > 3)
                    {
                        throw new TimeoutException("Parallel.ForEach is taking more than 3 seconds to complete");
                    }
                });
            }
            catch (AggregateException ex)
            {
                foreach (Exception innerEx in ex.InnerExceptions)
                {
                    Debug.WriteLine(innerEx.ToString());
                }
            }
        }

        private static void GenerateMD5Hashes()
        {
            var sw = Stopwatch.StartNew();
            //var md5M = MD5.Create();
            //朴素For循环
            //for (int i = 1; i < NUM_MD5_HASHES; i++)
            //{
            //    byte[] data = Encoding.Unicode.GetBytes(Environment.UserName + i.ToString());
            //    byte[] result = md5M.ComputeHash(data);
            //    string hexString = ConvertToHexString(result);

            //    //Console.WriteLine("MD5 HASH:{0}", hexString);
            //}
            //并行循环重构 22.56
            //var md5M = MD5.Create(); 将此句移到程序外边 出现问题-已关闭SafeHandle
            Parallel.For(1, NUM_MD5_HASHES, (int i) =>
            {
                var md5M = MD5.Create();
                byte[] data = Encoding.Unicode.GetBytes(Environment.UserName + i.ToString());
                byte[] result = md5M.ComputeHash(data);
                string hexString = ConvertToHexString(result);
            });
            Debug.WriteLine("MD5:" + sw.Elapsed.ToString());
        }

        //并发分区
        public static void ParallelPartitionGenerateMd5Keys()
        {
            var sw = Stopwatch.StartNew();
            Parallel.ForEach(Partitioner.Create(1, NUM_MD5_HASHES), (range) =>
              {
                  var md5M = MD5.Create();
                  Debug.WriteLine("ASE RANGE ({0},{1} TIEME OF BEFORE LOOP STARS:{2})", range.Item1, range.Item2, DateTime.Now.TimeOfDay);
                  for (int i = range.Item1; i < range.Item2; i++)
                  {
                      byte[] result = Encoding.Unicode.GetBytes(Environment.UserName + i.ToString());
                      string hexString = ConvertToHexString(result);
                  }
              });
            Debug.WriteLine("MD5:" + sw.Elapsed.ToString());
        }

        //并发分区
        public static void ParallelPartitionGenerateMd5Keys1()
        {
            var sw = Stopwatch.StartNew();
            Parallel.ForEach(Partitioner.Create(1, NUM_MD5_HASHES, ((int)(NUM_MD5_HASHES / System.Environment.ProcessorCount) + 1)), (range) =>
            {
                var md5M = MD5.Create();
                Debug.WriteLine("ASE RANGE ({0},{1} TIEME OF BEFORE LOOP STARS:{2})", range.Item1, range.Item2, DateTime.Now.TimeOfDay);
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    byte[] result = Encoding.Unicode.GetBytes(Environment.UserName + i.ToString());
                    string hexString = ConvertToHexString(result);
                }
            });
            Debug.WriteLine("MD5:" + sw.Elapsed.ToString());
        }
        static void ConvertEllipses()
        {
            Console.WriteLine("Ellipses converted");
        }
        static void ConvertRectangles()
        {
            Console.WriteLine("Rectangles converted");
        }

        static void ConvertLines()
        {
            Console.WriteLine("Lines converted");
        }

        static void CovertText()
        {
            Console.WriteLine("Text converted");
        }

        static void TestMethod()
        {
            Console.WriteLine("Running in a task");
            Console.WriteLine("Task is :{0}", Task.CurrentId);
        }
        static void DoOnFirst()
        {
            Console.WriteLine("doing some task {0}", Task.CurrentId);
            Thread.Sleep(3000);
        }

        static void DoOnSecond(Task t)
        {
            Console.WriteLine("task {0} finished", t.Id);
            Console.WriteLine("this task id {0}", Task.CurrentId);
            Console.WriteLine("do some cleanup");
            Thread.Sleep(3000);
        }
    }
}
