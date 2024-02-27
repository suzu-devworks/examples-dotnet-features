using Examples.Features.CS100.SameDeconstruction.Fixtures;

namespace Examples.Features.CS100.SameDeconstruction;

/// <summary>
/// Tests for Assignment and declaration in same deconstruction in C# 10.0.
/// </summary>
public class SameDeconstructionTests
{
    [Fact]
    public void BasicUsage()
    {
        var point = new Point { X = 100, Y = 200 };

        // declaration:
        {
            (int x, int y) = point;

            x.Should().Be(100);
            y.Should().Be(200);
        }

        // assignment:
        {
            int x = 0;
            int y = 0;
            (x, y) = point;

            x.Should().Be(100);
            y.Should().Be(200);
        }

        // mix:
        {
            int x = 0;
            // C# 9.0 : error CS8773: Feature 'Mixed declarations and expressions in deconstruction' is not available in C# 9.0. Please use language version 10.0 or greater.
            (x, int y) = point;

            x.Should().Be(100);
            y.Should().Be(200);
        }

        return;
    }
}
