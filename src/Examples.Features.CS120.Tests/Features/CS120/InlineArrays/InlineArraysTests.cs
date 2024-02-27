namespace Examples.Features.CS120.InlineArrays;

/// <summary>
/// Tests for Inline arrays in C# 12.0.
/// </summary>
public class InlineArraysTests
{
    [Fact]
    public void BasicUsage()
    {
        var buffer = new Buffer10<int>();
        for (int i = 0; i < 10; i++)
        {
            buffer[i] = i;
        }

        List<int> result = [];
        foreach (var i in buffer)
        {
            result.Add(i);
        }

        result.Should().BeEquivalentTo([0, 1, 2, 3, 4, 5, 6, 7, 8, 9]);

        return;
    }

    [System.Runtime.CompilerServices.InlineArray(10)]
    public struct Buffer10<T>
    {
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0051 // Remove unused private member
        private T _element0;
#pragma warning restore IDE0051
#pragma warning restore IDE0044

    }

    [Fact]
    public void WhenReadingArrayElement()
    {
        var buffer = new Buffer10<int>();
        for (int i = 0; i < 10; i++)
        {
            buffer[i] = i;
        }

        M1(buffer);
        M2(buffer);
        M3();

        return;

#pragma warning disable IDE0059 // Remove unnecessary value assignment

        static void M1(Buffer10<int> x)
        {
            // Ok, equivalent to `ref int a = ref InlineArrayAsSpan<Buffer10<int>, int>(ref x, 10)[0]`
            ref int a = ref x[0];

            // Ok, equivalent to `System.Span<int> a = InlineArrayAsSpan<Buffer10<int>, int>(ref x, 10).Slice(0, 10)`
            System.Span<int> aa = x[..];
        }

        static void M2(in Buffer10<int> x)
        {
            // Ok, equivalent to `ref readonly int a = ref InlineArrayAsReadOnlySpan<Buffer10<int>, int>(in x, 10)[0]`
            ref readonly int a = ref x[0];
            // error CS8329: Cannot use method 'this.get' as a ref or out value because it is a readonly variable
            //ref int b = ref x[0];

            // Ok, equivalent to `System.ReadOnlySpan<int> a = InlineArrayAsReadOnlySpan<Buffer10<int>, int>(in x, 10).Slice(0, 10)`
            System.ReadOnlySpan<int> aa = x[..];
            // error CS0029: Cannot implicitly convert type 'System.ReadOnlySpan<int>' to 'System.Span<int>'
            //System.Span<int> b = x[..];
        }

        static Buffer10<int> GetBuffer() => default;

        static void M3()
        {
            // Ok, equivalent to `int a = InlineArrayAsReadOnlySpan<Buffer10<int>, int>(GetBuffer(), 10)[0]`
            int a = GetBuffer()[0];

            // error CS8156: An expression cannot be used in this context because it may not be passed or returned by reference
            //ref readonly int b = ref GetBuffer()[0];
            // error CS1510: A ref or out value must be an assignable variable
            //ref int c = ref GetBuffer()[0];

            // error CS8156: An expression cannot be used in this context because it may not be passed or returned by reference
            // _ = GetBuffer()[..];
        }

#pragma warning restore IDE0059

    }

    [Fact]
    public void UnderstandConversions()
    {
        var buffer = new Buffer10<int>();
        for (int i = 0; i < 10; i++)
        {
            buffer[i] = i;
        }

        M1(buffer);
        M2(buffer);
        M3();

        ReadOnlySpan<int> actual = buffer;
        actual.ToArray().Should().BeEquivalentTo([0, 1, 2, 3, 4, 5, 6, 7, 8, 9]);

        return;

#pragma warning disable IDE0059 // Remove unnecessary value assignment

        static void M1(Buffer10<int> x)
        {
            // Ok, equivalent to `System.ReadOnlySpan<int> a = InlineArrayAsReadOnlySpan<Buffer10<int>, int>(in x, 10)`
            System.ReadOnlySpan<int> a = x;
            // Ok, equivalent to `System.Span<int> b = InlineArrayAsSpan<Buffer10<int>, int>(ref x, 10)`
            System.Span<int> b = x;
        }

        static void M2(in Buffer10<int> x)
        {
            // Ok, equivalent to `System.ReadOnlySpan<int> a = InlineArrayAsReadOnlySpan<Buffer10<int>, int>(in x, 10)`
            System.ReadOnlySpan<int> a = x;
            // error CS9164: Cannot convert expression to 'Span<int>' because it is not an assignable variable
            //System.Span<int> b = x;
        }

        static Buffer10<int> GetBuffer() => default;

        static void M3()
        {
            var b = GetBuffer()[0];
            // error CS9165: Cannot convert expression to 'ReadOnlySpan<int>' because it may not be passed or returned by reference
            //System.ReadOnlySpan<int> a = GetBuffer();
            // error CS9164: Cannot convert expression to 'Span<int>' because it is not an assignable variable
            //System.Span<int> b = GetBuffer();
        }

#pragma warning restore IDE0059

    }

}
