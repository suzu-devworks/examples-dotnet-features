using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Examples.Features.CSharp90.Tests.ModuleInitializers.Fixtures;
using Xunit;

namespace Examples.Features.CSharp90.Tests.ModuleInitializers
{
    /// <summary>
    /// Tests for module initializers in C# 9.0.
    /// </summary>
    public class ModuleInitializersTests
    {
        [Theory]
        [InlineData("TYPE-1", "This Class is DerivedA.")]
        [InlineData("TYPE-2", "DerivedB is this.")]
        public void When_ModuleInitializerUsed_Then_AllowToRunInitializeOnce(string input, string expected)
        {
            var instance = BaseClass.Create(input);

            var message = instance?.GetMessage();

            Assert.NotNull(instance);
            Assert.Equal(expected, message);
        }

        public class DerivedA : BaseClass
        {
            public DerivedA() : base(1, "DerivedA") { }

            public override string GetMessage() => $"This Class is DerivedA.";

            [ModuleInitializer]
            internal static void Init() => Register("TYPE-1", () => new DerivedA());
        }

        public class DerivedB : BaseClass
        {

            public DerivedB() : base(2, "DerivedB") { }

            public override string GetMessage() => $"DerivedB is this.";

            [ModuleInitializer]
            internal static void Init() => Register("TYPE-2", () => new DerivedB());

        }

        [Fact]
        public void When_CheckingInitializationOrder_Then_InitializedOnce()
        {
            _ = new InitializationOrderClass();
            var expectedOrder = new List<string>
            {
                "Declaration",
                "Static Constructor",
                "ModuleInitializer",
                "Constructor"
            };
            Assert.Equal(expectedOrder, InitializationOrderClass.Order);
        }

        public class InitializationOrderClass
        {
            private static readonly List<string> OrderList = new() { "Declaration" };

            public static IReadOnlyList<string> Order => OrderList.AsReadOnly();

            // constructor
            public InitializationOrderClass()
            {
                OrderList.Add("Constructor");
            }

            // static constructor
            static InitializationOrderClass()
            {
                OrderList.Add("Static Constructor");
            }

            [ModuleInitializer]
            internal static void Init() => OrderList.Add("ModuleInitializer");
        }

    }
}
