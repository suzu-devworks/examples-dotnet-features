using System;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS90.Records
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
        public void WhenSomeInitialize()
        {
            // default constructor only.
            var standard1 = new PersonOfStandard() { FirstName = "Nancy", LastName = "Davolio" };

            // Has not default constructor, argument is required.
            // _ = new PersonOfPositional() { FirstName = "Nancy", LastName = "Davolio" };
            var positional1 = new PersonOfPositional("Nancy", "Davolio");

            // A with expression makes a new record instance that is a copy of an existing record instance,
            // with specified properties and fields modified.
            var positional2 = positional1 with { FirstName = "John" };

            (positional1 != positional2).IsTrue();

            return;
        }

        [Theory]
        [MemberData(nameof(EqualsData))]
        public void WhenCallingEquals(PersonOfStandard? x, PersonOfStandard? y, bool expected)
        {
            // auto-generated Equals(), operator ==, operator !=.

            x!.Equals(y).Is(expected);

            (x == y).Is(expected);
            (x != y).Is(!expected);

            ReferenceEquals(x, y).IsFalse();

            return;
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
        public void WhenCallingToString(PersonOfPositional person, string? expected)
        {
            // auto-generated ToString().

            person!.ToString().Is(expected);

            return;
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
        public void WhenAccessingProperty(PersonOfPositional person, string? firstName, string? lastName)
        {
            // auto-generated Properties.

            person!.FirstName.Is(firstName);
            person!.LastName.Is(lastName);

            return;
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
