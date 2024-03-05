using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Examples.Features.CS110.ExtendedNameofScope;

/// <summary>
/// Tests for Extended nameof scope in C# 11.0.
/// </summary>
public class ExtendedNameofScopeTests
{
    [Fact]
    public void BasicUsage()
    {
        _ = Method("Hello world.");

        var attribute = GetType().GetMethod("Method", BindingFlags.Static | BindingFlags.Public)
            !.ReturnTypeCustomAttributes
            .GetCustomAttributes(inherit: false)
            .OfType<NotNullIfNotNullAttribute>()
            .FirstOrDefault();

        attribute!.ParameterName.Should().Be("msg");

        return;
    }

    [AttributeUsage(AttributeTargets.All)]
    public class ParameterString : Attribute
    {
        public ParameterString(string parameterName) => ParameterName = parameterName;
        public string ParameterName { get; }
    }

    // C# 10.0 : error ????

    [return: NotNullIfNotNull(nameof(msg))]
    [ParameterString(nameof(msg))]
    public static string? Method(string? msg)
    {
#pragma warning disable CS8321 // The local function 'xxx' is declared but never used
        [ParameterString(nameof(T))]
        static void LocalFunction<T>(T param)
        { }
#pragma warning restore CS8321

#pragma warning disable IDE0039 // Use local function instead of lambda
        var lambdaExpression = ([ParameterString(nameof(aNumber))] int aNumber) => aNumber.ToString();
#pragma warning restore IDE0039

        return msg;
    }

}
