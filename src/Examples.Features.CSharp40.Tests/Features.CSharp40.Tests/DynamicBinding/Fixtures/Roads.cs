namespace Examples.Features.CSharp40.Tests.DynamicBinding.Fixtures
{
    public interface IRoad
    {
        int Distance { get; }
    }

    public class Sidewalk : IRoad
    {
        public Sidewalk(int distance)
        {
            Distance = distance;
        }

        public int Distance { get; private set; }
    }

    public class Roadway : IRoad
    {
        public Roadway(int distance)
        {
            Distance = distance;
        }

        public int Distance { get; private set; }
    }
}
