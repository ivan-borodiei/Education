using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SynchroThreading
{
    interface ISinchroThreading
    {
        void Enter();
        void Exit();
    }

    class SinchroEvent : ISinchroThreading, IDisposable
    {
        AutoResetEvent marker = new AutoResetEvent(true);

        public void Enter()
        {
            marker.WaitOne();
        }

        public void Exit()
        {
            marker.Set();
        }

        public void Dispose()
        {
            marker.Dispose();
        }
    }
}
