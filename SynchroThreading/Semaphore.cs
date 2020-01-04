using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SynchroThreading
{
    class SinchroSemaphore: ISinchroThreading, IDisposable
    {
        Semaphore marker = new Semaphore(2,3);

        public void Enter()
        {
            marker.WaitOne();
        }

        public void Exit()
        {
            marker.Release();
        }

        public void Dispose()
        {
            marker.Dispose();
        }

    }
}
