using System.Reflection;

namespace Examples.GenericAttributes;

public class GenericAttributesTests
{
    [Fact]
    public void BasicUsage()
    {
        var attribute1 = GetType()
            .GetMethod("Method1")
            ?.GetCustomAttribute<TypeAttribute>(inherit: false);

        attribute1.Should().BeOfType<TypeAttribute>();


        var attribute2 = GetType()
            .GetMethod("Method2")
            ?.GetCustomAttribute<GenericAttribute<string>>(inherit: false);

        attribute2.Should().BeOfType<GenericAttribute<string>>();

        var attribute3 = GetType()
            .GetMethod("Method2")
            ?.GetCustomAttribute(typeof(GenericAttribute<>), inherit: false);

        attribute3.Should().BeOfType<GenericAttribute<string>>();

        return;

    }

    // Before C# 11:

    [AttributeUsage(AttributeTargets.All)]
    private class TypeAttribute : Attribute
    {
        public TypeAttribute(Type type) => ParamType = type;

        public Type ParamType { get; }
    }

    [TypeAttribute(typeof(string))]
    public static string? Method1() => default;

    // C# 11.0 features.

    // CS10.0 : error CS8936: Feature 'generic attributes' is not available in C# 10.0. Please use language version 11.0 or greater.
    [AttributeUsage(AttributeTargets.All)]
    public class GenericAttribute<T> : Attribute { }

    // CS10.0 : error CS0246: The type or namespace name 'T' could not be found (are you missing a using directive or an assembly reference?)
    // CS10.0 : error CS8936: Feature 'generic attributes' is not available in C# 10.0. Please use language version 11.0 or greater.
    [GenericAttribute<string>]
    public static string? Method2() => default;

    // Not allowed! generic attributes must be fully constructed types.
    // error CS0246: The type or namespace name 'T' could not be found (are you missing a using directive or an assembly reference?)
    // [GenericAttribute<T>]
    // private static string? Method3() => default;

}


