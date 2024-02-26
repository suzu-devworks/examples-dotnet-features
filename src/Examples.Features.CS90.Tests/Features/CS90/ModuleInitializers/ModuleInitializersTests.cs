using System.Runtime.CompilerServices;
using ChainingAssertion;
using Examples.Features.CS90.ModuleInitializers.Fixtures;
using Xunit;

namespace Examples.Features.CS90.ModuleInitializers
{
    /// <summary>
    /// Tests for module initializers in C# 9.0.
    /// </summary>
    public class ModuleInitializersTests
    {
        [Theory]
        [InlineData("TYPE-1", "This Class is DerivedA.")]
        [InlineData("TYPE-2", "DerivedB is this.")]
        public void TestName(string input, string expected)
        {
            var instance = BaseClass.Create(input);

            instance.IsNotNull();
            instance!.GetMessage().Is(expected);

            return;
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
    }
}
