using ChainingAssertion;
using Xunit;

// for C# 7.2

namespace Examples.Features.CS7minor2.RefReadonlyReturnMethods
{
    /// <summary>
    /// Tests for C# 7.2, Use the ref readonly modifier on method returns.
    /// </summary>
    /// <seealso href="https://docs.microsoft.com/ja-jp/dotnet/csharp/write-safe-efficient-code#use-ref-readonly-return-statements" />
    public class UnitTests
    {

        [Fact]
        public void WhenUsingRefReadonlyReturnMember()
        {
            // define normal struct.
            var point1 = new SamplePoint(1, 2, 3);

            point1.X.Is(1);
            point1.X = 10;
            point1.X.Is(10);

            // define ref struct.
            var point2 = new SamplePoint(1, 2, 3);
            ref var point2r = ref point2;

            point2r.X.Is(1);
            point2r.X = 10;
            point2r.X.Is(10);
            point2.X.Is(10);

            // define ref readonly struct.
            var point3 = new SamplePoint(1, 2, 3);
            ref readonly var point3r = ref point3;

            point3.X.Is(1);
            // error CS0131 The left-hand side of an assignment must be a variable, property or indexer
            //point3r.X = 10;
            point3.X.Is(1);

            return;
        }


        private struct SamplePoint
        {
            public double X;
            public double Y;
            public double Z;

            public SamplePoint(double x, double y, double z)
            {
                X = x;
                Y = y;
                Z = z;
            }

        }

    }

}
