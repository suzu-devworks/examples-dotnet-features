using System.Text;

#pragma warning disable IDE0230 // Use UTF-8 string literal

namespace Examples.Features.CS110.UTF8StringLiterals;

/// <summary>
/// Tests for UTF-8 string literals in C# 11.0.
/// </summary>
public class UTF8StringLiterals
{
    [Fact]
    public void BasicUsage()
    {
        ReadOnlySpan<byte> authWithTrailingSpace = new byte[] { 0x41, 0x55, 0x54, 0x48, 0x20 };
        ReadOnlySpan<byte> authStringLiteral = "AUTH "u8;

        authWithTrailingSpace.SequenceEqual(authStringLiteral).Should().BeTrue();

        byte[] bytes = "AUTH "u8.ToArray();

        IsMatch(bytes).Should().BeTrue();

        byte[] unicodeBytes = Encoding.Unicode.GetBytes("AUTH ");
        byte[] utf8Bytes = Encoding.UTF8.GetBytes("AUTH ");

        IsMatch(unicodeBytes).Should().BeFalse();
        IsMatch(utf8Bytes).Should().BeTrue();

        return;

        static bool IsMatch(in byte[] values)
            => values is [0x41, 0x55, 0x54, 0x48, 0x20];

    }
}

