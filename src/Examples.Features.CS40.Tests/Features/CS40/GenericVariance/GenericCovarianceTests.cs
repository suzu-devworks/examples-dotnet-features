using System;
using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS40.GenericVariance
{
    /// <summary>
    /// Tests for Covariance of generics in C# 4,0.
    /// </summary>
    /// <remarks>
    /// Covariance uses the `out` modifier.
    /// </remarks>
    public class GenericCovarianceTests
    {
        [Fact]
        public void WhenUsingGenericInterface()
        {
            // define :
            //  interface System.Collections.Generic.IEnumerable<out T>

            // OK: IEnumerable<Derived> => IEnumerable<Based>

            IEnumerable<Derived> derivedList = new List<Derived>() { new Derived() };
            IEnumerable<Based> covarianceAssign = derivedList;

            covarianceAssign.IsInstanceOf<IEnumerable<Derived>>();

            Func<IEnumerable<Based>, Based> doBasedFunc = (p) => p.FirstOrDefault();
            Based covarianceResult = doBasedFunc(derivedList);

            covarianceResult.IsInstanceOf<Derived>();

            // NG: IEnumerable<Based> => IEnumerable<Derived>

            // error CS0266: Cannot implicitly convert type 'IEnumerable<Based>' to 'IEnumerable<Derived>'. An explicit conversion exists (are you missing a cast?)
            // IEnumerable<Based> basedList = new List<Based>() { new Derived() };
            // IEnumerable<Derived> invalidList = basedList;

            // error CS1503: Argument '1' cannot convert from 'IEnumerable<Based>' to 'IEnumerable<Derived>'.
            // Func<IEnumerable<Derived>, Derived> doDerivedFunc = (p) => p.FirstOrDefault();
            // Derived invalidResult = doDerivedFunc(basedList);

            return;
        }

        [Fact]
        public void WhenUsingGenericClass_IsInvariance()
        {
            // define :
            //  class System.Collections.Generic.List<T>

            // NG: List<Based> => List<Derived>

            List<Derived> derivedList = new List<Derived>() { new Derived() };

            // error CS0029: Cannot implicitly convert type 'List<Derived>' to 'List<Based>'
            // List<Based> invalidAssign = derivedList;

            IEnumerable<Based> covarianceAssign = derivedList;
            covarianceAssign.IsInstanceOf<List<Derived>>();

            // error CS1503: Argument 1: cannot convert from 'List<Derived>' to 'List<Based>'
            // Func<List<Based>, Based> doBasedFunc = (p) => p.FirstOrDefault();
            // Based invalidResult = doBasedFunc(derivedList);

            return;
        }

        [Fact]
        public void WhenGenericDelegate()
        {
            // define :
            //  delegate TResult System.Func<out TResult>()

            // OK: Func<Derived> => Func<Based>

            Func<Derived> doDerivedFunc = () => new Derived();
            Func<Based> doCovarianceFunc = doDerivedFunc;

            Based actual = doCovarianceFunc();
            actual.IsInstanceOf<Derived>();

            // NG: Func<Based> => Func<Derived>

            // error CS0266: Cannot implicitly convert type 'Func<Based>' to 'Func<Derived>'. An explicit conversion exists (are you missing a cast?)
            // Func<Based> doBasedFunc = () => new Derived();
            // Func<Derived> doInvalidFunc = doBasedFunc;

            return;
        }

        [Fact]
        public void WhenUsingUserDefinedInterface()
        {
            ICovariance<Derived> derived = new MyCovariance<Derived>();

            ICovariance<Based> covariance = derived;

            covariance.IsInstanceOf<ICovariance<Derived>>();

            return;
        }

        public interface ICovariance<out T>
        {
            // A covariance type parameter can be used as the return type of a delegate.
            T Get();

            // error CS1961: Invalid variance: The type parameter 'T' must be contravariantly valid on 'GenericVarianceTests.ICovariance<T>.Set(T)'. 'T' is covariant.
            // void Set(T value);
        }

        public class MyCovariance<T> : ICovariance<T>
        {
            public T Get()
            {
                throw new NotImplementedException();
            }
        }

    }
}
