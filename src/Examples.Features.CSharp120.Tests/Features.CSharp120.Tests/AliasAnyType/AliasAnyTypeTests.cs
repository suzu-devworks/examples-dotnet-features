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

namespace Examples.Features.CSharp120.Tests.AliasAnyType;

public class AliasAnyTypeTests
{
    [Fact]
    public void When_UsingAliases_Then_TypesRemainEquivalent()
    {
        CS11Primitive value11 = 1;
        CS12Primitive value12 = value11;

        Assert.IsType<int>(value12);
        Assert.IsType<int>(value12);
        Assert.IsType<int>(value12);

        CS11Nullable nullable11 = null;
        CS12Nullable nullable12 = nullable11;

        Assert.Null(nullable12);

        CS11Tuple tuple11 = (1, 2);
        CS12Tuple tuple12 = tuple11;

        Assert.IsType<(int, int)>(tuple12);
        Assert.IsType<(int, int)>(tuple12);

        CS12Array arrays12 = [1, 2, 3];

        Assert.IsType<int[]>(arrays12);
        Assert.IsType<int[]>(arrays12);

        Point point12 = (800, 600);

        Assert.IsType<Point>(point12);
        Assert.IsType<(int, int)>(point12);
    }

}
