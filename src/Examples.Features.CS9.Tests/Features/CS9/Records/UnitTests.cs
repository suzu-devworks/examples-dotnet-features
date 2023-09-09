using System;
using ChainingAssertion;
using Xunit;

#nullable enable

namespace Examples.Features.CS9.Records
{
    /// <summary>
    /// Test for C# 9.0 Records.
    /// </summary>
    public class UnitTests
    {
        [Fact]
        public void WhenSomeInitialize()
        {
            // Type-1 has not default constructor.s
            // _ = new Type1Person() { FirstName = "Nancy", LastName = "Davolio" };
            var type1 = new Type1Person("Nancy", "Davolio");

            // Type2 is default constructor
            var type2 = new Type2Person() { FirstName = "Nancy", LastName = "Davolio" };

            // A with expression makes a new record instance that is a copy of an existing record instance,
            // with specified properties and fields modified.
            var type1other = type1 with { FirstName = "John" };

            // Assert.
            (type1 != type1other).IsTrue();

            // ToString() Generated.
            //TODO DateTime cultre formatter.
            type2.ToString().Is("Type2Person { FirstName = Nancy, LastName = Davolio, DateOfBirth =  }");

            return;
        }

        [Fact]
        public void WhenCallingEquals()
        {
            var object1 = new Type2Person()
            {
                FirstName = "Nancy",
                LastName = "Davolio",
                DateOfBirth = DateTime.Parse("2000-02-29")
            };
            var object2 = new Type2Person()
            {
                FirstName = "Nancy",
                LastName = "Davolio",
                DateOfBirth = DateTime.Parse("2000-02-29")
            };

            object1.Equals(object2).Is(true);
            (object1 == object2).Is(true);
            object.ReferenceEquals(object1, object2).IsFalse();

            return;
        }

        [Fact]
        public void WhenCallingNotEquals()
        {
            var object1 = new Type2Person()
            {
                FirstName = "Nancy",
                LastName = "Davolio",
                DateOfBirth = DateTime.Parse("2000-02-29")
            };

            (object1 == null).IsFalse();

            (object1 == new Type2Person()
            {
                FirstName = "Nancy",
                LastName = "Davolio",
                DateOfBirth = DateTime.Parse("2000-03-01")
            })
                .IsFalse();

            (object1 == new Type2Person() { FirstName = "Nancy", LastName = "Davolio" })
                .IsFalse();

            (object1 == new Type2Person()
            {
                FirstName = "Nancy",
                LastName = "davolio",
                DateOfBirth = DateTime.Parse("2000-02-09")
            })
                .IsFalse();

            (object1 == new Type2Person()
            {
                FirstName = "John",
                LastName = "Davolio",
                DateOfBirth = DateTime.Parse("2000-02-09")
            })
                .IsFalse();

            return;
        }

    }
}
