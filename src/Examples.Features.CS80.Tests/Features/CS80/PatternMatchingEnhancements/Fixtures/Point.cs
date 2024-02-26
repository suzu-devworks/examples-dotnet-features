namespace Examples.Features.CS80.PatternMatchingEnhancements.Fixtures
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);
    }

}
