using System.Linq;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS73.ExpressionVariablesInMoreLocations
{
    /// <summary>
    /// Tests for Using expression variables in more locations in C# 7.3.
    /// </summary>
    public class ExpressionVariablesInMoreLocationsTests
    {
        [Fact]
        public void WhenDeclaringVariables_WithinQueryExpression()
        {
            var query =
                from s in new[] { "a", "abc", "112", "132", "451", null }
                // C# 7.2 : error CS8320: Feature 'declaration of expression variables in member initializers and queries' is not available in C# 7.2. Please use language version 7.3 or greater.
                where s is string x && x.Length > 1
                // C# 7.2 : error CS8320: Feature 'declaration of expression variables in member initializers and queries' is not available in C# 7.2. Please use language version 7.3 or greater.
                where int.TryParse(s, out var x) && (x % 3) == 0
                select s;

            query.ToArray().IsStructuralEqual(new[] { "132" });

            return;
        }

        [Fact]
        public void WhenDeclaringVariables_WithinMemberInitializers()
        {
            Derived instance;

            instance = new Derived(100);

            instance.Is(x => x.Value == 100 && x.Field == 123 && x.Property == 123);

            instance = new Derived("200");

            instance.Is(x => x.Value == 200 && x.Field == 123 && x.Property == 123);

            instance = new Derived("ABC");

            instance.Is(x => x.Value == int.MinValue && x.Field == 123 && x.Property == 123);

            return;
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
