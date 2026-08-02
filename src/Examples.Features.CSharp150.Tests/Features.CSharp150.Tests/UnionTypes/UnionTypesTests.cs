namespace Examples.Features.CSharp150.Tests.UnionTypes;

public class UnionTypesTests
{
    public record class Cat(string Name);
    public record class Dog(string Name);
    public record class Bird(string Name);

    public union Pet(Cat, Dog, Bird);

    [Fact]
    public void When_DefiningSwitchWhereControllingIsUnionType_Then_DefaultNotRequired()
    {
        Pet pet = new Dog("Rex");

        var result = pet switch
        {
            Cat cat => $"It's a cat named {cat.Name}.",
            Dog dog => $"It's a dog named {dog.Name}.",
            Bird bird => $"It's a bird named {bird.Name}.",
        };

        Assert.Equal("It's a dog named Rex.", result);
    }
}
