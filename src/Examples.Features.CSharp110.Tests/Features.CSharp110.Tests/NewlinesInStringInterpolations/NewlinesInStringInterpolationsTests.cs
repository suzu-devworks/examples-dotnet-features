namespace Examples.Features.CSharp110.Tests.NewlinesInStringInterpolations;

public class NewlinesInStringInterpolationsTests
{
    [Fact]
    public void When_UsingNewlinesWithinStringInterpolation_Then_IsNoProblemWithFormat()
    {
        var a = 1;
        var b = 2;

#pragma warning disable IDE0055 // Fix formatting
        // C# 10.0 : error CS8967: Newlines inside a non-verbatim interpolated string are not supported in C# 10.0. Please use language version 11.0 or greater.
        var newLineInterpolationsMessage = $"a: {
            a
            }, b: {b}";
#pragma warning restore IDE0055

        Assert.Equal("a: 1, b: 2", newLineInterpolationsMessage);

        var verbatimInterpolationsMessage = $@"a: {a}, b: {b}";

        Assert.Equal("a: 1, b: 2", verbatimInterpolationsMessage);
    }

}
