using System;
using System.Linq;
using ChainingAssertion;
using Examples.Features.CS70.RefLocalsAndReturns.Fixtures;
using Xunit;

namespace Examples.Features.CS70.RefLocalsAndReturns
{
    /// <summary>
    /// Tests for Ref locals and returns in C# 7.0.
    /// </summary>
    public class RefLocalsAndReturnsTests
    {

        [Fact]
        public void WhenUsingRefLocals()
        {
            int a = 1;
            ref int alias = ref a;

            a.Is(1);
            alias.Is(a);

            // update origin.
            a = 2;

            a.Is(2);
            alias.Is(a);

            // update alias.
            alias = 3;

            a.Is(3);
            alias.Is(a);

            return;
        }

        [Fact]
        public void WhenUsingRefLocals_WithArray()
        {
            int[] xs = new[] { 0, 0, 0 };

            ref int element0 = ref xs[0];
            element0 = 1;

            ref int element2 = ref xs[2];
            element2 = 3;

            xs.IsStructuralEqual(new[] { 1, 0, 3 });

            return;
        }

        [Fact]
        public void WhenUsingRefReturnFunction()
        {
            int[] xs = new[] { 10, 20, 30, 40 };

            ref int found = ref FindFirst(xs, s => s == 30);
            found = 0;

            xs.IsStructuralEqual(new[] { 10, 20, 0, 40 });

            return;

            ref int FindFirst(int[] numbers, Func<int, bool> predicate)
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (predicate(numbers[i]))
                    {
                        return ref numbers[i];
                    }
                }
                throw new InvalidOperationException("No element satisfies the given condition.");
            }
        }

        [Fact]
        public void WhenUsingRefReturnFunction_WithMatrix()
        {
            int[,] array2d = new[,] {
                { 0, 0, 1 },
                { 0, 1, 0 },
                { 1, 0, 0 },
            };

            array2d[0, 2].Is(1);
            array2d[1, 1].Is(1);
            array2d[2, 0].Is(1);

            ref var found = ref Find(array2d, s => s > 0);
            found = 0;

            array2d[0, 2].Is(0);
            array2d[1, 1].Is(1);
            array2d[2, 0].Is(1);

            return;

            ref int Find(int[,] matrix, Func<int, bool> predicate)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (predicate(matrix[i, j]))
                        {
                            return ref matrix[i, j];
                        }
                    }
                }
                throw new InvalidOperationException("Not found");
            }

        }

        [Fact]
        public void WhenUsingRefReturnFunction_WithClassArray()
        {
            var bc = new BookCollection();

            ref var found = ref bc.GetBookByTitle("Call of the Wild, The");

            found.Is(x => x.Title == "Call of the Wild, The" && x.Author == "Jack London");

            found = new Book { Title = "Republic, The", Author = "Plato" };

            // search for the same thing again.
            ref var book = ref bc.GetBookByTitle("Call of the Wild, The");

            book.IsNull();

        }

        private class BookCollection
        {
            private readonly Book[] _books = {
                new Book { Title = "Call of the Wild, The", Author = "Jack London" },
                new Book { Title = "Tale of Two Cities, A", Author = "Charles Dickens" }
            };

            private Book _dummy = null;

            public ref Book GetBookByTitle(string title)
            {
                for (int ctr = 0; ctr < _books.Length; ctr++)
                {
                    if (title == _books[ctr].Title)
                        return ref _books[ctr];
                }
                return ref _dummy;
            }
        }

        [Fact]
        public void WhenUsingRefReturnFunction_WithStructArray()
        {
            Figure[] array = Enumerable.Range(0, 10)
                .Select(n => new Figure { Id = n, Name = $"fig.{n}" })
                .ToArray();

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
        }

    }

}
