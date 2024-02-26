// C #11 notation
using Primitive11 = System.Int32;
using Nullable11 = System.Nullable<int>;
using Tuple11 = System.ValueTuple<int, int>;

// C# 12 notation
using Primitive12 = int;
using Nullable12 = int?;
using Tuple12 = (int, int);
using Array12 = int[];

using Point = (int x, int y);

namespace Examples.Features.CS120.AliasAnyType;

public class AliasAnyTypeTests
{
    [Fact]
    public void BasicUsage()
    {
        Primitive11 value11 = 1;
        Primitive12 value12 = value11;

        value12.Should()
            .BeOfType(typeof(Primitive12))
            .And.BeOfType(typeof(Primitive11))
            .And.BeOfType(typeof(int));

        Nullable11 nullable11 = null;
        Nullable12 nullable12 = nullable11;

        nullable12.Should().NotHaveValue()
            .And.BeNull();
        // nullable12.Should()
        //     .BeOfType(typeof(Nullable12))
        //     .And.BeOfType(typeof(Primitive11))
        //     .And.BeOfType(typeof(int?));

        Tuple11 tuple11 = (1, 2);
        Tuple12 tuple12 = tuple11;

        tuple12.Should()
            .BeOfType(typeof(Tuple12))
            .And.BeOfType(typeof(Tuple11))
            .And.BeOfType(typeof((int, int)));

        Array12 arrays12 = [1, 2, 3];

        arrays12.Should()
            .BeOfType(typeof(Array12))
            .And.BeOfType(typeof(int[]));

        Point point12 = (800, 600);

        point12.Should()
            .BeOfType(typeof(Point))
            .And.BeOfType(typeof((int, int)));

        return;
    }

}
