using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examples.Features.CSharp150.Tests.CollectionExpressionArguments;

public class CollectionExpressionArgumentsTests
{
    [Fact]
    public void When_UsingWithElement_Then_PassCapacityArgumentToConstructor()
    {
        string[] values = ["one", "two", "three"];

        // Pass capacity argument to List<T> constructor
        List<string> names = [with(capacity: values.Length * 2), .. values];

        Assert.Equal(6, names.Capacity);
    }

    [Fact]
    public void When_UsingWithElement_Then_PassComparerArgumentToConstructor()
    {
        string[] values = ["Hello", "HELLO", "hello"];

        // Pass comparer argument to HashSet<T> constructor
        HashSet<string> set = [with(StringComparer.OrdinalIgnoreCase), .. values];
        // set contains only one element because all strings are equal with OrdinalIgnoreCase

        Assert.Single(set);
    }

}
