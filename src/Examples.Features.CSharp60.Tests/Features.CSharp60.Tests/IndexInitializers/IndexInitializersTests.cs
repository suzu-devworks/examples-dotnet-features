using System.Collections.Generic;
using Examples.Features.CSharp60.Tests.IndexInitializers.Fixtures;
using Xunit;

namespace Examples.Features.CSharp60.Tests.IndexInitializers
{
    /// <summary>
    /// Tests for Index initializers in C# 6.0.
    /// </summary>
    public class IndexInitializersTests
    {
        [Fact]
        public void When_WhenInitializingDictionary_Then_Successful()
        {
            var data = new List<IDictionary<int, StudentName>>()
            {
                DictionaryDataWithCs20,
                DictionaryDataWithCs30,
                DictionaryDataWithCs60,
            };

            foreach (var dict in data)
            {
                Assert.Equal(3, dict.Count);
                Assert.Equal("Andy", dict[113].FirstName);
                Assert.Equal("Dina", dict[112].FirstName);
                Assert.Equal("Sachin", dict[111].FirstName);
            }
        }

        // C# 2.0 or later
        public static readonly IDictionary<int, StudentName> DictionaryDataWithCs20 = InitializeStatic();

        private static IDictionary<int, StudentName> InitializeStatic()
        {
#pragma warning disable IDE0028 // Collection initialization can be simplified
            IDictionary<int, StudentName> dict = new Dictionary<int, StudentName>();
            dict.Add(111, new StudentName(211, "Sachin", "Karnik"));
            dict.Add(112, new StudentName(317, "Dina", "Salimzianova"));
            dict.Add(113, new StudentName(198, "Andy", "Ruth"));
            // spell-checker: words Salimzianova Sachin Karnik
#pragma warning restore IDE0028 // Collection initialization can be simplified

            return dict;
        }

        // C# 3.0 or later
        public static readonly IDictionary<int, StudentName> DictionaryDataWithCs30 = new Dictionary<int, StudentName>()
        {
            { 111, new StudentName { FirstName = "Sachin", LastName = "Karnik", ID = 211 } },
            { 112, new StudentName { FirstName = "Dina", LastName = "Salimzianova", ID = 317 } },
            { 113, new StudentName { FirstName = "Andy", LastName = "Ruth", ID = 198 } }
        };

        // C# 6.0 or later
        public static readonly IDictionary<int, StudentName> DictionaryDataWithCs60 = new Dictionary<int, StudentName>()
        {
            // C# 5.0 : Feature 'dictionary initializer' is not available in C# 5. Please use language version 6 or greater.
            [111] = new StudentName { FirstName = "Sachin", LastName = "Karnik", ID = 211 },
            [112] = new StudentName { FirstName = "Dina", LastName = "Salimzianova", ID = 317 },
            [113] = new StudentName { FirstName = "Andy", LastName = "Ruth", ID = 198 }
        };

        [Fact]
        public void When_InitializingMatrix_WithIndexInitialized_Then_Successful()
        {
#pragma warning disable format
            var identity = new Matrix
            {
                // An indexer containing a setter can be used as one of the expressions in an object initializer.
                // C# 5.0 : Feature 'dictionary initializer' is not available in C# 5. Please use language version 6 or greater.
                [0, 0] = 1.0,
                [0, 1] = 0.0,
                [0, 2] = 0.0,

                [1, 0] = 0.0,
                [1, 1] = 1.0,
                [1, 2] = 0.0,

                [2, 0] = 0.0,
                [2, 1] = 0.0,
                [2, 2] = 1.0,
            };
            #pragma warning restore format

            Assert.Equal(1.0, identity.Det);
        }

    }

}
