using System;
using System.Linq;
using Examples.Features.CSharp70.Tests.RefLocalsAndReturns.Fixtures;
using Xunit;

namespace Examples.Features.CSharp70.Tests.RefLocalsAndReturns
{
    /// <summary>
    /// Tests for Ref locals and returns in C# 7.0.
    /// </summary>
    public class RefLocalsAndReturnsTests
    {
        [Fact]
        public void When_UpdatingValueWithRefLocal_Then_UpdatesAreReflectedInReference()
        {
            int a = 1;
            ref int alias = ref a;

            Assert.Equal(a, alias);

            // update origin.
            a = 2;
            Assert.Equal(a, alias);

            // update alias.
            alias = 3;
            Assert.Equal(a, alias);
        }


        [Fact]
        public void When_UpdatingArrayWithRefLocal_Then_UpdatesAreReflectedInOriginal()
        {
            int[] array = new[] { 0, 0, 0 };

            ref int element0 = ref array[0];
            // update element.
            element0 = 1;

            ref int element2 = ref array[2];
            // update element.
            element2 = 3;

            var expected = new[] { 1, 0, 3 };
            Assert.True(expected.SequenceEqual(array));
        }

        [Fact]
        public void When_UpdatingArrayWithRefReturnFunction_Then_UpdatesAreReflectedInOriginal()
        {
            int[] array = new[] { 10, 20, 30, 40 };

            ref int found = ref FindFirst(array, e => e == 30);
            // update element.
            found = 0;

            var expected = new[] { 10, 20, 0, 40 };
            Assert.True(expected.SequenceEqual(array));

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
        public void When_UpdatingMatrixWithRefLocalFunction_Then_UpdatesAreReflectedInOriginal()
        {
            int[,] array2d = new[,] {
                { 0, 0, 1 },
                { 0, 1, 0 },
                { 1, 0, 0 },
            };

            Assert.Equal(1, array2d[0, 2]);
            Assert.Equal(1, array2d[1, 1]);
            Assert.Equal(1, array2d[2, 0]);

            ref var found = ref Find(array2d, s => s > 0);
            // update element.
            found = 0;

            Assert.Equal(0, array2d[0, 2]);
            Assert.Equal(1, array2d[1, 1]);
            Assert.Equal(1, array2d[2, 0]);

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
        public void When_UpdateCollectionWithRefReturnMember_Then_UpdatesAreReflectedInOriginal()
        {
            var bc = new BookCollection();

            ref var book = ref bc.GetBookByTitle("Call of the Wild, The");

            Assert.Equal("Call of the Wild, The", book.Title);
            Assert.Equal("Jack London", book.Author);

            // update element reference.
            book = new Book { Title = "Republic, The", Author = "Plato" };

            // search for the same thing again.
            ref var again = ref bc.GetBookByTitle("Call of the Wild, The");

            Assert.Null(again);
        }

        public class BookCollection
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
        public void When_UpdatingStructArrayWithRefReturnFunction_Then_UpdatesAreReflectedInOriginal()
        {
            Figure[] array = Enumerable.Range(0, 10)
                .Select(n => new Figure { Id = n, Name = $"fig.{n}" })
                .ToArray();

            // define ref variable.
            ref Figure byRefValue = ref Find(7, array);

            // Updated array over reference to found(ref variable).
            byRefValue = new Figure { Id = 999, Name = "updated." };

            Assert.Equal(999, array[7].Id);
            Assert.Equal("updated.", array[7].Name);

            // define normal variable.
            Figure byValue = Find(5, array);

            // Not update array.
            byValue = new Figure { Id = 888, Name = "not update." };

            Assert.Equal(5, array[5].Id);
            Assert.Equal("fig.5", array[5].Name);

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
