using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Multithreading
{
    class Progress
    {
        private int counter;
        private bool isStoped;        

        public event Action<int> ChangeProgress;
        public event Action<bool> ResultInfo;
        private SynchronizationContext context;

        public Progress(SynchronizationContext context)
        {
            this.context = context;
        }

        private void Reset()
        {
            counter = 0;
            isStoped = false;            
        }

        public void Start()
        {
            Reset();
            while (counter < 100)
            {
                if (isStoped)
                    break;
                counter++;
                Thread.Sleep(30);                
                //ChangeProgress(counter);
                context.Send(x => { ChangeProgress((int)x); }, counter);
            }

            //ResultInfo(isStoped);
            context.Send(x => { ResultInfo((bool)x); }, isStoped);
        }

        public void Stop()
        {
            isStoped = true;
        }



        public void StartWithCancellation(CancellationToken token)
        {
            Reset();
            while (counter < 100)
            {
                if (token.IsCancellationRequested)
                    break;
                counter++;
                Thread.Sleep(30);
                //ChangeProgress(counter);
                context.Send(x => { ChangeProgress((int)x); }, counter);
            }

            //ResultInfo(isStoped);
            context.Send(x => { ResultInfo((bool)x); }, isStoped);
        }



        public int StartWithTask(CancellationToken token)
        {            
            Reset();
            while (counter < 100)
            {
                //if (token.IsCancellationRequested)
                    //break;                
                token.ThrowIfCancellationRequested();

                //if (counter == 50)
                //    throw new Exception("yep;");

                counter++;
                Thread.Sleep(30);
                //ChangeProgress(counter);
                context.Send(x => { ChangeProgress((int)x); }, counter);
            }

            //ResultInfo(isStoped);
            context.Send(x => { ResultInfo((bool)x); }, isStoped);

            return 100;
        }

    }
}
