using System;
using System.Collections.Generic;
using Examples.Features.CSharp40.Tests.GenericVariance.Fixtures;
using Xunit;

namespace Examples.Features.CSharp40.Tests.GenericVariance
{
    /// <summary>
    /// Tests for Contravariance of generics in C# 4,0.
    /// </summary>
    /// <remarks>
    /// Contravariance uses the `in` modifier.
    /// </remarks>
    public class GenericContravarianceTests
    {
        [Fact]
        public void When_UsingGenericInterface_WithContravariantTypeParameters_Then_CanBeAssignedLessDerivedTypes()
        {
            // Definition: `public interface IComparer<in T>`

            IComparer<Base> basedComparer = new MyComparer<Base>();
            IComparer<Derived> derivedComparer = new MyComparer<Derived>();

            // OK: IComparer<Base> to IComparer<Base>
            {
                IComparer<Base> assigned = basedComparer;
                Assert.Equal(0, assigned.Compare(new Base(), new Base()));
            }

            // OK: IComparer<Derived> to IComparer<Derived>
            {
                IComparer<Derived> assigned = derivedComparer;
                Assert.Equal(0, assigned.Compare(new Derived(), new Derived()));
            }

            // OK: IComparer<Base> to IComparer<Derived>
            {
                IComparer<Derived> contravariantAssigned = basedComparer;
                Assert.Equal(0, contravariantAssigned.Compare(new Derived(), new Derived()));
            }

            // NG: IComparer<Derived> to IComparer<Base>
            {
                // error CS0266: Cannot implicitly convert type 'IComparer<Derived>' to 'IComparer<Base>'. An explicit conversion exists (are you missing a cast?)
                // IComparer<Base> invalid = derivedComparer;
            }
        }

        [Fact]
        public void When_UsingGenericInterface_WithInvariantTypeParameters_Then_CanBeAssignedOnlySpecifiedType()
        {
            // Definition: `public interface IInvariantComparer<T> : IComparer<T>`

            IInvariantComparer<Base> basedComparer = new MyComparer<Base>();
            IInvariantComparer<Derived> derivedComparer = new MyComparer<Derived>();

            // OK: IInvariantComparer<Base> to IInvariantComparer<Base>
            {
                IInvariantComparer<Base> assigned = basedComparer;
                Assert.Equal(0, assigned.Compare(new Base(), new Base()));
            }

            // OK: IInvariantComparer<Derived> to IComparer<Derived>
            {
                IInvariantComparer<Derived> assigned = derivedComparer;
                Assert.Equal(0, assigned.Compare(new Derived(), new Derived()));
            }

            // NG: IInvariantComparer<Base> to IInvariantComparer<Derived>
            {
                // error CS0266: Cannot implicitly convert type 'IInvariantComparer<Base>' to 'IInvariantComparer<Derived>'. An explicit conversion exists (are you missing a cast?)
                // IInvariantComparer<Derived> invalidAssigned = basedComparer;
            }

            // NG: IInvariantComparer<Derived> to IInvariantComparer<Base>
            {
                // error CS0266: Cannot implicitly convert type 'IInvariantComparer<Derived>' to 'IInvariantComparer<Base>'. An explicit conversion exists (are you missing a cast?)
                // IInvariantComparer<Base> invalidAssigned = derivedComparer;
            }
        }

        [Fact]
        public void When_UsingGenericDelegate_WithContravariantTypeParameters_Then_CanBeAssignedLessDerivedTypes()
        {
            // Definition: `public delegate void Action<in T>(T obj) `

            Action<Base> baseAction = x => { };
            Action<Derived> derivedAction = x => { };

            // OK: Action<Base> to Action<Base>
            {
                Action<Base> assigned = baseAction;
                assigned(new Base());
            }

            // OK: Action<Derived> to Action<Derived>
            {
                Action<Derived> assigned = derivedAction;
                assigned(new Derived());
            }

            // OK: Action<Base> to Action<Derived>
            {
                Action<Derived> assigned = baseAction;
                assigned(new Derived());
            }

            // NG: Action<Derived> to Action<Base>
            {
                // error CS0266: Cannot implicitly convert type 'Action<Derived>' to 'Action<Base>'. An explicit conversion exists (are you missing a cast?)
                // Action<Base> assigned = derivedAction;
            }
        }

        [Fact]
        public void When_ImplementingGenericInterfaces_WithCovariantTypeParameters_Then_CanUseAsParameters()
        {
            IContravariance<Base> based = new MyContravariance<Base>();
            IContravariance<Derived> assigned = based;
            assigned.SetValue(new Derived());
        }

        public interface IContravariance<in T>
        {
            // error CS1961: Invalid variance: The type parameter 'T' must be covariantly valid on 'IContravariance<T>.GetValue()'. 'T' is contravariant.
            // T GetValue();

            // A contravariance type parameters can be used as parameter types.
            void SetValue(T value);

        }

        public class MyContravariance<T> : IContravariance<T>
        {
            public void SetValue(T value)
            {
            }
        }

        public interface IInvariantComparer<T> : IComparer<T>
        {
            new int Compare(T x, T y);
        }

        public class MyComparer<T> : IComparer<T>, IInvariantComparer<T>
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
}
