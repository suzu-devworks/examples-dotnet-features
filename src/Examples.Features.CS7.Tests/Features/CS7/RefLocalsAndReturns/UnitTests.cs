using System;
using System.Linq;
using ChainingAssertion;
using Xunit;

// for C# 7.x

namespace Examples.Features.CS7.RefLocalsAndReturns
{
    /// <summary>
    /// Tests for C# 7.0, Ref locals and returns.
    /// </summary>
    /// <seealso href="https://ufcpp.net/study/csharp/sp_ref.html?p=2#ref-returns" />
    public class RefLocalsAndReturnsTests
    {

        [Fact]
        public void WhenUsingRefReturnFunction()
        {
            // define ref return function.
            ref int Max(ref int a, ref int b)
            {
                if (a < b)
                {
                    return ref b;
                }
                else
                {
                    return ref a;
                }
            }

            var x = 10;
            var y = 20;
            ref var m = ref Max(ref x, ref y);

            // Update over reference to y.
            m = 0;

            x.Is(10);
            y.Is(0);

            return;
        }

        [Fact]
        public void WhenUsingRefReturnFunction_WithArray()
        {
            // define primitive value array.
            var arr = new[] { -1, -1, -1, -1, -1 };

            // use normal function.
            // int ByValue(int[] array, int index) => array[index];

            // for (int i = 0; i < arr.Length; i++)
            // {
            //     // error CS0131 The left-hand side of an assignment must be a variable, property or indexer
            //     ByValue(arr, i) = i;
            // }

            // use ref return function.
            ref int ByRef(int[] array, int index) => ref array[index];

            for (int i = 0; i < arr.Length; i++)
            {
                ByRef(arr, i) = i;
            }

            // assert
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].Is(i);
            }

            return;
        }

        [Fact]
        public void WhenUsingRefReturnFunction_WithStructArray()
        {
            // define ref return function.
            ref Figure Find(int id, Figure[] figures)
            {
                for (int i = 0; i < figures.Length; i++)
                {
                    if (figures[i].Id == id)
                    {
                        // return the storage location, not the value.
                        return ref figures[i];
                    }
                }
                throw new IndexOutOfRangeException($"{nameof(figures)} not found");
            }

            Figure[] array = Enumerable.Range(0, 10).Select(n => new Figure { Id = n, Name = $"fig.{n}" }).ToArray();

            // define ref variable.
            ref Figure found = ref Find(7, array);

            // Updated array over reference to found(ref variable).
            found = new Figure { Id = 999, Name = "new." };
            array[7].Id.Is(999);
            array[7].Name.Is("new.");

            // define normal variable.
            Figure figureByVal = Find(5, array);

            // Not update array.
            figureByVal = new Figure { Id = 888, Name = "not update." };
            array[5].Id.Is(5);
            array[5].Name.Is("fig.5");

            return;
        }

    }

}
