using System.Linq;
using Xunit;

namespace Examples.Features.CSharp73.Tests.ExpressionVariablesInMoreLocations
{
    /// <summary>
    /// Tests for Using expression variables in more locations in C# 7.3.
    /// </summary>
    public class ExpressionVariablesInMoreLocationsTests
    {
        [Fact]
        public void When_DeclaringVariablesWithinQueryExpression_Then_VariablesCanBeUsedInFilters()
        {
            var query =
                from s in new[] { "a", "abc", "112", "132", "451", null }
                // C# 7.2 : error CS8320: Feature 'declaration of expression variables in member initializers and queries' is not available in C# 7.2. Please use language version 7.3 or greater.
                where s is string x && x.Length > 1
                // C# 7.2 : error CS8320: Feature 'declaration of expression variables in member initializers and queries' is not available in C# 7.2. Please use language version 7.3 or greater.
                where int.TryParse(s, out var x) && (x % 3) == 0
                select s;

            var expected = new[] { "132" };
            Assert.Equal(expected, query.ToArray());
        }

        [Fact]
        public void When_DeclaringVariablesWithinMemberInitializers_Then_VariablesCanBeUsedInInitializers()
        {
            Derived instance;

            instance = new Derived(100);

            Assert.True(instance.Value == 100 && instance.Field == 123 && instance.Property == 123);

            instance = new Derived("200");

            Assert.True(instance.Value == 200 && instance.Field == 123 && instance.Property == 123);

            instance = new Derived("ABC");

            Assert.True(instance.Value == int.MinValue && instance.Field == 123 && instance.Property == 123);
        }

        private class Base
        {
            public Base(out int x)
            {
                // I didn't know you could use out in the constructor.
                x = int.MinValue;
            }
        }

        private class Derived : Base
        {
            // C# 7.2 : error CS8320: Feature 'declaration of expression variables in member initializers and queries' is not available in C# 7.2. Please use language version 7.3 or greater.
            public Derived(string s) : this(int.TryParse(s, out var x) ? x : -1)
            {
            }

            // C# 7.2 : error CS8320: Feature 'declaration of expression variables in member initializers and queries' is not available in C# 7.2. Please use language version 7.3 or greater.
            public Derived(int a) : base(out var x)
            {
                Value = (a == -1) ? x : a;
            }

            public int Value { get; }

            // C# 7.2 : error CS8320: Feature 'declaration of expression variables in member initializers and queries' is not available in C# 7.2. Please use language version 7.3 or greater.
            public int Field = int.TryParse("123", out var x) ? x : -1;

            // C# 7.2 : error CS8320: Feature 'declaration of expression variables in member initializers and queries' is not available in C# 7.2. Please use language version 7.3 or greater.
            public int Property { get; set; } = int.TryParse("123", out var x) ? x : -1;

        }

    }
}
