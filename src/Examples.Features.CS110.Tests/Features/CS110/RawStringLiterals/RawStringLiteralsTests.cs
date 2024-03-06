namespace Examples.Features.CS110.RawStringLiterals;

/// <summary>
/// Tests for Raw string literals in C# 11.0.
/// </summary>
public class RawStringLiteralsTests
{
    [Fact]
    public void BasicUsage()
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

        longMessage.Should().HaveLength(169);

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

        location2.Should().Be("You are at {43.062, 141.37}");

        var location3 = $$$"""
            You are at {{{{longitude}}}, {latitude}}
            """;

        location3.Should().Be("You are at {43.062, {latitude}}");

        // The preceding example specifies that two braces start and end an interpolation.

        var preSpaceLiteral = $$$"""
                        Hello world.
                """;

        preSpaceLiteral.Should().StartWith("        H");

        return;
    }

}
