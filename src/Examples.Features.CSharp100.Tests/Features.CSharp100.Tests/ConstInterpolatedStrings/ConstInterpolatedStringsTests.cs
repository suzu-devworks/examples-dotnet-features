namespace Examples.Features.CSharp100.Tests.ConstInterpolatedStrings;

/// <summary>
///  Tests for Allow const interpolated strings in C# 10.0.
/// </summary>
public class ConstInterpolatedStringsTests
{
    [Fact]
    public void When_ConstInterpolatedString_Then_ConstantEvaluated()
    {
        const string language = "C#";
        const string platform = ".NET";
        const string version = "10.0";

        // C# 9.0 :  error CS8773: Feature 'constant interpolated strings' is not available in C# 9.0. Please use language version 10.0 or greater.
        const string fullProductName = $"{platform} - Language: {language} Version: {version}";

        Assert.Equal(".NET - Language: C# Version: 10.0", fullProductName);
    }
}
