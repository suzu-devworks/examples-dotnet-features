using ChainingAssertion;
using Xunit;

#nullable enable

namespace Examples.Features.CS9.InitOnlySetters
{
    /// <summary>
    /// Tests for C# 9.0 Init only setters.
    /// </summary>
    public class UnitTests
    {

        [Fact]
        public void WhenSomeInitialize()
        {
            var person1 = new PrivateSetterPerson();
            // CS0272 The property or indexer '...Id' cannot be used in this context because the set accessor is inaccessible.
            //person1.Id = "123";
            //person1.Name = "HOGE";

            person1.Id.IsNull();
            person1.Name.IsNull();

            // CS0272 The property or indexer '...Id' cannot be used in this context because the set accessor is inaccessible.
            //person1 = new PrivateSetterPerson() { Id = "123", Name = "HOGE" };


#pragma warning disable IDE0017 // Object initialization can be simplified
            var person2 = new InternalSetterPerson();
#pragma warning restore IDE0017 // Object initialization can be simplified

            person2.Id = "123";
            person2.Name = "HOGE";

            person2.Id.Is("123");
            person2.Name.Is("HOGE");

            person2 = new InternalSetterPerson() { Id = "456", Name = "Foo" };

            person2.Id.Is("456");
            person2.Name.Is("Foo");

            // Rewritable with internal scope.
            person2.Id = "789";
            person2.Name = "Bar";
            person2.Id.Is("789");
            person2.Name.Is("Bar");


            var person3 = new InitOnlyPerson();
            // CS8852 Init-only property or indexer '...Id' can only be assigned in an object initializer, or on 'this' or 'base' in an instance constructor or an 'init' accessor.
            //person3.Id = "123";
            //person3.Name = "HOGE";

            person3.Id.IsNull();
            person3.Name.IsNull();

            person3 = new InitOnlyPerson() { Id = "456", Name = "Foo" };

            person3.Id.Is("456");
            person3.Name.Is("Foo");

            // CS8852 Init-only property or indexer '...Id' can only be assigned in an object initializer, or on 'this' or 'base' in an instance constructor or an 'init' accessor.
            //person3.Id = "789";
            //person3.Name = "Bar";

            return;
        }

    }
}
