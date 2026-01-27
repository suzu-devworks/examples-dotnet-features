using Examples.Features.CSharp90.Tests.InitOnlySetters.Fixtures;
using Xunit;

namespace Examples.Features.CSharp90.Tests.InitOnlySetters
{
    /// <summary>
    /// Tests for Init only setters in C# 9.0.
    /// </summary>
    public class InitOnlySettersTests
    {
        [Fact]
        public void When_InitializingSomeObjects_Then_CanRestrictSettings()
        {
            // Private setter { get; private set; }
            {
                var person = new PersonOfPrivateSetter();

                Assert.Null(person.Id);
                Assert.Null(person.Name);

                // error CS0272: The property or indexer '...Id' cannot be used in this context because the set accessor is inaccessible.
                // person.Id = "123";
                // person.Name = "Andy Ruth";

                // overwrite within the same assembly
                // error CS0272: The property or indexer '...Id' cannot be used in this context because the set accessor is inaccessible.
                // person = new PersonOfPrivateSetter() { Id = "456", Name = "Dina Salimzianova" };

            }
            // spell-checker: words Salimzianova

            // Internal setter { get; internal set; }
            {
                var person = new PersonOfInternalSetter();

                Assert.Null(person.Id);
                Assert.Null(person.Name);

                person.Id = "123";
                person.Name = "Andy Ruth";

                Assert.Equal("123", person.Id);
                Assert.Equal("Andy Ruth", person.Name);

                // overwrite within the same assembly
                person = new PersonOfInternalSetter() { Id = "456", Name = "Dina Salimzianova" };

                Assert.Equal("456", person.Id);
                Assert.Equal("Dina Salimzianova", person.Name);
            }

            // Init only setter { get; init; }
            {
                var person = new PersonOfInitSetter();

                Assert.Null(person.Id);
                Assert.Null(person.Name);

                // error CS8852: Init-only property or indexer '...Id' can only be assigned in an object initializer, or on 'this' or 'base' in an instance constructor or an 'init' accessor.
                // person.Id = "123";
                // person.Name = "Andy Ruth";

                // overwrite within the same assembly
                person = new PersonOfInitSetter() { Id = "456", Name = "Foo" };

                Assert.Equal("456", person.Id);
                Assert.Equal("Foo", person.Name);
            }

        }

        [Fact]
        public void When_UsingInitOnlySetters_Then_CanBeParameterValidation()
        {
            // Using init only setters for parameter validation
            {
                // Valid initialization
                var cat = new Cat { Name = "Whiskers", Age = 3 };
                Assert.Equal("Whiskers", cat.Name);
                Assert.Equal(3, cat.Age);

                // Invalid initialization - negative age
                Assert.Throws<System.ArgumentOutOfRangeException>(() =>
                {
                    var invalidCat = new Cat { Name = "Shadow", Age = -2 };
                });
            }
        }

        private class Cat
        {
            public string Name { get; init; } = default!;
            public int Age
            {
                get => _age;
                init
                {
                    if (value < 0)
                    {
                        throw new System.ArgumentOutOfRangeException(nameof(value), "Age cannot be negative.");
                    }
                    _age = value;
                }
            }
            private int _age;
        }


    }
}
