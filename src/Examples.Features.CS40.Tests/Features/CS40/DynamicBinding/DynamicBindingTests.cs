using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ChainingAssertion;
using Examples.Features.CS40.DynamicBinding.Fixtures;
using Microsoft.CSharp.RuntimeBinder;
using Xunit;

namespace Examples.Features.CS40.DynamicBinding
{
    /// <summary>
    /// Tests for Dynamic binding in C# 4,0.
    /// </summary>
    public class DynamicBindingTests
    {
        [Fact]
        public void WhenUsedForLateBinding()
        {
            // The compiler does not check for the existence of the method
            // and looks for it by name at run time.

            // Loading Dummy DLL.
            Type dynamicType = Assembly.GetExecutingAssembly()
                .GetType(typeof(Calculator).FullName);

            // calc is dynamic.
            dynamic calc = Activator.CreateInstance(dynamicType);

            // Invoke unknown(exists) member.
            var result1 = calc.Add(1, 2);

            // cast dynamic to object.
            ((object)result1).IsInstanceOf<int>()
                .Is(3);

            ((object)calc.Sub(10, 4)).IsInstanceOf<int>()
                .Is(6);

            ((object)calc.Mul(7, 8)).IsInstanceOf<int>()
                .Is(56);

            ((object)calc.Div(20, 6)).IsInstanceOf<int>()
                .Is(3);

            return;
        }

        [Fact]
        public void WhenUsedForLateBinding_WithNotExists_ThrowException()
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
        public void WhenUsedForDuckTyping()
        {
            // "If it walks like a duck and quacks like a duck, it must be a duck"

            Func<dynamic, IRoad, int> walk = (duck, road) =>
            {
                int minutes = duck.MoveLegs(road);
                return minutes;
            };

            var sidewalk = new Sidewalk(100);

            var duck1 = new DuckAnimal();
            var actual1 = walk(duck1, sidewalk);

            actual1.Is(2);

            var duck2 = new DuckToys();
            var actual2 = walk(duck2, sidewalk);

            actual2.Is(7);

            return;
        }

        [Fact]
        public void WhenCallingStaticMethod_WithGenericType()
        {
            // C# generic type cannot call static methods. That is, operators are not allowed.

            // Once cast to dynamic, the performance will be significantly worse,
            // but it can be achieved.

            Sum(Enumerable.Range(1, 10)).Is(55);

            Sum(new[] { "A", "b", "c ", "d", "E" }).Is("Abc dE");

            return;
        }

        private static T Sum<T>(IEnumerable<T> list)
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

        [Fact]
        public void WhenUsedForMultipleDispatch()
        {
            // Switching the method that is actually called based on the dynamic type information
            // of multiple instances is called "Multiple dispatch".

            var road1 = new Roadway(200);
            var road2 = new Sidewalk(100);

            var original1 = Dispatcher.Dispatch(road1, road1);
            var original2 = Dispatcher.Dispatch(road1, road2);
            var original3 = Dispatcher.Dispatch(road2, road1);
            var original4 = Dispatcher.Dispatch(road2, road2);

            var actual1 = DynamicDispatch(road1, road1);
            var actual2 = DynamicDispatch(road1, road2);
            var actual3 = DynamicDispatch(road2, road1);
            var actual4 = DynamicDispatch(road2, road2);

            actual1.Is(x => x == original1 && x == 400);
            actual2.Is(x => x == original2 && x == 100);
            actual3.Is(x => x == original3 && x == 101);
            actual4.Is(x => x == original4 && x == -200);

            return;
        }

        private static int DynamicDispatch(IRoad x, IRoad y)
        {
            return new RoadCalculator().Sum((dynamic)x, (dynamic)y);
        }

    }

}
