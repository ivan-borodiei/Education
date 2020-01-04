using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SynchroThreading
{
    class SynchroMutex : ISinchroThreading, IDisposable
    {
        Mutex marker = new Mutex();

        public void Enter()
        {
            marker.WaitOne();
        }

        public void Exit()
        {
            marker.ReleaseMutex();
        }

        public void Dispose()
        {
            marker.Dispose();
        }

    }

}
