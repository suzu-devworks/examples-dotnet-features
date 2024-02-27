#pragma warning disable format
// C# 11.0
using CS11Nullable = System.Nullable<int>;
using CS11Primitive = System.Int32;
using CS11Tuple = System.ValueTuple<int, int>;

// C# 12.0 or later
using CS12Array = int[];
using CS12Nullable = int?;
using CS12Primitive = int;
using CS12Tuple = (int, int);

using Point = (int x, int y);
#pragma warning restore format

namespace Examples.Features.CS120.AliasAnyType;

public class AliasAnyTypeTests
{
    [Fact]
    public void BasicUsage()
    {
        CS11Primitive value11 = 1;
        CS12Primitive value12 = value11;

        value12.Should()
            .BeOfType(typeof(CS12Primitive))
            .And.BeOfType(typeof(CS11Primitive))
            .And.BeOfType(typeof(int));

        CS11Nullable nullable11 = null;
        CS12Nullable nullable12 = nullable11;

        nullable12.Should().NotHaveValue()
            .And.BeNull();
        // nullable12.Should()
        //     .BeOfType(typeof(CS12Nullable))
        //     .And.BeOfType(typeof(CS11Nullable))
        //     .And.BeOfType(typeof(int?));

        CS11Tuple tuple11 = (1, 2);
        CS12Tuple tuple12 = tuple11;

        tuple12.Should()
            .BeOfType(typeof(CS12Tuple))
            .And.BeOfType(typeof(CS11Tuple))
            .And.BeOfType(typeof((int, int)));

        CS12Array arrays12 = [1, 2, 3];

        arrays12.Should()
            .BeOfType(typeof(CS12Array))
            .And.BeOfType(typeof(int[]));

        Point point12 = (800, 600);

        point12.Should()
            .BeOfType(typeof(Point))
            .And.BeOfType(typeof((int, int)));

        return;
    }

}
