namespace Examples.Features.CS100.ConstInterpolatedStrings;

/// <summary>
///  Tests for Allow const interpolated strings in C# 10.0.
/// </summary>
public class ConstInterpolatedStringsTests
{
    [Fact]
    public void BasicUsage()
    {
        const string LANGUAGE = "C#";
        const string PLATFORM = ".NET";
        const string VERSION = "10.0";
        // C# 9.0 :  error CS8773: Feature 'constant interpolated strings' is not available in C# 9.0. Please use language version 10.0 or greater.
        const string FULL_PRODUCT_NAME = $"{PLATFORM} - Language: {LANGUAGE} Version: {VERSION}";

        FULL_PRODUCT_NAME.Should().Be(".NET - Language: C# Version: 10.0");

        return;
    }
}
