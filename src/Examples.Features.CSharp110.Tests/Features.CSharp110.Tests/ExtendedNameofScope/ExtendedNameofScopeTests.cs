using System.Diagnostics.CodeAnalysis;
using System.Reflection;

#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable CS8321 // Local function is declared but never used

namespace Examples.Features.CSharp110.Tests.ExtendedNameofScope;

/// <summary>
/// Tests for Extended nameof scope in C# 11.0.
/// </summary>
public class ExtendedNameofScopeTests
{
    [Fact]
    public void When_UsingInParameterAttributes_Then_CanBeUsed()
    {
        _ = Method("Hello world.");

        var attribute = GetType().GetMethod("Method", BindingFlags.Static | BindingFlags.Public)
            !.ReturnTypeCustomAttributes
            .GetCustomAttributes(inherit: false)
            .OfType<NotNullIfNotNullAttribute>()
            .FirstOrDefault();

        Assert.Equal("msg", attribute!.ParameterName);
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
        [ParameterString(nameof(T))]
        static void LocalFunction<T>(T param)
        { }

        var lambdaExpression = ([ParameterString(nameof(aNumber))] int aNumber) => aNumber.ToString();

        return msg;
    }

}
