namespace Examples.Features.CSharp110.Tests.RawStringLiterals;

/// <summary>
/// Tests for Raw string literals in C# 11.0.
/// </summary>
public class RawStringLiteralsTests
{
    [Fact]
    public void When_UsingRawStringLiteral_Then_TrailingBlockSpacesTrimmed()
    {
        // error CS8936: Feature 'raw string literals' its no available in C# 10.0. Please use language version 11.0 or greater.

        string longMessage = """
            This is a long message.
            It has several lines.
                Some are indented
                        more than others.
            Some should start at the first column.
            Some have "quoted text" in them.
            """;

        Assert.Equal(169, longMessage.Length);

        // Multiple $ characters denote how many consecutive braces start and end the interpolation:

        var longitude = 43.062;
        var latitude = 141.37;

        // error CS9006: The interpolated raw string literal does not start with enough '$' characters to allow this many consecutive opening braces as content.
        // var location1 = $"""
        //     You are at {{longitude}, {latitude}}
        //     """;

        var location2 = $$"""
            You are at {{{longitude}}, {{latitude}}}
            """;

        Assert.Equal("You are at {43.062, 141.37}", location2);

        var location3 = $$$"""
            You are at {{{{longitude}}}, {latitude}}
            """;

        Assert.Equal("You are at {43.062, {latitude}}", location3);

        // The preceding example specifies that two braces start and end an interpolation.

        var preSpaceLiteral = $$$"""
                        Hello world.
                """;

        Assert.StartsWith("        H", preSpaceLiteral);
    }

}
