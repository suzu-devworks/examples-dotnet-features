using System;
using System.Collections.Generic;
using ChainingAssertion;
using Examples.Features.CS40.GenericVariance.Fixtures;
using Xunit;

namespace Examples.Features.CS40.GenericVariance
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
        public void WhenUsingGenericInterface()
        {
            // define :
            //  interface System.Collections.Generic.IComparer<in T>

            // NG: IComparer<Derived> => IComparer<Base>

            // error CS0266: Cannot implicitly convert type 'IComparer<Derived>' to 'IComparer<Base>'. An explicit conversion exists (are you missing a cast?)
            // IComparer<Derived> derivedComparer = new MyComparer<Derived>();
            // IComparer<Base> invalidCompare = derivedComparer;

            // error CS1503: Argument 1: cannot convert from 'IComparer<Derived>' to 'IComparer<Base>'
            // Func<IComparer<Base>, int> doBaseFunc = (comp) => comp.Compare(new Base(), new Base());
            // int invalidResult = doBaseFunc(derivedComparer);

            // OK: IComparer<Base> => IComparer<Derived>

            IComparer<Based> basedComparer = new MyComparer<Based>();
            IComparer<Derived> contravarianceCompare = basedComparer;

            contravarianceCompare.IsInstanceOf<IComparer<Based>>();

            Func<IComparer<Derived>, int> doDerivedFunc = (comp) => comp.Compare(new Derived(), new Derived());
            int contravarianceResult = doDerivedFunc(basedComparer);

            contravarianceResult.Is(0);

            return;
        }

        [Fact]
        public void WhenUsingGenericClass_IsInvariance()
        {
            // define :
            //  class System.Collections.Generic.List<T>

            // NG: MyComparer<Base> => MyComparer<Derived>

            MyComparer<Based> basedComparer = new MyComparer<Based>();

            // error CS0029: Cannot implicitly convert type 'MyComparer<Based>' to 'MyComparer<Derived>'
            // MyComparer<Derived> invalidCompare = basedComparer;
            IComparer<Derived> contravarianceCompare = basedComparer;
            contravarianceCompare.IsInstanceOf<MyComparer<Based>>();

            // error CS1503: Argument 1: cannot convert from 'MyComparer<Based>' to 'MyComparer<Derived>'
            // Func<MyComparer<Derived>, int> doDerivedFunc = (comp) => comp.Compare(new Derived(), new Derived());
            // int invalidResult = doDerivedFunc(basedComparer);

            return;
        }

        [Fact]
        public void WhenGenericDelegate()
        {
            // define :
            //  delegate void System.Action<in T>(T obj)

            // NG: Action<Derived> => Action<Base>

            // error CS0266: Cannot implicitly convert type 'Action<Derived>' to 'Action<Base>'. An explicit conversion exists (are you missing a cast?)
            // object actualDerived = null;
            // Action<Derived> doDerivedAction = param => { actualDerived = param; };
            // Action<Base> invalidAction = doDerivedAction;

            // OK: Action<Base> => Action<Derived>

            object actualBased = null;
            Action<Based> doBasedAction = param => { actualBased = param; };
            Action<Derived> doContravarianceAction = doBasedAction;
            doContravarianceAction(new Derived());

            actualBased.IsInstanceOf<Derived>();

            return;
        }

        [Fact]
        public void WhenUsingUserDefinedInterface()
        {
            IContravariance<Based> based = new MyContravariance<Based>();

            IContravariance<Derived> contravariance = based;

            contravariance.IsInstanceOf<IContravariance<Based>>();

            return;
        }

        public interface IContravariance<in T>
        {
            // error CS1961: Invalid variance: The type parameter 'T' must be covariantly valid on 'GenericContravarianceTests.IContravariance<T>.Func()'. 'T' is contravariant.
            // T Func();

            // A contravariance type parameters can be used as parameter types.
            void Act(T param);

        }

        public class MyContravariance<T> : IContravariance<T>
        {
            public void Act(T param)
            {
                throw new NotImplementedException();
            }
        }

    }
}
