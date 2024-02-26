namespace Examples.Features.CS120.PrimaryConstructors.Fixtures;

public class Person(string name)
{
#pragma warning disable CS9124 // Parameter is captured into the state of the enclosing type and its value is also used to initialize a field, property, or event.
    public string Name { get; set; } = name;   // initialization
#pragma warning restore CS9124
    public override string ToString() => name; // capture
}
