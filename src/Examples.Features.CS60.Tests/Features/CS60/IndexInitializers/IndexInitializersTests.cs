using System;
using System.Collections.Generic;
using ChainingAssertion;
using Examples.Features.CS60.IndexInitializers.Fixtures;
using Xunit;

#pragma warning disable xUnit1014 // MemberData should use nameof operator for member name

namespace Examples.Features.CS60.IndexInitializers
{
    /// <summary>
    /// Tests for Index initializers in C# 6.0.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers" />
    public class IndexInitializersTests
    {
        [Theory]
        [MemberData("MatrixData")]
        public void WhenInitializingMatrix(Matrix initialized, double expected)
        {
            initialized.Det.Is(expected);
            return;
        }

        public static TheoryData<Matrix, double> MatrixData
        {
            get
            {
                return new TheoryData<Matrix, double>
                {
                    { Data.IdentityMatrix , 1.0 }
                };
            }
        }

        private partial class Data
        {
#pragma warning disable format
            public static readonly Matrix IdentityMatrix = new Matrix
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
        }

        [Theory]
        [MemberData("DictionaryData")]
        public void WhenInitializingDictionary(IDictionary<int, StudentName> initialized)
        {
            initialized.Count.Is(3);
            initialized[113].FirstName.Is("Andy");
            initialized[112].FirstName.Is("Dina");
            initialized[111].FirstName.Is("Sachin");
            return;
        }

        public static TheoryData<IDictionary<int, StudentName>> DictionaryData
        {
            get
            {
                return new TheoryData<IDictionary<int, StudentName>>
                {
                    { Data.DictionaryDataWithCS20 },
                    { Data.DictionaryDataWithCS30 },
                    { Data.DictionaryDataWithCS60 },
                };
            }
        }

        private partial class Data
        {
            // C# 2.0 or later
            public static readonly IDictionary<int, StudentName> DictionaryDataWithCS20 = InitializeStatic();

            private static Dictionary<int, StudentName> InitializeStatic()
            {
#pragma warning disable IDE0028 // Collection initialization can be simplified
                Dictionary<int, StudentName> dict = new Dictionary<int, StudentName>();
                dict.Add(111, new StudentName(211, "Sachin", "Karnik"));
                dict.Add(112, new StudentName(317, "Dina", "Salimzianova"));
                dict.Add(113, new StudentName(198, "Andy", "Ruth"));
#pragma warning restore IDE0028 // Collection initialization can be simplified

                return dict;
            }

            // C# 3.0 or later
            public static readonly IDictionary<int, StudentName> DictionaryDataWithCS30 = new Dictionary<int, StudentName>()
            {
                { 111, new StudentName { FirstName = "Sachin", LastName = "Karnik", ID = 211 } },
                { 112, new StudentName { FirstName = "Dina", LastName = "Salimzianova", ID = 317 } },
                { 113, new StudentName { FirstName = "Andy", LastName = "Ruth", ID = 198 } }
            };

            // C# 6.0 or later
            public static readonly IDictionary<int, StudentName> DictionaryDataWithCS60 = new Dictionary<int, StudentName>()
            {
                // C# 5.0 : Feature 'dictionary initializer' is not available in C# 5. Please use language version 6 or greater.
                [111] = new StudentName { FirstName = "Sachin", LastName = "Karnik", ID = 211 },
                [112] = new StudentName { FirstName = "Dina", LastName = "Salimzianova", ID = 317 },
                [113] = new StudentName { FirstName = "Andy", LastName = "Ruth", ID = 198 }
            };

        }

        [Theory]
        [MemberData("DictionaryWithFactoriesData")]
        public void WhenInitializingDictionary_WithFactories(IDictionary<int, Func<StudentName>> initialized)
        {
            initialized.Count.Is(3);
            initialized[113]().FirstName.Is("Andy");
            initialized[112]().FirstName.Is("Dina");
            initialized[111]().FirstName.Is("Sachin");

            return;
        }

        public static TheoryData<IDictionary<int, Func<StudentName>>> DictionaryWithFactoriesData
        {
            get
            {
                return new TheoryData<IDictionary<int, Func<StudentName>>>
                {
                    { Data.FactoryDictionaryData }
                };
            }
        }

        private partial class Data
        {
            // CS6.0 or later
            public static readonly IDictionary<int, Func<StudentName>> FactoryDictionaryData = new Dictionary<int, Func<StudentName>>()
            {
                // C# 5.0 : Feature 'dictionary initializer' is not available in C# 5. Please use language version 6 or greater.
                [111] = () => new StudentName { FirstName = "Sachin", LastName = "Karnik", ID = 211 },
                [112] = () => new StudentName { FirstName = "Dina", LastName = "Salimzianova", ID = 317 },
                [113] = () => new StudentName { FirstName = "Andy", LastName = "Ruth", ID = 198 }
            };
        }

    }
}
