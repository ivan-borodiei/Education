using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class CustomSort : IComparer<CustomSort>, IComparable<CustomSort>
    {
        public int A { get; set; }

        public int Compare(CustomSort x, CustomSort y)
        {
            return x.A - y.A;
        }

        public int CompareTo(CustomSort other)
        {
            return this.A - other.A;
        }
    }
}
