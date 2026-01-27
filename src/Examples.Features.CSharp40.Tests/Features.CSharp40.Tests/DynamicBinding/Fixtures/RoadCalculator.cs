namespace Examples.Features.CSharp40.Tests.DynamicBinding.Fixtures
{
    public class RoadCalculator
    {
        public int Sum(Roadway x, Roadway y)
        {
            return x.Distance + y.Distance;
        }

        public int Sum(Roadway x, Sidewalk y)
        {
            return x.Distance - y.Distance;
        }

        public int Sum(Sidewalk x, Roadway y)
        {
            return 1 - x.Distance + y.Distance;
        }

        public int Sum(Sidewalk x, Sidewalk y)
        {
            return 0 - x.Distance - y.Distance;
        }
    }
}
