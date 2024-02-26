using Examples.Features.CS120.CollectionExpressions.Fixtures;

namespace Examples.Features.CS120.CollectionExpressions;

public class CollectionExpressionsTests
{
    [Fact]
    public void BasicUsage()
    {
        // Create an array:
        int[] a = [1, 2, 3, 4, 5, 6, 7, 8];

        a.Should().HaveCount(8)
            .And.AllBeAssignableTo<int>()
            .And.BeInAscendingOrder()
            .And.Equal(1, 2, 3, 4, 5, 6, 7, 8);

        // Create a list:
        List<string> b = ["one", "two", "three"];
        IList<string> bb = ["one", "two", "three"]; // It seems okay to use interface.

        b.Should().HaveCount(3)
            .And.AllBeAssignableTo<string>()
            .And.Equal("one", "two", "three");

        bb.Should().HaveCount(3)
            .And.AllBeAssignableTo<string>()
            .And.BeEquivalentTo(b);

        // Create a span
        Span<char> c = ['a', 'b', 'c', 'd', 'e', 'f', 'h', 'i'];

        // Span<T> ?
        // https://github.com/fluentassertions/fluentassertions/issues/902
        c.ToArray().Should().HaveCount(8)
            .And.AllBeAssignableTo<char>()
            .And.ContainInOrder(['a', 'b', 'c']);

        // Create a jagged 2D array:
        int[][] twoD = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];

        twoD.Should().HaveCount(3)
            .And.AllSatisfy(x => x.Should().HaveCount(3)
                .And.AllBeOfType<int>());

        // Create a jagged 2D array from variables:
        int[] row0 = [1, 2, 3];
        int[] row1 = [4, 5, 6];
        int[] row2 = [7, 8, 9];
        int[][] twoDFromVariables = [row0, row1, row2];

        twoDFromVariables.Should().HaveCount(3)
            .And.AllSatisfy(x => x.Should().HaveCount(3)
                .And.AllBeOfType<int>());

