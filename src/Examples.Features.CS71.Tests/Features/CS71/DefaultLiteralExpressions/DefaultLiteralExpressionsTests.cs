using System;
using System.Collections.Generic;
using System.Threading;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS71.DefaultLiteralExpressions
{
    /// <summary>
    /// Tests default literal expressions in c# 7.1.
    /// </summary>
    public class DefaultLiteralExpressionsTests
    {
        [Fact]
        public void BasicUsage()
        {
            // C# 7.0
            {
                int numeric = default(int);
                numeric.Is(0);

                DateTime dateTime = default(DateTime);
                dateTime.Is(DateTime.MinValue);

                CancellationToken token = default(CancellationToken);
                token.Is(CancellationToken.None);

                object reference = default(Object);
                reference.IsNull();

                IList<string> generics = default(IList<string>);
                generics.IsNull();
            }

            // C# 7.1 or later
            {
                int numeric = default;
                numeric.Is(0);

                DateTime dateTime = default;
                dateTime.Is(DateTime.MinValue);

                CancellationToken token = default;
                token.Is(CancellationToken.None);

                object reference = default;
                reference.IsNull();

                IList<string> generics = default;
                generics.IsNull();
            }

            return;
        }

        [Fact]
        public void WhenUsingOptionalGenericParameter()
        {
            var actual1 = InitializeArray<int>(3);

            actual1.IsStructuralEqual(new[] { 0, 0, 0 });

            var actual2 = InitializeArray<bool>(4, default);

            actual2.IsStructuralEqual(new[] { false, false, false, false });

            System.Numerics.Complex fillValue = default;
            var actual3 = InitializeArray(3, fillValue);

            actual3.IsStructuralEqual(new[]{
                new System.Numerics.Complex(0.0, 0.0),
                new System.Numerics.Complex(0.0, 0.0),
                new System.Numerics.Complex(0.0, 0.0),
             });

            return;

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
