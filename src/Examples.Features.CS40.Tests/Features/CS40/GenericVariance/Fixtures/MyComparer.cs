using System.Collections.Generic;

namespace Examples.Features.CS40.GenericVariance.Fixtures
{
    public class MyComparer<T> : IComparer<T>
    {
        public int Compare(T x, T y)
        {
            if ((x == null) && (y == null)) { return 0; }
            if (x == null) { return -1; }
            if (y == null) { return 1; }
            if (ReferenceEquals(x, y)) { return 0; }

            return 0;
        }
    }

}
