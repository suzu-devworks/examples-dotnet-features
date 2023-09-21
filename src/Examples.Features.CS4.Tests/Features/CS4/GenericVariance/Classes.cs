using System;
using System.Collections.Generic;

namespace Examples.Features.CS4.GenericVariance
{

    public class Base { }

    public class Derived : Base { }

    public interface ICovariant<out T1, T2>
    {
        // A covariant type parameter can be used as the return type of a delegate.
        T1 Get(T2 param);
    }

    public class MyCovariant<T1, T2> : ICovariant<T1, T2>
    {
        public T1 Get(T2 param)
        {
            throw new NotImplementedException();
        }
    }

    public interface IContravariant<in T1, T2>
    {
        // A contravariant type parameters can be used as parameter types.
        T2 Do(T1 param);
    }

    public class MyContravariant<T1, T2> : IContravariant<T1, T2>
    {
        public T2 Do(T1 param)
        {
            throw new NotImplementedException();
        }
    }

    public class MyComparer<T> : IComparer<T>
    {
        public int Compare(T x, T y)
        {
            return -1;
        }
    }

}
