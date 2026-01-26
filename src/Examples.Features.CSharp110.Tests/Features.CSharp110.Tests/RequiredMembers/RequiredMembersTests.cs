using System.Diagnostics.CodeAnalysis;

namespace Examples.Features.CSharp110.Tests.RequiredMembers;

/// <summary>
/// Tests for Required members in C# 11.0.
/// </summary>
public class RequiredMembersTests
{
    [Fact]
    public void When_UsingRequiredProperty_Then_CanForceInitialization()
    {
        // error CS9035: Required member 'xxx' must be set in the object initializer or attribute constructor.
        //_ = new MyClass();

        var actual = new MyClass { Text = "My class text.", Value = null };

        Assert.Multiple(
            () => Assert.NotNull(actual),
            () => Assert.Equal("My class text.", actual.Text),
            () => Assert.Null(actual.Value)
        );
    }

    private class MyClass
    {
        public required string Text { get; init; }
        public required int? Value { get; init; }

    }

    // The required modifier can't be applied to members of an interface.

    private interface IMyInterface
    {
        // error CS0106: The modifier 'required' is not valid for this ite
        // public required string Text { get; init; }
    }

    // Derived classes can't hide a required member declared in the base class

    private class MyDelved : MyClass
    {
        // error CS9031: Required member 'RequiredMembersTests.MyClass.Text' cannot be hidden by 'RequiredMembersTests.MyDelved.Text'.
        // public new string Text { get; set; }
    }

    [Fact]
    public void When_UsingGenericNewConstraint_Then_CanNotUsedTypeArgument()
    {
        // A type with any required members may not be used as a type argument when the type parameter includes the new() constraint.

        var actual = new MyGenericClass<MyClass> { Value = new MyClass { Text = "My generic class.", Value = 0 } };

        Assert.Multiple(
            () => Assert.NotNull(actual),
            () => Assert.IsType<MyClass>(actual.Value),
            () => Assert.Equal("My generic class.", actual.Value.Text),
            () => Assert.Equal(0, actual.Value.Value)
        );

        // error CS9040: 'MyClass' cannot satisfy the 'new()' constraint on parameter 'T' in the generic type or or method 'MyGenericNewClass<T>'
        //  because 'MyClass' has required members.
        // _ = new MyGenericNewClass<MyClass> { Value = new MyClass { Text = "My generic class.", Value = 0 } };
    }

    private class MyGenericClass<T>
    {
        public required T Value { get; init; }
    }

    private class MyGenericNewClass<T>
        where T : new()
    {
        public required T Value { get; init; }
    }

    [Fact]
    public void When_UsingSetsRequiredMembersAttributes_Then_DisablesCompilerChecks()
    {
        // The SetsRequiredMembers disables the compiler's checks that all required members
        //  are initialized when an object is created. Use it with caution.

        _ = new Person() { FirstName = "Andy", LastName = "Foo" };
        _ = new Person("Belle", "Bar");

        var student1 = new Student() { Id = Guid.NewGuid(), FirstName = "Conrad", LastName = "Baz" };

        Assert.Multiple(
            () => Assert.NotEqual(Guid.Empty, student1.Id),
            () => Assert.Equal("Conrad", student1.FirstName),
            () => Assert.Equal("Baz", student1.LastName)
        );

        // No Id check occurs.
        var student2 = new Student("Daisy", "Hum");

        Assert.Multiple(
            () => Assert.Equal(Guid.Empty, student2.Id),
            () => Assert.Equal("Daisy", student2.FirstName),
            () => Assert.Equal("Hum", student2.LastName)
        );
    }

    public class Person
    {
        public Person() { }

        [SetsRequiredMembers]
        public Person(string firstName, string lastName) =>
            (FirstName, LastName) = (firstName, lastName);

        public required string FirstName { get; init; }
        public required string LastName { get; init; }

        public int? Age { get; set; }
    }

    public class Student : Person
    {
        public Student() : base()
        {
        }

        [SetsRequiredMembers]
        public Student(string firstName, string lastName) :
            base(firstName, lastName)
        {
        }

        public required Guid Id { get; init; }

        public double GPA { get; set; }
    }

}

