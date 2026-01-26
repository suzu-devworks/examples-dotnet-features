using Examples.Features.CSharp100.Tests.ExtendedDeconstruction.Fixtures;

namespace Examples.Features.CSharp100.Tests.ExtendedDeconstruction;

/// <summary>
/// Tests for Assignment and declaration in same deconstruction in C# 10.0.
/// </summary>
public class ExtendedDeconstructionTests
{
    [Fact]
    public void When_DeconstructingPoint_Then_ValuesAssignedAndDeclared()
    {
        var point = new Point { X = 100, Y = 200 };

        // declaration:
        {
            (int x, int y) = point;

            Assert.Equal(100, x);
            Assert.Equal(200, y);
        }

        // assignment:
        {
            int x;
            int y;
            (x, y) = point;

            Assert.Equal(100, x);
            Assert.Equal(200, y);
        }

        // Allow both assignment and declaration in the same deconstruction.
        {
            int x;
            // C# 9.0 : error CS8773: Feature 'Mixed declarations and expressions in deconstruction' is not available in C# 9.0. Please use language version 10.0 or greater.
            (x, int y) = point;

            Assert.Equal(100, x);
            Assert.Equal(200, y);
        }
    }
}
