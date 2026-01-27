using Examples.Features.CSharp120.Tests.CollectionExpressions.Fixtures;

namespace Examples.Features.CSharp120.Tests.CollectionExpressions;

public class CollectionExpressionsTests
{
    [Fact]
    public void When_CreatingCollections_Then_ShapesMatch()
    {
        // Create an array:
        int[] a = [1, 2, 3, 4, 5, 6, 7, 8];

        Assert.Equal(8, a.Length);
        Assert.All(a, item => Assert.IsType<int>(item));
        Assert.Equal([1, 2, 3, 4, 5, 6, 7, 8], a);

        // Create a list:
        List<string> b = ["one", "two", "three"];
        IList<string> bb = ["one", "two", "three"]; // It seems okay to use interface.

        Assert.Equal(3, b.Count);
        Assert.All(b, item => Assert.IsType<string>(item));
        Assert.Equal(["one", "two", "three"], b);

        Assert.Equal(3, bb.Count);
        Assert.All(bb, item => Assert.IsType<string>(item));
        Assert.Equal(b, bb);

        // Create a span
        Span<char> c = ['a', 'b', 'c', 'd', 'e', 'f', 'h', 'i'];

        // Span<T> ?
        // https://github.com/fluentassertions/fluentassertions/issues/902
        char[] cArray = c.ToArray();
        Assert.Equal(8, cArray.Length);
        Assert.All(cArray, item => Assert.IsType<char>(item));

        // Create a jagged 2D array:
        int[][] twoD = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];

        Assert.Equal(3, twoD.Length);
        Assert.All(twoD, x =>
        {
            Assert.Equal(3, x.Length);
            Assert.All(x, item => Assert.IsType<int>(item));
        });

        // Create a jagged 2D array from variables:
        int[] row0 = [1, 2, 3];
        int[] row1 = [4, 5, 6];
        int[] row2 = [7, 8, 9];
        int[][] twoDFromVariables = [row0, row1, row2];

        Assert.Equal(3, twoDFromVariables.Length);
        Assert.All(twoDFromVariables, x =>
        {
            Assert.Equal(3, x.Length);
            Assert.All(x, item => Assert.IsType<int>(item));
        });
    }

    [Fact]
    public void When_SpreadingElements_Then_SequencesCombine()
    {
        string[] vowels = ["a", "e", "i", "o", "u"];
        string[] consonants = ["b", "c", "d", "f", "g", "h", "j", "k", "l", "m",
                       "n", "p", "q", "r", "s", "t", "v", "w", "x", "z"];

        string[] alphabet = [.. vowels, .. consonants, "y"];

        Assert.Equal(vowels.Length + consonants.Length + 1, alphabet.Length);
        Assert.All(alphabet, item => Assert.IsType<string>(item));
        Assert.All(alphabet, x =>
        {
            Assert.Single(x);
            Assert.Matches(@"^[a-z]$", x);
        });

        // Spreads can be used with slice pattern (pattern matching).
        Assert.False(alphabet is ["a", "b", "c", ..]);
        Assert.True(alphabet is ["a", "e", "i", ..]);

        // You can also cut out an array using a slicing pattern.
        if (alphabet is [var first, .. var middle, var last])
        {
            Assert.IsType<string>(first);
            Assert.Equal("a", first);
            Assert.IsType<string>(last);
            Assert.Equal("y", last);
            Assert.IsType<string[]>(middle);
            Assert.Equal(24, middle.Length);
        }
    }

    [Fact]
    public void When_ConvertingExpressions_Then_TargetsAreAllowed()
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
        Assert.Equal(3 * 9, all.Length);
    }

    [Fact]
    public void When_UsingRefSafety_Then_SpansRemainScoped()
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
        Assert.Equal(3 * 2, all.Length);
    }

    [Fact]
    public void When_UsingCollectionBuilder_Then_CreatesExpectedBuffer()
    {
        // A type opts in to collection expression support by writing a Create() method and applying
        // the `System.Runtime.CompilerServices.CollectionBuilderAttribute`
        // on the collection type to indicate the builder method.

        LineBuffer line = ['H', 'e', 'l', 'l', 'o', ' ', 'W', 'o', 'r', 'l', 'd', '!'];

        char[] lineArray = line.ToArray();
        Assert.Equal(80, lineArray.Length);
        Assert.StartsWith("Hello World!", new string(lineArray));
    }

    [Fact]
    public void When_UsingEmptyLiteral_Then_CollectionsStayEmpty()
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

            Assert.Equal(2, l.Count);

            // this is okay.
            int[] l2 = b ? [1, 2, 3] : [];

            Assert.Empty(l2);

        }

        // The empty collection expression is permitted to be a singleton
        // if used to construct a final collection value that is known to not be mutable.
        {
            // Can be a singleton, like Array.Empty<int>()
            int[] x = [];

            Assert.Empty(x);

            // Can be a singleton. Allowed to use Array.Empty<int>(), Enumerable.Empty<int>(),
            // or any other implementation that can not be mutated.
            IEnumerable<int> y = [];

            Assert.Empty(y);

            // Must not be a singleton.  Value must be allowed to mutate, and should not mutate
            // other references elsewhere.
            List<int> z = [];

            Assert.Empty(z);
        }

        // Add method is not required for empty collection
        ConvertibleCollection c1 = [];
        Assert.Empty(c1);

        ConvertibleCollectionWithAdd c2 = [];
        Assert.Empty(c2);
    }

}
