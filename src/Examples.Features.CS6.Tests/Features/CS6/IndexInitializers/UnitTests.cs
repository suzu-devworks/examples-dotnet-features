using System;
using System.Collections.Generic;
using ChainingAssertion;
using Xunit;

// for C# 6.0

namespace Examples.Features.CS6.IndexInitializers
{
    /// <summary>
    /// Tests for C# 6.0, Index initializers.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers" />
    public class UnitTests
    {
        [Fact]
        public void WhenInitializingMatrix()
        {
            // initialized.
            Data.MatrixForCS6AndLater.Det.Is(1.0);

            return;
        }

        private partial class Data
        {
#pragma warning disable format

             // CS6.0 or later
            public static readonly Matrix MatrixForCS6AndLater = new Matrix
            {
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

        [Fact]
        public void WhenInitializingDictionary()
        {
            // initialized.
            Data.DictionaryForCS2AndLater.Count.Is(3);
            Data.DictionaryForCS2AndLater[113].FirstName.Is("Andy");

            Data.DictionaryForCS3AndLater.Count.Is(3);
            Data.DictionaryForCS3AndLater[113].FirstName.Is("Andy");

            Data.DictionaryForCS6AndLater.Count.Is(3);
            Data.DictionaryForCS6AndLater[113].FirstName.Is("Andy");

            return;
        }

        private partial class Data
        {
            // CS2.0 or later
            public static readonly IDictionary<int, StudentName> DictionaryForCS2AndLater = Initialize();

            private static Dictionary<int, StudentName> Initialize()
            {
#pragma warning disable IDE0028 // Collection initialization can be simplified
                Dictionary<int, StudentName> dict = new Dictionary<int, StudentName>();
                dict.Add(111, new StudentName(211, "Sachin", "Karnik"));
                dict.Add(112, new StudentName(317, "Dina", "Salimzianova"));
                dict.Add(113, new StudentName(198, "Andy", "Ruth"));
#pragma warning restore IDE0028 // Collection initialization can be simplified

                return dict;
            }

            // CS3.0 or later
            public static readonly IDictionary<int, StudentName> DictionaryForCS3AndLater = new Dictionary<int, StudentName>()
            {
                { 111, new StudentName { FirstName = "Sachin", LastName = "Karnik", ID = 211 } },
                { 112, new StudentName { FirstName = "Dina", LastName = "Salimzianova", ID = 317 } },
                { 113, new StudentName { FirstName = "Andy", LastName = "Ruth", ID = 198 } }
            };

            // CS6.0 or later
            public static readonly IDictionary<int, StudentName> DictionaryForCS6AndLater = new Dictionary<int, StudentName>()
            {
                [111] = new StudentName { FirstName = "Sachin", LastName = "Karnik", ID = 211 },
                [112] = new StudentName { FirstName = "Dina", LastName = "Salimzianova", ID = 317 },
                [113] = new StudentName { FirstName = "Andy", LastName = "Ruth", ID = 198 }
            };

        }


        [Fact]
        public void WhenInitializingDictionary_WithFactories()
        {
            // initialized.
            Data.Factories.Count.Is(3);
            Data.Factories[113]().FirstName.Is("Andy");

            return;
        }

        private partial class Data
        {
            // CS6.0 or later
            public static readonly IDictionary<int, Func<StudentName>> Factories = new Dictionary<int, Func<StudentName>>()
            {
                [111] = () => new StudentName { FirstName = "Sachin", LastName = "Karnik", ID = 211 },
                [112] = () => new StudentName { FirstName = "Dina", LastName = "Salimzianova", ID = 317 },
                [113] = () => new StudentName { FirstName = "Andy", LastName = "Ruth", ID = 198 }
            };
        }

    }
}
