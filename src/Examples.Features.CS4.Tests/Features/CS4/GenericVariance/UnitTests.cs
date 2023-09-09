using System;
using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using Xunit;

#pragma warning disable IDE0059 //Unnecessary assignment of a value to 'covariantList'

// for C# 4.0

namespace Examples.Features.CS4.GenericVariance
{
    /// <summary>
    /// Tests for C# 4.0, Generic Co- and Contra- variance.
    /// </summary>
    public class UnitTests
    {

        [Fact]
        public void WhenUsingCovarianceType_WithGenericInterfaces()
        {
            // define : IEnumerable<out T>.
            IEnumerable<Derived> derivedList = new List<Derived>() { new Derived() };

            // OK covariance: IEnumerable<Derived> => IEnumerable<out Base>.
            IEnumerable<Base> covariantList = derivedList;

            // OK covariance: IEnumerable<Derived> => IEnumerable<out Base>.
            Func<IEnumerable<Base>, Base> doFunc = (parameter) => parameter.FirstOrDefault();
            Base resultValue = doFunc(derivedList);
            resultValue.IsInstanceOf<Derived>();

            // define : ICovariant<out T1, T2>
            ICovariant<Derived, Derived> derivedInstance = new MyCovariant<Derived, Derived>();

            // OK covariance: ICovariant<Derived, Derived> => ICovariant<out Base, Derived>.
            ICovariant<Base, Derived> validConvariantInstance = derivedInstance;

            // error CS0266: Cannot implicitly convert type ...
            // ICovariant<Base, Base> invalidConvariantInstance = derivedInstance;

            return;
        }

        [Fact]
        public void WhenUsingContravarianceType_WithGenericInterfaces()
        {
            // define : IComparer<in T>.
            IComparer<Base> baseComparer = new MyComparer<Base>();

            // OK contravariance: IComparer<Base> => IComparer<in Derived>.
            IComparer<Derived> contravariantCompare = baseComparer;

            // OK contravariance: IEnumerable<Base> => IComparer<in Derived>.
            Func<IComparer<Derived>, int> doFunc = (parameter) =>
            {
                var x = new Derived();
                var y = new Derived();
                return parameter.Compare(x, y);
            };
            int resultValue = doFunc(baseComparer);
            resultValue.Is(-1);

            // define : IContravariant<in T1, T2>
            IContravariant<Base, Base> baseInstance = new MyContravariant<Base, Base>();

            // OK contravariance: IContravariant<Base, Base> => IContravariant<in Derived, Base>.
            IContravariant<Derived, Base> validcontravariantInstance = baseInstance;

            // error CS0266: Cannot implicitly convert type ...
            // IContravariant<Derived, Derived> invalidcontravariantInstance = baseInstance;

            return;
        }

        [Fact]
        public void WhenUsingVarianceType_WithGenericDelegates()
        {
            // define: Func<in T, out TResult>(T arg);
            Func<Base, Derived> funcBase = x => x as Derived ?? new Derived();

            // OK variance: Func<Base, Derived> => Func<in Base, out Base>.
            Func<Base, Base> func1 = funcBase;
            func1(new Base()).IsInstanceOf<Derived>();
            func1(new Derived()).IsInstanceOf<Derived>();

            // OK variance: Func<Base, Derived> => Func<in Derived, out Derived>.
            Func<Derived, Derived> func2 = funcBase;
            func2(new Derived()).IsInstanceOf<Derived>();

            // OK variance: Func<Base, Derived> => Func<in Derived, out Base>.
            Func<Derived, Base> func3 = funcBase;
            func3(new Derived()).IsInstanceOf<Derived>();

            // define: Func<in T, out TResult>(T arg);
            Func<Derived, Base> funcInvaliant = x => x;

            // error CS0266: Cannot implicitly convert type ...
            // Func<Base, Base>  funcInvalid1 = funcInvaliant;
            // Func<Base, Derived>  funcInvalid2 = funcInvaliant;
            // Func<Derived, Derived>  funcInvalid3 = funcInvaliant;

            return;
        }


    }

}

