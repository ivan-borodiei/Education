using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;


namespace Multithreading
{
    public partial class ThreadForm : Form
    {
        private Progress progress;
        private CancellationTokenSource source;
        private SynchronizationContext context;

        public ThreadForm()
        {
            InitializeComponent();
            
            //StopWithFlag
            //btnStart.Click += btnStart_Click;
            //btnStop.Click += btnStop_Click;

            //StopWithCancellationToken
            //btnStart.Click += btnStartWithCancellationToken_Click;
            //btnStop.Click += btnStopWithCancellationToken_Click;

            //StartWithTask
            //btnStart.Click += btnStartWithTask_Click;
            //btnStop.Click += btnStopWithTask_Click;

            //StartWithAsyncAwait
            btnStart.Click += btnStartWithAsyncAwait_Click;
            btnStop.Click += btnStopWithAsyncAwait_Click;

            
            Load += ChangeClock;
            
            context = SynchronizationContext.Current;
        }

        private void ChangeClock(object sender, EventArgs e)
        {
            Action a = () =>
            {
                while (true)
                {
                    lblClock.Invoke((Action)(() => lblClock.Text = DateTime.Now.ToString()));
                    Thread.Sleep(500);
                };
            };

            Task.Factory.StartNew(a);
        }

        private void ResultInfo(bool res)
        {
            MessageBox.Show(String.Format("Process was {0}!", res ? "stopped" : "compleated"));
        }

        private void ChangeProgress(int counter)
        {
            Action<int> refreshProgress = (x) => 
            { 
                progressBar1.Value = x == 100 ? 100 : x + 1; progressBar1.Value = x;
                this.Text = String.Format("ThreadId = {0}", Thread.CurrentThread.ManagedThreadId);
            };
            refreshProgress(counter);
            //Invoke(refreshProgress, counter);                        
        }




        private void btnStart_Click(object sender, EventArgs e)
        {
            progress = new Progress(context);
            progress.ChangeProgress += ChangeProgress;
            progress.ResultInfo += ResultInfo;
            ThreadPool.QueueUserWorkItem((x) => progress.Start());
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            progress.Stop();
        }




        private void btnStartWithCancellationToken_Click(object sender, EventArgs e)
        {
            source = new CancellationTokenSource();
            progress = new Progress(context);
            progress.ChangeProgress += ChangeProgress;
            progress.ResultInfo += ResultInfo;
            ThreadPool.QueueUserWorkItem((x) => progress.StartWithCancellation(source.Token));
        }

        private void btnStopWithCancellationToken_Click(object sender, EventArgs e)
        {
            source.Cancel();            
        }



        private void btnStartWithTask_Click(object sender, EventArgs e)
        {
            this.Text = String.Format("ThreadId = {0}", Thread.CurrentThread.ManagedThreadId);

            source = new CancellationTokenSource();
            progress = new Progress(context);
            progress.ChangeProgress += ChangeProgress;
            progress.ResultInfo += ResultInfo;
            Task<int> task = new Task<int>(() => progress.StartWithTask(source.Token), source.Token);
            task.Start();
            task.ContinueWith((x) => {this.Text = String.Format("ThreadId = {0}, Result = {1}", Thread.CurrentThread.ManagedThreadId, task.Result.ToString()); }, TaskScheduler.FromCurrentSynchronizationContext());
            //task.ContinueWith((x) => { context.Send((y) => this.Text = String.Format("ThreadId = {0}, Result = {1}", Thread.CurrentThread.ManagedThreadId, task.Result.ToString()), null); });
        }

        private void btnStopWithTask_Click(object sender, EventArgs e)
        {            
            source.Cancel();            
        }



        private async void btnStartWithAsyncAwait_Click(object sender, EventArgs e)
        {
            this.Text = String.Format("ThreadId = {0}", Thread.CurrentThread.ManagedThreadId);

            source = new CancellationTokenSource();
            progress = new Progress(context);
            progress.ChangeProgress += ChangeProgress;
            progress.ResultInfo += ResultInfo;

            try
            {                
                int rez = await Task<int>.Run(() => progress.StartWithTask(source.Token), source.Token);
                this.Text = String.Format("ThreadId = {0}, Result = {1}", Thread.CurrentThread.ManagedThreadId, rez.ToString());
                
            }
            catch(OperationCanceledException ex)
            {
                this.Text = ex.Message;
            }
            catch(Exception exGen)
            {

            }
            
                                   
        }

        private void btnStopWithAsyncAwait_Click(object sender, EventArgs e)
        {
            source.Cancel();
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            
            //AsyncAwait.Text = await Met();

            //AsyncAwait.Text = await Task.Run(() => { Thread.Sleep(3000); return "Done"; });

            var t = MetAsync();
        }

        static Task<string> Met()
        {
            return Task.Run(() => { Thread.Sleep(3000); return "Done"; });
        }

        async Task MetAsync()
        {
            await Task.Run(() => { Thread.Sleep(5000); });
        }


    }
}
