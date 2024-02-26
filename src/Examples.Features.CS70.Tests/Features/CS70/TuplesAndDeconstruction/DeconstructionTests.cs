using System.Collections.Generic;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS70.TuplesAndDeconstruction
{
    /// <summary>
    /// Tests for Tuples and deconstruction in C# 7.0.
    /// </summary>
    public class DeconstructionTests
    {
        [Fact]
        public void WhenUsingInstanceMethod()
        {
            var person = new Person("John", "Quincy", "Adams", "Boston", "MA");

            var (fName1, lName1) = person;

            (fName1, lName1).Is(("John", "Adams"));

            var (fName2, mName2, lName2) = person;

            (fName2, mName2, lName2).Is(("John", "Quincy", "Adams"));

            // Can also be used to discard.
            var (fName3, _, city3, state3) = person;

            (fName3, city3, state3).Is(("John", "Boston", "MA"));

            return;
        }

        public class Person
        {
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }
            public string State { get; set; }

            public Person(string fname, string mname, string lname,
                          string cityName, string stateName)
            {
                FirstName = fname;
                MiddleName = mname;
                LastName = lname;
                City = cityName;
                State = stateName;
            }

            // Return the first and last name.
            public void Deconstruct(out string fname, out string lname)
            {
                fname = FirstName;
                lname = LastName;
            }

            public void Deconstruct(out string fname, out string mname, out string lname)
            {
                fname = FirstName;
                mname = MiddleName;
                lname = LastName;
            }

            public void Deconstruct(out string fname, out string lname,
                                    out string city, out string state)
            {
                fname = FirstName;
                lname = LastName;
                city = City;
                state = State;
            }
        }


        [Fact]
        public void WhenUsingExtensionMethod()
        {
            // KeyValuePair has no `Deconstruct` definition.
            var (key, value) = new KeyValuePair<string, int>("one", 1);

            key.Is("one");
            value.Is(1);

            return;
        }

    }

    public static class Extensions
    {
        public static void Deconstruct<T, U>(this KeyValuePair<T, U> pair, out T key, out U value)
        {
            key = pair.Key;
            value = pair.Value;
        }
    }

}
