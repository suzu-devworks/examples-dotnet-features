using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Examples.Features.CSharp40.Tests.DynamicBinding.Fixtures;
using Microsoft.CSharp.RuntimeBinder;
using Xunit;

namespace Examples.Features.CSharp40.Tests.DynamicBinding
{
    /// <summary>
    /// Tests for Dynamic binding in C# 4,0.
    /// </summary>
    public class DynamicBindingTests
    {
        [Fact]
        public void When_UsingDynamicForLateBinding_Then_CanCallUnknownMembers()
        {
            // The compiler does not check for the existence of the method
            // and looks for it by name at run time.

            // Loading Dummy DLL.
            Type dynamicType = Assembly.GetExecutingAssembly()
                .GetType(typeof(Calculator).FullName);

            // calc is dynamic.
            dynamic calc = Activator.CreateInstance(dynamicType);

            // Invoke unknown(exists) member.
            {
                var result = calc.Add(1, 2);
                Assert.IsType<int>(result);
                Assert.Equal(3, result);
            }

            {
                var result = calc.Sub(10, 4);
                Assert.IsType<int>(result);
                Assert.Equal(6, result);
            }

            {
                var result = calc.Mul(7, 8);
                Assert.IsType<int>(result);
                Assert.Equal(56, result);
            }

            {
                var result = calc.Div(20, 6);
                Assert.IsType<int>(result);
                Assert.Equal(3, result);
            }
        }

        [Fact]
        public void When_UsingDynamicForLateBinding_WithNotExists_ThrowException()
        {
            // Loading Dummy DLL.
            Type dynamicType = Assembly.GetExecutingAssembly()
                .GetType(typeof(Calculator).FullName);

            // calc is dynamic.
            dynamic calc = Activator.CreateInstance(dynamicType);

            // Invoke unknown(not exists) member.
            Assert.Throws<RuntimeBinderException>(() => calc.NonExistentMethod());
        }

        [Fact]
        public void When_UsingDynamicForDuckTyping_Then_CanBeHandledByBehavior()
        {
            // "If it walks like a duck and quacks like a duck, it must be a duck"

            Func<dynamic, IRoad, int> walk = (duck, road) =>
            {
                int minutes = duck.MoveLegs(road);
                return minutes;
            };

            var sidewalk = new Sidewalk(100);

            {
                var duck = new DuckAnimal();
                var actual = walk(duck, sidewalk);

                Assert.Equal(2, actual);
            }

            {
                var duck = new DuckToys();
                var actual = walk(duck, sidewalk);

                Assert.Equal(7, actual);
            }
        }

        [Fact]
        public void When_UsingDynamicForGenericStaticMethod_Then_CanBeUsingOperators()
        {
            // C# generic type cannot call static methods. That is, operators are not allowed.

            // Once cast to dynamic, the performance will be significantly worse,
            // but it can be achieved.

            {
                var result = Sum(Enumerable.Range(1, 10));
                Assert.Equal(55, result);
            }

            {
                var result = Sum(new[] { "A", "b", "c ", "d", "E" });
                Assert.Equal("Abc dE", result);
            }
        }

        private static T Sum<T>(IEnumerable<T> list)
        {
            T sum = default(T);

            foreach (var x in list)
            {
                // I don't know if + operator can be used for T.
                // sum += x;

                // via dynamic. but it seems to be late.
                sum += (dynamic)x;
            }

            return sum;
        }

        [Fact]
        public void When_UsingDynamicForMultipleDispatch_Then_CanBeSwitchingMethodsBasedOnTypes()
        {
            // Switching the method that is actually called based on the dynamic type information
            // of multiple instances is called "Multiple dispatch".

            var roadway = new Roadway(200);
            var sidewalk = new Sidewalk(100);

            // Dispatcher with if statement
            Assert.Equal(400, Dispatcher.Dispatch(roadway, roadway));
            Assert.Equal(100, Dispatcher.Dispatch(roadway, sidewalk));
            Assert.Equal(101, Dispatcher.Dispatch(sidewalk, roadway));
            Assert.Equal(-200, Dispatcher.Dispatch(sidewalk, sidewalk));

            // Dynamic Dispatcher
            Assert.Equal(400, DynamicDispatch(roadway, roadway));
            Assert.Equal(100, DynamicDispatch(roadway, sidewalk));
            Assert.Equal(101, DynamicDispatch(sidewalk, roadway));
            Assert.Equal(-200, DynamicDispatch(sidewalk, sidewalk));
        }

        private static int DynamicDispatch(IRoad x, IRoad y)
        {
            return new RoadCalculator().Sum((dynamic)x, (dynamic)y);
        }

    }
}
