using System.Collections;
using System.Runtime.CompilerServices;

namespace Examples.Features.CS120.CollectionExpressions.Fixtures;

[CollectionBuilder(typeof(LineBufferBuilder), nameof(LineBufferBuilder.Create))]
public class LineBuffer : IEnumerable<char>
{
    private readonly char[] _buffer = new char[80];

    public LineBuffer(ReadOnlySpan<char> buffer)
    {
        int number = (_buffer.Length < buffer.Length) ? _buffer.Length : buffer.Length;
        for (int i = 0; i < number; i++)
        {
            _buffer[i] = buffer[i];
        }
    }

    public IEnumerator<char> GetEnumerator() => _buffer.AsEnumerable<char>().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _buffer.GetEnumerator();

    // etc
}

internal static class LineBufferBuilder
{
    internal static LineBuffer Create(ReadOnlySpan<char> values) => new(values);
}
