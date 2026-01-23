using System;
using Xunit;

#pragma warning disable xUnit1045 //Avoid using TheoryData type arguments that might not be serializable

namespace Examples.Features.CSharp90.Tests.Records
{
    /// <summary>
    /// Test for Records in C# 9.0.
    /// </summary>
    public class RecordsTests
    {
        /// <summary>
        /// Define standard init-only property.
        /// </summary>
        public record PersonOfStandard
        {
            public string FirstName { get; init; } = default!;
            public string LastName { get; init; } = default!;
            public DateTime? DateOfBirth { get; init; }
        };

        /// <summary>
        /// Define positional parameter provided in the record declaration.
        /// </summary>
        public record PersonOfPositional(string FirstName, string LastName);

        [Fact]
        public void When_InitializeRecords_Then_ValuesSet()
        {
            // default constructor only.
            var standard = new PersonOfStandard() { FirstName = "Nancy", LastName = "Davolio" };

            Assert.Equal("Nancy", standard.FirstName);
            Assert.Equal("Davolio", standard.LastName);

            // Has not default constructor, argument is required.
            // var positional = new PersonOfPositional() { FirstName = "Nancy", LastName = "Davolio" };
            var positional = new PersonOfPositional("Nancy", "Davolio");

            Assert.Equal("Nancy", positional.FirstName);
            Assert.Equal("Davolio", positional.LastName);

            // A with expression makes a new record instance that is a copy of an existing record instance,
            // with specified properties and fields modified.
            var copied = positional with { FirstName = "John" };

            Assert.Equal("John", copied.FirstName);
            Assert.Equal("Davolio", copied.LastName);

            Assert.Equal("Nancy", positional.FirstName);
            Assert.Equal("Davolio", positional.LastName);

            Assert.NotSame(positional, copied);
            Assert.NotSame(positional.FirstName, copied.FirstName);
            Assert.Same(positional.LastName, copied.LastName);

            // spell-checker: words N Davolio
        }

        [Theory]
        [MemberData(nameof(EqualsData))]
        public void When_RecordsCompared_Then_EqualsReturnsCorrectValue(PersonOfStandard? x, PersonOfStandard? y, bool expected)
        {
            Assert.NotSame(x, y);

            // auto-generated Equals(), operator ==, operator !=.
            Assert.Equal(expected, x!.Equals(y));
            Assert.Equal(expected, x == y);
            Assert.Equal(!expected, x != y);
        }

        public static TheoryData<PersonOfStandard?, PersonOfStandard?, bool> EqualsData
            => new()
            {
                {
                    new PersonOfStandard()
                    {
                        FirstName = "Nancy",
                        LastName = "Davolio",
                        DateOfBirth = DateTime.Parse("2000-02-29")
                    },
                    new PersonOfStandard()
                    {
                        FirstName = "Nancy",
                        LastName = "Davolio",
                        DateOfBirth = DateTime.Parse("2000-02-29")
                    },
                    true
                },
                {
                    new PersonOfStandard()
                    {
                        FirstName = "John",
                        LastName = "Davolio",
                        DateOfBirth = DateTime.Parse("2000-02-29")
                    },
                    new PersonOfStandard()
                    {
                        FirstName = "Nancy",
                        LastName = "Davolio",
                        DateOfBirth = DateTime.Parse("2000-02-29")
                    },
                    false
                },
                {
                    new PersonOfStandard()
                    {
                        FirstName = "Nancy",
                        LastName = "Davolio",
                        DateOfBirth = DateTime.Parse("2000-02-29")
                    },
                    new PersonOfStandard()
                    {
                        FirstName = "Nancy",
                        LastName = "davolio",
                        DateOfBirth = DateTime.Parse("2000-02-29")
                    },
                    false
                },
                {
                    new PersonOfStandard()
                    {
                        FirstName = "Nancy",
                        LastName = "Davolio",
                        DateOfBirth = DateTime.Parse("2000-02-29")
                    },
                    new PersonOfStandard()
                    {
                        FirstName = "Nancy",
                        LastName = "Davolio",
                        DateOfBirth = DateTime.Parse("2000-02-28")
                    },
                    false
                },
                {
                    new PersonOfStandard()
                    {
                        FirstName = "Nancy",
                        LastName = "Davolio",
                        DateOfBirth = DateTime.Parse("2000-02-29")
                    },
                    null,
                    false
                }
            };

        [Theory]
        [MemberData(nameof(ToStringData))]
        public void When_ToStringCalled_Then_FormattedStringReturned(PersonOfPositional person, string? expected)
        {
            // auto-generated ToString().
            Assert.Equal(expected, person!.ToString());

        }

        public static TheoryData<PersonOfPositional, string> ToStringData
           => new()
           {
                {
                    new PersonOfPositional("Nancy", "Davolio"),
                    "PersonOfPositional { FirstName = Nancy, LastName = Davolio }"
                },
                {
                    new PersonOfPositional("John", "Davolio"),
                    "PersonOfPositional { FirstName = John, LastName = Davolio }"
                }
           };

        [Theory]
        [MemberData(nameof(PropertiesData))]
        public void When_PropertyAccessed_Then_ValueReturned(PersonOfPositional person, string? firstName, string? lastName)
        {
            // auto-generated Properties.
            Assert.Equal(firstName, person!.FirstName);
            Assert.Equal(lastName, person!.LastName);
        }

        public static TheoryData<PersonOfPositional, string?, string?> PropertiesData
           => new()
           {
                {
                    new PersonOfPositional("Nancy", "Davolio"),
                    "Nancy", "Davolio"
                },
                {
                    new PersonOfPositional("John", "Davolio"),
                    "John", "Davolio"
                }
           };

    }
}
