using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ChainingAssertion;
using Microsoft.CSharp.RuntimeBinder;
using Xunit;

// for C# 4.0

namespace Examples.Features.CS4.DynamicBinding
{
    /// <summary>
    /// Tests for C# 4.0, Dynamic binding.
    /// </summary>
    /// <seealso cref="https://ufcpp.net/study/csharp/sp4_dynamic.html"/>
    public class UnitTests
    {

        [Fact]
        public void CaseForLateBinding()
        {
            // The compiler does not check for the existence of the method and looks for it by name at run time.

            // Loading Dummy DLL.
            Type dynamicType = Assembly.GetExecutingAssembly()
                .GetType(typeof(Calculator).FullName);

            // calc is dynamic.
            dynamic calc = Activator.CreateInstance(dynamicType);

            // Invoke unknown(exists) member.
            var result1 = calc.Add(1, 2);

            // Assert.
            // RuntimeBinderException: ''int' does not contain a definition for 'IsInstanceOf''
            // result1.IsInstanceOf<int>();
            // result1.GetType().Is(typeof(int));
            ((Type)result1.GetType()).Is(typeof(int));
            ((int)result1).Is(3);

            ((int)calc.Sub(10, 4)).Is(6);
            ((int)calc.Mul(7, 8)).Is(56);
            ((int)calc.Div(20, 6)).Is(3);

            return;
        }

        [Fact]
        public void CaseForLateBinding_WithCallingNotExisis_ThrowException()
        {
            // Loading Dummy DLL.
            Type dynamicType = Assembly.GetExecutingAssembly()
                .GetType(typeof(Calculator).FullName);

            // calc is dynamic.
            dynamic calc = Activator.CreateInstance(dynamicType);

            // Invoke unknown(not exists) member.
            Assert.Throws<RuntimeBinderException>(() => calc.NonExistentMethod());

            return;
        }

        [Fact]
        public void CaseForDuckTyping()
        {
            // > "If it walks like a duck and quacks like a duck, it must be a duck"

            Func<dynamic, IRoad, int> walk = (duck, road) =>
            {
                int minutes = duck.MoveLegs(road);
                return minutes;
            };

            var sidewalk = new Sidewalk(100);

            var duck1 = new DuckAnimal();
            walk(duck1, sidewalk).Is(2);

            var duck2 = new DuckToys();
            walk(duck2, sidewalk).Is(7);

            return;
        }

        [Fact]
        public void CaseForStaticMethodCall_WhenUsingGenerics()
        {

            Invoker.Sum(Enumerable.Range(1, 10)).Is(55);
            Invoker.Sum(new[] { "A", "b", "c ", "d", "E" }).Is("Abc dE");

            return;
        }

        private static class Invoker
        {
            public static T Sum<T>(IEnumerable<T> list)
            {
                T sum = default(T);
                dynamic sumX = default(T);

                foreach (var x in list)
                {
                    // I don't know if + operator can be used for T.
                    //sum += x;

                    // via dynamic. but it seems to be late.
                    sumX += x;
                }
                sum += sumX;

                return sum;
            }
        }


        [Fact]
        public void CaseForMultipleDispatch()
        {
            // Switching the method that is actually called based on the dynamic type information
            // of multiple instances is called "Multiple dispatch".

            var road1 = new Roadway(200);
            var road2 = new Sidewalk(100);

            var original1 = Dispatcher.Dispatch(road1, road1);
            var original2 = Dispatcher.Dispatch(road1, road2);
            var original3 = Dispatcher.Dispatch(road2, road1);
            var original4 = Dispatcher.Dispatch(road2, road2);

            var value1 = Dispatcher.DispatchWithDynamic(road1, road1);
            var value2 = Dispatcher.DispatchWithDynamic(road1, road2);
            var value3 = Dispatcher.DispatchWithDynamic(road2, road1);
            var value4 = Dispatcher.DispatchWithDynamic(road2, road2);

            //Assert.
            value1.Is(original1);
            value2.Is(original2);
            value3.Is(original3);
            value4.Is(original4);

            return;
        }

        private static class Dispatcher
        {
            public static int Dispatch(IRoad x, IRoad y)
            {
                // Switch the actual method to call based on the dynamic type information of multiple instances.
                if ((x is Roadway) && (y is Roadway)) { return RoadCalculator.Sum((Roadway)x, (Roadway)y); }
                if ((x is Roadway) && (y is Sidewalk)) { return RoadCalculator.Sum((Roadway)x, (Sidewalk)y); }
                if ((x is Sidewalk) && (y is Roadway)) { return RoadCalculator.Sum((Sidewalk)x, (Roadway)y); }
                if ((x is Sidewalk) && (y is Sidewalk)) { return RoadCalculator.Sum((Sidewalk)x, (Sidewalk)y); }

                throw new NotSupportedException();
            }

            public static int DispatchWithDynamic(IRoad x, IRoad y)
            {
                return RoadCalculator.Sum((dynamic)x, (dynamic)y);
            }

        }

    }

}
