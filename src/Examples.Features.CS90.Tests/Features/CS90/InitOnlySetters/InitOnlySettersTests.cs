using ChainingAssertion;
using Examples.Features.CS90.InitOnlySetters.Fixtures;
using Xunit;

namespace Examples.Features.CS90.InitOnlySetters
{
    /// <summary>
    /// Tests for Init only setters in C# 9.0.
    /// </summary>
    public class InitOnlySettersTests
    {

        [Fact]
        public void BasicUsage()
        {
            var person1 = new PersonOfPrivateSetter();
            // error CS0272: The property or indexer '...Id' cannot be used in this context because the set accessor is inaccessible.
            //person1.Id = "123";
            //person1.Name = "HOGE";

            person1.Id.IsNull();
            person1.Name.IsNull();

            // error CS0272: The property or indexer '...Id' cannot be used in this context because the set accessor is inaccessible.
            //person1 = new PrivateSetterPerson() { Id = "123", Name = "HOGE" };

#pragma warning disable IDE0017 // Object initialization can be simplified
            var person2 = new PersonOfInternalSetter();
#pragma warning restore IDE0017

            person2.Id = "123";
            person2.Name = "HOGE";

            person2.Id.Is("123");
            person2.Name.Is("HOGE");

            person2 = new PersonOfInternalSetter() { Id = "456", Name = "Foo" };

            person2.Id.Is("456");
            person2.Name.Is("Foo");

            // Re-writable with internal scope.
            person2.Id = "789";
            person2.Name = "Bar";
            person2.Id.Is("789");
            person2.Name.Is("Bar");


            var person3 = new PersonOfInitSetter();
            // error CS8852: Init-only property or indexer '...Id' can only be assigned in an object initializer, or on 'this' or 'base' in an instance constructor or an 'init' accessor.
            //person3.Id = "123";
            //person3.Name = "HOGE";

            person3.Id.IsNull();
            person3.Name.IsNull();

            person3 = new PersonOfInitSetter() { Id = "456", Name = "Foo" };

            person3.Id.Is("456");
            person3.Name.Is("Foo");

            // error CS8852: Init-only property or indexer '...Id' can only be assigned in an object initializer, or on 'this' or 'base' in an instance constructor or an 'init' accessor.
            //person3.Id = "789";
            //person3.Name = "Bar";

            return;
        }

    }
}
