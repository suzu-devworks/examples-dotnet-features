using System.Text;

namespace Examples.Features.CSharp110.Tests.UTF8StringLiterals;

/// <summary>
/// Tests for UTF-8 string literals in C# 11.0.
/// </summary>
public class UTF8StringLiterals
{
    [Fact]
    public void When_UsingUtf8LiteralString_Then_CanBeUsedAsEncodingBinaries()
    {
        ReadOnlySpan<byte> authWithTrailingSpace = new byte[] { 0x41, 0x55, 0x54, 0x48, 0x20 };
        ReadOnlySpan<byte> authStringLiteral = "AUTH "u8;

        Assert.Equal(authWithTrailingSpace.Length, authStringLiteral.Length);
        Assert.Equal(authWithTrailingSpace.ToArray(), authStringLiteral.ToArray());

        byte[] bytes = "AUTH "u8.ToArray();

        Assert.True(IsMatch(bytes));

        byte[] unicodeBytes = Encoding.Unicode.GetBytes("AUTH ");
        byte[] utf8Bytes = Encoding.UTF8.GetBytes("AUTH ");

        Assert.False(IsMatch(unicodeBytes));
        Assert.True(IsMatch(utf8Bytes));

        static bool IsMatch(in byte[] values)
            => values is [0x41, 0x55, 0x54, 0x48, 0x20];
    }
}

