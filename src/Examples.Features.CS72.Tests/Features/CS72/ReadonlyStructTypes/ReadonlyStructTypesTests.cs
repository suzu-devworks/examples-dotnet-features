using System;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS72.ReadonlyStructTypes
{
    /// <summary>
    /// Tests for Declare <c>readonly</c> struct types in C# 7.2.
    /// </summary>
    /// <seealso href="https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/builtin-types/struct#readonly-struct" />
    public class ReadonlyStructTypesTests
    {
        [Fact]
        public void BasicUsage()
        {
            var point1 = new Coords(100.0, 100.0);

            // error CS0200: Property or indexer 'property' cannot be assigned to -- it is read only
            // point1.X = 0;
            // point1.Y = 0;

            point1.Is(p => p.X == 100.0 && p.Y == 100.0);

            // overwrite!
            point1 = new Coords(200.0, 200.0);

            point1.Is(p => p.X == 200.0 && p.Y == 200.0);

            // By the way, find the distance between two points.
            var distance = point1.GetDistance(new Coords(180.0, 120.0));
            distance.Is(x => 82.46 < x && x < 82.47);

            return;
        }

        // C# 7.1 : error CS8302: Feature 'readonly structs' is not available in C# 7.1. Please use language version 7.2 or greater.
        private readonly struct Coords
        {
            public readonly string SystemOfCoordinates;

            // error CS8340: Instance fields of readonly types must be readonly.
            // private int _dimension;

            public Coords(double x, double y)
            {
                X = x;
                Y = y;

                SystemOfCoordinates = "Cartesian";
            }

            public double X { get; }
            public double Y { get; }

            // error CS8341: Auto-implemented instance properties in readonly types must be readonly.
            // public double Z { get; set; }

            public override string ToString() => $"({X}, {Y})";

            // C# 7.1 : error CS8302: Feature 'readonly references' is not available in C# 7.1. Please use language version 7.2 or greater.
            public double GetDistance(in Coords other)
                => Math.Sqrt(Math.Pow((other.X - this.X), 2.0) + Math.Pow((other.Y - this.Y), 2.0));

        }
    }
}
