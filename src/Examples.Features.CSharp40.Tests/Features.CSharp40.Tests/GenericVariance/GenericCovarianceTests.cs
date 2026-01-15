using System;
using System.Collections.Generic;
using Examples.Features.CSharp40.Tests.GenericVariance.Fixtures;
using Xunit;

namespace Examples.Features.CSharp40.Tests.GenericVariance
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
        public void When_UsingGenericInterface_WithCovariantTypeParameters_Then_CanBeAssignedMoreDerivedTypes()
        {
            // Definition: `public interface IEnumerable<out T>`

            IEnumerable<Base> basedList = new List<Base>() { new Base() };
            IEnumerable<Derived> derivedList = new List<Derived>() { new Derived() };

            // OK: IEnumerable<Base> to IEnumerable<Base>
            {
                IEnumerable<Base> assigned = basedList;
                Assert.IsType<IEnumerable<Base>>(assigned, exactMatch: false);
            }

            // OK: IEnumerable<Derived> to IEnumerable<Derived>
            {
                IEnumerable<Derived> assigned = derivedList;
                Assert.IsType<IEnumerable<Derived>>(assigned, exactMatch: false);
            }

            // OK: IEnumerable<Derived> to IEnumerable<Base>
            {
                IEnumerable<Base> covarianceAssigned = derivedList;
                Assert.IsType<IEnumerable<Derived>>(covarianceAssigned, exactMatch: false);
            }

            // NG: IEnumerable<Base> to IEnumerable<Derived>
            {
                // error CS0266: Cannot implicitly convert type 'IComparer<Base>' to 'IComparer<Derived>'. An explicit conversion exists (are you missing a cast?)
                // IEnumerable<Derived> invalid = basedList;
            }
        }

        [Fact]
        public void When_UsingGenericInterface_WithInvariantTypeParameters_Then_CanBeAssignedOnlySpecifiedType()
        {
            // Definition: `public interface IList<T>`

            IList<Base> basedList = new List<Base>() { new Base() };
            IList<Derived> derivedList = new List<Derived>() { new Derived() };

            // OK: IList<Base> to IList<Base>
            {
                IList<Base> assigned = basedList;
                Assert.IsType<IList<Base>>(assigned, exactMatch: false);
            }

            // OK: IList<Derived> => IList<Derived>
            {
                IList<Derived> assigned = derivedList;
                Assert.IsType<IList<Derived>>(assigned, exactMatch: false);
            }

            // NG: IList<Derived> => IList<Base>
            {
                // error CS0266: Cannot implicitly convert type 'IList<Derived>' to 'IList<Base>'. An explicit conversion exists (are you missing a cast?)
                // IList<Base> invalid = derivedList;
            }

            // NG: IList<Base> => IList<Derived>
            {
                // error CS0266: Cannot implicitly convert type 'IList<Base>' to 'IList<Derived>'. An explicit conversion exists (are you missing a cast?)
                // IList<Derived> invalid = basedList;
            }
        }

        [Fact]
        public void When_UsingGenericDelegate_WithCovariantTypeParameters_Then_CanBeAssignedMoreDerivedTypes()
        {
            // Definition: `public delegate TResult Func<in T,out TResult>(T arg)`

            Func<Base> doBaseFunc = () => new Base();
            Func<Derived> doDerivedFunc = () => new Derived();

            // OK: Func<Base> to Func<Base>
            {
                Func<Base> assignedFunc = doBaseFunc;
                Assert.IsType<Base>(assignedFunc());
            }

            // OK: Func<Derived> to Func<Derived>
            {
                Func<Derived> assignedFunc = doDerivedFunc;
                Assert.IsType<Derived>(assignedFunc());
            }

            // OK: Func<Derived> to Func<Base>
            {
                Func<Base> covarianceAssigned = doDerivedFunc;
                Assert.IsType<Derived>(covarianceAssigned());
            }

            // NG: Func<Base> to Func<Derived>
            {
                // error CS0266: Cannot implicitly convert type 'IList<Base>' to 'IList<Derived>'. An explicit conversion exists (are you missing a cast?)
                // Func<Derived> invalid = doBaseFunc;
            }
        }

        [Fact]
        public void When_ImplementingGenericInterfaces_WithCovariantTypeParameters_Then_CanUseAsReturnValue()
        {
            IMyCovariance<Derived> derived = new MyCovariance<Derived>();
            IMyCovariance<Base> assigned = derived;
            Assert.IsType<Derived>(assigned.GetValue(), exactMatch: false);
        }

        public interface IMyCovariance<out T>
        {
            // A covariance type parameter can be used as the return type of a delegate.
            T GetValue();

            // error CS1961: Invalid variance: The type parameter 'T' must be contravariantly valid on 'IMyCovariance<T>.SetValue(T)'. 'T' is covariant.
            // void SetValue(T value);
        }

        public class MyCovariance<T> : IMyCovariance<T> where T : new()
        {
            public T GetValue()
            {
                return new T();
            }
        }

    }
}
