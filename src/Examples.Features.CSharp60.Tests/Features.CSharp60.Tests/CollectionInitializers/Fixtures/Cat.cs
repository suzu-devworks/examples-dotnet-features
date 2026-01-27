namespace Examples.Features.CSharp60.Tests.CollectionInitializers.Fixtures
{
    public class Cat
    {
        // Automatically implemented properties.
        public int Age { get; set; }
        public string Name { get; set; }

        public Cat()
        {
        }

        public Cat(string name)
        {
            this.Name = name;
        }
    }
}
