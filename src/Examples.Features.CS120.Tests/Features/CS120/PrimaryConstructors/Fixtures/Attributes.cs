using System.Text.Json.Serialization;

namespace Examples.Features.CS120.PrimaryConstructors.Fixtures;

[AttributeUsage(AttributeTargets.All)]
public class FooAttribute : Attribute;

[method: JsonConstructor] // Good
public partial record Rec(
    [property: Foo] int X,
    [field: NonSerialized] int Y
);

// [method: BarAttr] // warning CS0657: 'method' is not a valid attribute location for this declaration. Valid attribute locations for this declaration are 'type'. All attributes in this block will be ignored.
// public partial record Rec
// {
//     public void Frobnicate()
//     {
//         ...
//     }
// }

[method: JsonConstructor] // Good
public record MyUnit1();

// [method: Attr] // warning CS0657: 'method' is not a valid attribute location for this declaration. Valid attribute locations for this declaration are 'type'. All attributes in this block will be ignored.
// public record MyUnit2;
