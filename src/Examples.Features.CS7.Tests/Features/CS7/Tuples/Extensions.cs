using System.Collections.Generic;

namespace Examples.Features.CS7.Tuples
{

    public static class Extensions
    {
        public static void Deconstruct<T, U>(this KeyValuePair<T, U> pair, out T key, out U value)
        {
            key = pair.Key;
            value = pair.Value;
        }
    }

}