        return;
    }

    [Fact]
    public void UseSpreadElement()
    {
        string[] vowels = ["a", "e", "i", "o", "u"];
        string[] consonants = ["b", "c", "d", "f", "g", "h", "j", "k", "l", "m",
                       "n", "p", "q", "r", "s", "t", "v", "w", "x", "z"];

        string[] alphabet = [.. vowels, .. consonants, "y"];

        alphabet.Should().HaveCount(vowels.Length + consonants.Length + 1)
            .And.AllBeAssignableTo<string>()
            .And.AllSatisfy(x => x.Should().HaveLength(1)
                .And.MatchRegex(@"^[a-z]$"));

        // Spreads can be used with slice pattern (pattern matching).
        (alphabet is ["a", "b", "c", ..]).Should().BeFalse();
        (alphabet is ["a", "e", "i", ..]).Should().BeTrue();

        // You can also cut out an array using a slicing pattern.
        if (alphabet is [var first, .. var middle, var last])
        {
            first.Should().BeOfType<string>().And.Be("a");
            last.Should().BeOfType<string>().And.Be("y");
            middle.Should().BeOfType<string[]>()
                .And.HaveCount(24);
        }

        return;
    }

    [Fact]
    public void UnderstandConversionRules()
    {
        // A single dimensional array type T[]
        int[] a = [1, 2, 3];

        // A span type:
        //  System.Span<T>
        Span<int> b1 = [1, 2, 3];
        //  System.ReadOnlySpan<T>
        ReadOnlySpan<int> b2 = [1, 2, 3];

        // A type with a create method with an iteration type determined from a GetEnumerator instance method or enumerable interface, not from an extension method
        //  ???

        // A struct or class type that implements System.Collections.IEnumerable where:
        //  The type has an applicable constructor that can be invoked with no arguments,
        //  and the constructor is accessible at the location of the collection expression.
        //  ???

        //  If the collection expression has any elements,
        //  the type has an applicable instance or extension method Add that can be invoked with a single argument of the iteration type,
        //  and the method is accessible at the location of the collection expression.
        ConvertibleCollectionWithAdd d2 = [1, 2, 3];

        // An interface type :
        //  System.Collections.Generic.IEnumerable<T>
        IEnumerable<int> e1 = [1, 2, 3];
        //  System.Collections.Generic.IReadOnlyCollection<T>
        IReadOnlyCollection<int> e2 = [1, 2, 3];
        //  System.Collections.Generic.IReadOnlyList<T>
        IReadOnlyList<int> e3 = [1, 2, 3];
        //  System.Collections.Generic.ICollection<T>
        ICollection<int> e4 = [1, 2, 3];
        //  System.Collections.Generic.IList<T>
        IList<int> e5 = [1, 2, 3];

        int[] all = [
            .. a,
            .. b1,
            .. b2,
            .. d2,
            .. e1,
            .. e2,
            .. e3,
            .. e4,
            .. e5,
            ];
        all.Should().HaveCount(3 * 9);

        return;
    }

    [Fact]
    public void UnderstandRefSafety()
    {
        // A collection expression with a safe-context of declaration-block cannot escape the enclosing scope,
        // and the compiler may store the collection on the stack rather than the heap.

        // To allow a collection expression for a ref struct type to escape the declaration-block,
        // it may be necessary to cast the expression to another type.

        static ReadOnlySpan<int> AsSpanConstants()
        {
            return [1, 2, 3]; // ok: span refers to assembly data section
        }

        static ReadOnlySpan<T> AsSpan<T>(T x, T y, T z)
        {
            //return [x, y];    // error: span may refer to stack data
            return (T[])[x, y, z]; // ok: span refers to T[] on heap
        }

        var a = AsSpanConstants();
        var b = AsSpan(1, 2, 3);

        int[] all = [.. a, .. b];
        all.Should().HaveCount(3 * 2);

        return;
    }

    [Fact]
    public void UseCollectionBuilder()
    {
        // A type opts in to collection expression support by writing a Create() method and applying
        // the `System.Runtime.CompilerServices.CollectionBuilderAttribute`
        // on the collection type to indicate the builder method.

        LineBuffer line = ['H', 'e', 'l', 'l', 'o', ' ', 'W', 'o', 'r', 'l', 'd', '!'];

        line.Should().HaveCount(80)
            .And.StartWith("Hello World!".ToCharArray());

        return;
    }

    [Fact]
    public void UseEmptyCollectionLiteral()
    {
        // error CS9176: There is no target type for the collection expression.
        //var v = [];

        // Spreading an empty literal is permitted to be elided.
        {
            bool b = false;
            var x = 1;
            var y = 2;
            // error CS0173: Type of conditional expression cannot be determined because there is no implicit conversion between 'class1' and 'class2'
            // List<int> l = [x, y, .. b ? [1, 2, 3] : []];
            List<int> l = [x, y, .. b ? [1, 2, 3] : (int[])[]];

            l.Should().HaveCount(2);

            // this is okay.
            int[] l2 = b ? [1, 2, 3] : [];

            l2.Should().BeEmpty();

        }

        // The empty collection expression is permitted to be a singleton
        // if used to construct a final collection value that is known to not be mutable.
        {
            // Can be a singleton, like Array.Empty<int>()
            int[] x = [];

            x.Should().BeEmpty();

            // Can be a singleton. Allowed to use Array.Empty<int>(), Enumerable.Empty<int>(),
            // or any other implementation that can not be mutated.
            IEnumerable<int> y = [];

            y.Should().BeEmpty();

            // Must not be a singleton.  Value must be allowed to mutate, and should not mutate
            // other references elsewhere.
            List<int> z = [];

            z.Should().BeEmpty();
        }

        // Add method is not required for empty collection
        ConvertibleCollection c1 = [];
        c1.Should().BeEmpty();

        ConvertibleCollectionWithAdd c2 = [];
        c2.Should().BeEmpty();

        return;
    }

}
