using System;

namespace Examples.Features.CS4.DynamicBinding
{
    public class Calculator
    {
        public int Add(int x, int y) { return x + y; }
        public int Sub(int x, int y) { return x - y; }
        public int Mul(int x, int y) { return x * y; }
        public int Div(int x, int y) { return x / y; }
    }

    public class DuckAnimal
    {
        public int MoveLegs(IRoad road)
        {
            return (int)Math.Ceiling(road.Distance / 60.0);
        }
    }

    public class DuckToys
    {
        public int MoveLegs(IRoad road)
        {
            return (int)Math.Ceiling(road.Distance / 15.0);
        }
    }

    public interface IRoad
    {
        int Distance { get; }
    }

    public class Sidewalk : IRoad
    {
        public Sidewalk(int distance)
        {
            this.Distance = distance;
        }

        public int Distance { get; private set; }
    }

    public class Roadway : IRoad
    {
        public Roadway(int distance)
        {
            this.Distance = distance;
        }

        public int Distance { get; private set; }
    }

    public static class RoadCalculator
    {
        public static int Sum(Roadway x, Roadway y)
        {
            return x.Distance + y.Distance;
        }

        public static int Sum(Roadway x, Sidewalk y)
        {
            return x.Distance - y.Distance;
        }

        public static int Sum(Sidewalk x, Roadway y)
        {
            return 0 - x.Distance + y.Distance;
        }

        public static int Sum(Sidewalk x, Sidewalk y)
        {
            return 0 - x.Distance - y.Distance;
        }
    }

}
