using Examples.Features.CSharp60.Tests.NameOfOperators.Fixtures;
using Xunit;

namespace Examples.Features.CSharp60.Tests.NameOfOperators

{
    public class NameOfOperatorTests
    {
        private static readonly object MyMember = new object();

        public static readonly string MyStaticMemberName = nameof(MyMember);
        public const string MyConstMemberName = nameof(MyMember);

        public static readonly string HelperStaticMemberName = Helper.NameOf(() => MyMember);

        // CS0133: The expression being assigned to 'variable' must be constant
        // public const string HelperConstMemberName = Helper.NameOf(() => MyMember);

        [Fact]
        public void When_GettingMemberName_Then_Returns()
        {
            // Use Helper
            {
                // In use method.
                Assert.Equal("MyMember", Helper.NameOf(() => MyMember));
                // In use static.
                Assert.Equal("MyMember", HelperStaticMemberName);
            }

            // C# 6.0 or later
            {
                // In use method.
                Assert.Equal("MyMember", nameof(MyMember));
                // In use static.
                Assert.Equal("MyMember", MyStaticMemberName);
                // In use const.
                Assert.Equal("MyMember", MyConstMemberName);
            }

        }
    }

}
