using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SynchroThreading
{
    class Program
    {
        static void Main(string[] args)
        {
            //AsynchAwait();

            //TroubleSynch();
            SynchroThreading();
            //SpinLock();

            //AutoResetEvent();
            //Semaphore();
            //Mutex();
            //Hybrid();

            Console.ReadLine();
        }


        static private void SharedResourse(ISinchroThreading s)
        {            
            s.Enter();
            if (s is SynchroMutex)
                s.Enter();
            Thread.Sleep(500);
            Console.WriteLine("Thread {0} started!", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(100000);
            Console.WriteLine("Thread {0} done!", Thread.CurrentThread.ManagedThreadId);
            s.Exit();
            if (s is SynchroMutex)
                s.Exit();            
        }

        //событие
        static void AutoResetEvent()
        {
            SinchroEvent e = new SinchroEvent();
            for (int i = 0; i < 3; i++)
                Task.Run(() => SharedResourse(e));
        }

        //семафор
        static void Semaphore()
        {
            SinchroSemaphore e = new SinchroSemaphore();
            for (int i = 0; i < 3; i++)
                Task.Run(() => SharedResourse(e));            
        }

        //мютекс
        static void Mutex()
        {            
            SynchroMutex e = new SynchroMutex();
            for (int i = 0; i < 3; i++)
                Task.Run(() => SharedResourse(e));
        }

        //мютекс
        static void Hybrid()
        {
            HybridSynchro e = new HybridSynchro();
            for (int i = 0; i < 3; i++)
                Task.Run(() => SharedResourse(e));
        }


        //конструкция пользовательского режима - аналог lock
        static void SpinLock()
        {
            SinchroSpinLock e = new SinchroSpinLock();
            ISinchroThreading o = e; //т.к SinchroSpinLock структура, будет передаваться копия в разделяемый ресурс, соответственно синхронизация не будет работать
            for (int i = 0; i < 3; i++)                            
                Task.Run(() => SharedResourse(o));
            
        }


        public void Resource()
        {
            //this make trouble - потому что маркер this захвачен другим потоком, снаружи
            Monitor.Enter(this);            
            Thread.Sleep(1000);
            Monitor.Exit(this);
        }

        static void TroubleSynch()
        {            
            Program prg = new Program();
            Monitor.Enter(prg);
            ThreadPool.QueueUserWorkItem(x => prg.Resource());
            Thread.Sleep(15000);
            Monitor.Exit(prg);
        }


        static void SynchroThreading()
        {
            object obj = new object();
            //SpinLock sl = new SpinLock();

            WaitCallback act = (x) =>
            {
                //lock (obj)
                //bool flag = false;
                //sl.Enter(ref flag);
                Monitor.Enter(obj);
                {                    
                    Console.WriteLine("Thread = {0}", Thread.CurrentThread.ManagedThreadId);
                    for (int j = 1; j < 10; j++)
                    {
                        Console.Write(j + " ");
                        Thread.Sleep(100);
                    }
                    Console.WriteLine();
                }
                Monitor.Exit(obj);
                //sl.Exit();

            };

            for (int i = 0; i < 10; i++)
            {

                ThreadPool.QueueUserWorkItem(act, i);
                Thread.Sleep(1000);
            }
        }

        static async Task<int> TaskExample()
        {
            Task<int> t = Task<int>.Run(() => 3);
            return await t;
        }

        static async void TaskVoidExample()
        {
            await Task.Run(() => { });
        }


        static async void AsynchAwait()
        {
            Task<string> t = new Task<string>(() => AsynkOperation());
            t.Start();
            string res = await t;
            Console.WriteLine(res);
        }

        private static string AsynkOperation()
        {
            Thread.Sleep(10000);
            return "qwe";
        }

    }
}
