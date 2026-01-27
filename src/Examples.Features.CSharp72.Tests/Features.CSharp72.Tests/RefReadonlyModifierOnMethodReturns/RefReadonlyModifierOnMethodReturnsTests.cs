using Examples.Features.CSharp72.Tests.RefReadonlyModifierOnMethodReturns.Fixtures;
using Xunit;

namespace Examples.Features.CSharp72.Tests.RefReadonlyModifierOnMethodReturns
{
    /// <summary>
    /// Tests for Use the <c>ref readonly</c> modifier on method returns in C# 7.2.
    /// </summary>
    /// <seealso href="https://docs.microsoft.com/ja-jp/dotnet/csharp/write-safe-efficient-code#use-ref-readonly-return-statements" />
    public class RefReadonlyModifierOnMethodReturnsTests
    {
        [Fact]
        public void When_UsingRefReadonlyModifier_Then_ReferencesAreReadOnly()
        {
            // use normal struct.
            {
                var point = new SamplePoint(1, 2, 3);

                Assert.True(point.X == 1 && point.Y == 2 && point.Z == 3);

                point.X = 10;

                Assert.True(point.X == 10 && point.Y == 2 && point.Z == 3);
            }

            // use ref struct.
            {
                var point = new SamplePoint(1, 2, 3);
                ref var alias = ref point;

                Assert.True(point.X == 1 && point.Y == 2 && point.Z == 3);

                point.X = 10;
                alias.Z = 30;

                Assert.True(point.X == 10 && point.Y == 2 && point.Z == 30);
                Assert.True(alias.X == 10 && alias.Y == 2 && alias.Z == 30);
            }

            // use ref readonly struct.
            {
                var point = new SamplePoint(1, 2, 3);
                // C# 7.1 : error CS8302: Feature 'readonly references' is not available in C# 7.1. Please use language version 7.2 or greater.
                ref readonly var alias = ref point;

                Assert.True(point.X == 1 && point.Y == 2 && point.Z == 3);

                point.X = 10;
                // error CS0131 The left-hand side of an assignment must be a variable, property or indexer
                //alias.Z = 30;

                Assert.True(point.X == 10 && point.Y == 2 && point.Z == 3);
                Assert.True(alias.X == 10 && alias.Y == 2 && alias.Z == 3);
            }
        }

    }

}
