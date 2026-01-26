namespace Examples.Features.CSharp100.Tests.ExtendedDeconstruction.Fixtures
{
    public class Point
    {
        public int X { get; init; }
        public int Y { get; init; }

        public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);
    }
}
