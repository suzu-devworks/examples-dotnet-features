namespace Examples.Features.CS110.NewlinesInStringInterpolations;

public class NewlinesInStringInterpolationsTests
{
    [Fact]
    public void BasicUsage()
    {
        var a = 1;
        var b = 2;

#pragma warning disable IDE0055
        // C# 10.0 : error CS8967: Newlines inside a non-verbatim interpolated string are not supported in C# 10.0. Please use language version 11.0 or greater.
        var newLineInterpolationsMessage = $"a: {
            a
            }, b: {b}";
#pragma warning restore

        newLineInterpolationsMessage.Should().Be("a: 1, b: 2");

        var verbatimInterpolationsMessage = $@"a: {
            a
            }, b: {b}";

        verbatimInterpolationsMessage.Should().Be("a: 1, b: 2");

        return;
    }

}

