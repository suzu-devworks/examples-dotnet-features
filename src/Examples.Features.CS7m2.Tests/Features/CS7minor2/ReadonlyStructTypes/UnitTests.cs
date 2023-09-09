using System;
using ChainingAssertion;
using Xunit;

// for C# 7.2

namespace Examples.Features.CS7minor2.ReadonlyStructTypes
{
    /// <summary>
    /// Tests for C# 7.2, ref readonly.
    /// </summary>
    /// <seealso href="https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/builtin-types/struct#readonly-struct" />
    public class UnitTests
    {

        [Fact]
        public void WhenUsingReadonlyStruct()
        {
            var point1 = new Coords(100, 100);
            //point1.X = 10;  // NG CS0191

            point1 = new Coords(200, 200);
            point1.ToString().Is("(200, 200)");

            var point2 = new Coords(180, 120);
            point2.GetDistance(point1).Is(x => 82.46 < x && x < 82.47);

            return;
        }


        private readonly struct Coords
        {
            public Coords(double x, double y)
            {
                X = x;
                Y = y;
            }

            public double X { get; }
            public double Y { get; }

            public override string ToString() => $"({X}, {Y})";

            public double GetDistance(in Coords other)
                => Math.Sqrt(Math.Pow((other.X - this.X), 2.0) + Math.Pow((other.Y - this.Y), 2.0));

        }

    }
}
