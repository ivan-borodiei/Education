using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SynchroThreading
{
    struct SinchroSpinLock : ISinchroThreading
    {
        private int marker;
        
        public void Enter()
        {
           while (true)
           {
               if (Interlocked.Exchange(ref marker, 1) == 0)
                   return;
           }
        }

        public void Exit()
        {
            Volatile.Write(ref marker, 0);
        }

    }
}
