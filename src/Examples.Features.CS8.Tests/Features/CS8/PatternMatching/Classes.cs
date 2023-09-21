namespace Examples.Features.CS8.PatternMatching
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);
    }

    public class Segment
    {
        public Point? Start { get; set; }
        public Point? End { get; set; }
    }

    public abstract class Shape
    {
        public Point? Point { get; set; }
    }

    public class Rectangle : Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public void Deconstruct(out int width, out int height, out Point? point)
        {
            width = Width;
            height = Height;
            point = Point;
        }
    }


    public class StateKey
    {
        public bool IsValid { get; set; }
    }

    public enum State
    {
        Closed,
        Opened,
        Locked
    }

    public enum Operation
    {
        Open,
        Close,
        Lock,
    }

}
