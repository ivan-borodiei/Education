using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SynchroThreading
{
    class HybridSynchro: ISinchroThreading, IDisposable
    {
        private int m = 0;
        private AutoResetEvent marker = new AutoResetEvent(false);

        public void Enter()
        {
            if (Interlocked.Increment(ref m) == 1)
                return;
            marker.WaitOne();
        }

        public void Exit()
        {
            if (Interlocked.Decrement(ref m) == 0)
                return;
            marker.Set();
        }

        public void Dispose()
        {            
            marker.Dispose();
        }
    }
}
