using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace Examples.Features.CS71.DefaultLiteralExpressions
{
    /// <summary>
    /// Tests default literal expressions in C# 7.1.
    /// </summary>
    public class DefaultLiteralExpressionsTests
    {
        [Fact]
        public void When_UsingDefaultLiteralExpressions_Then_IsEasierToWrite()
        {
            // C# 7.0 or earlier
            {
                int numeric = default(int);
                Assert.Equal(0, numeric);

                DateTime dateTime = default(DateTime);
                Assert.Equal(DateTime.MinValue, dateTime);

                CancellationToken token = default(CancellationToken);
                Assert.Equal(CancellationToken.None, token);

                object reference = default(Object);
                Assert.Null(reference);

                IList<string> generics = default(IList<string>);
                Assert.Null(generics);
            }

            // C# 7.1 or later
            {
                int numeric = default;
                Assert.Equal(0, numeric);

                DateTime dateTime = default;
                Assert.Equal(DateTime.MinValue, dateTime);

                CancellationToken token = default;
                Assert.Equal(CancellationToken.None, token);

                object reference = default;
                Assert.Null(reference);

                IList<string> generics = default;
                Assert.Null(generics);
            }
        }

        [Fact]
        public void When_UsingOptionalGenericParameter_Then_ReturnsCorrectDefaultValues()
        {
            // int type parameter
            {
                var actual = InitializeArray<int>(3);

                var expected = new[] { 0, 0, 0 };
                Assert.Equal(expected, actual);
            }

            // bool type parameter
            {
                var actual = InitializeArray<bool>(4);

                var expected = new[] { false, false, false, false };
                Assert.Equal(expected, actual);
            }

            // System.Numerics.Complex type parameter
            {
                var actual = InitializeArray<System.Numerics.Complex>(3, default);

                var expected = new[]{
                    new System.Numerics.Complex(0.0, 0.0),
                    new System.Numerics.Complex(0.0, 0.0),
                    new System.Numerics.Complex(0.0, 0.0),
                    };
                Assert.Equal(expected, actual);
            }

            // string type parameter
            {
                var actual = InitializeArray<string>(2);

                Assert.Equal(2, actual.Length);
                Assert.Null(actual[0]);
                Assert.Null(actual[1]);
            }


            T[] InitializeArray<T>(int length, T initialValue = default)
            {
                if (length < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(length), "Array length must be nonnegative.");
                }

                var array = new T[length];
                for (var i = 0; i < length; i++)
                {
                    array[i] = initialValue;
                }
                return array;
            }
        }

    }
}
