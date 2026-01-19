using System.Collections.Generic;
using Examples.Features.CSharp70.Tests.TuplesAndDeconstruction.Fixtures;
using Xunit;

namespace Examples.Features.CSharp70.Tests.TuplesAndDeconstruction
{
    /// <summary>
    /// Tests for Tuples and deconstruction in C# 7.0.
    /// </summary>
    public class DeconstructionTests
    {
        [Fact]
        public void When_DeconstructionWithInstanceMethod_Then_OverloadSolvedIt()
        {
            var person = new Person("John", "Quincy", "Adams", "Boston", "MA");

            var (fName1, lName1) = person;
            Assert.Equal(("John", "Adams"), (fName1, lName1));

            var (fName2, mName2, lName2) = person;
            Assert.Equal(("John", "Quincy", "Adams"), (fName2, mName2, lName2));

            // Can also be used to discard.
            var (fName3, _, _, state3) = person;
            Assert.Equal(("John", "MA"), (fName3, state3));
        }

        [Fact]
        public void WhenUsingExtensionMethod()
        {
            // KeyValuePair has no `Deconstruct` definition.
            var (key, value) = new KeyValuePair<string, int>("one", 1);

            Assert.Equal("one", key);
            Assert.Equal(1, value);
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
