using System.Collections.Generic;
using Examples.Features.CSharp60.Tests.CollectionInitializers.Fixtures;
using Xunit;

namespace Examples.Features.CSharp60.Tests.CollectionInitializers
{
    public static class Extensions
    {
        public static void Add(this List<Cat> list, string name, int age)
        {
            list.Add(new Cat { Name = name, Age = age });
        }

        public static void Add(this Queue<Cat> queue, Cat entry)
        {
            queue.Enqueue(entry);
        }

        public static void Add(this Queue<Cat> queue, string name, int age)
        {
            queue.Add(new Cat { Name = name, Age = age });
        }

        public static void Add(this IDictionary<string, Cat> dict, Cat entry)
        {
            if (entry == null)
            {
                dict.Add("null", null);
            }
            else
            {
                dict.Add(entry.Name, entry);
            }
        }

        public static void Add(this IDictionary<string, Cat> dict, string name, int age)
        {
            dict.Add(new Cat { Name = name, Age = age });
        }

    }

    /// <summary>
    /// Tests for Extension Add methods in collection initializers in C# 6.0.
    /// </summary>
    public class CollectionInitializersTests
    {
        [Fact]
        public void When_WhenInitializingList_WithExtensionAddMethods_Then_InitialingSuccessful()
        {
            // C# 3.0 or later
            var catsWithCs30 = new List<Cat>
            {
                new Cat { Name = "Sylvester", Age = 8 },
                new Cat { Name = "Whiskers", Age = 2 },
                new Cat { Name = "Sasha", Age = 14 },
                null
            };

            // C# 6.0 or later
            var catsWithCs60 = new List<Cat>
            {
                { "Sylvester", 8 },
                { "Whiskers", 2 },
                { "Sasha", 14 },
                null
            };

            Assert.Equal(catsWithCs30.Count, catsWithCs60.Count);
        }

        [Fact]
        public void When_WhenInitializingQueue_WithExtensionAddMethods_Then_InitialingSuccessful()
        {
            // C# 2.0 or later
            Queue<Cat> catsWithCs20 = new Queue<Cat>();
            catsWithCs20.Enqueue(new Cat { Name = "Sylvester", Age = 8 });
            catsWithCs20.Enqueue(new Cat { Name = "Whiskers", Age = 2 });
            catsWithCs20.Enqueue(new Cat { Name = "Sasha", Age = 14 });
            catsWithCs20.Enqueue(null);

            // C# 6.0 or later
            var catsWithCs60 = new Queue<Cat>
            {
                new Cat { Name = "Sylvester", Age = 8 },
                new Cat { Name = "Whiskers", Age = 2 },
                { "Sasha", 14 },
                null
            };

            Assert.Equal(catsWithCs20.Count, catsWithCs60.Count);
        }

        [Fact]
        public void When_WhenInitializingDictionary_WithExtensionAddMethods_Then_InitialingSuccessful()
        {
            // C# 3.0 or later
            var catsWithCs30 = new Dictionary<string, Cat>()
            {
                { "Sylvester", new Cat { Name = "Sylvester", Age = 8 }},
                { "Whiskers", new Cat { Name = "Whiskers", Age = 2 }},
                { "Sasha", new Cat { Name = "Sasha", Age = 14 }},
                { "null", null }
            };

            // C# 6.0 or later
            var catsWithCs60 = new Dictionary<string, Cat>
            {
                new Cat { Name = "Sylvester", Age = 8 },
                new Cat { Name = "Whiskers", Age = 2 },
                { "Sasha", 14 },
                null
            };

            Assert.Equal(catsWithCs30.Count, catsWithCs60.Count);
        }

    }

}
