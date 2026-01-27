using System;

namespace Examples.Features.CSharp40.Tests.DynamicBinding.Fixtures
{
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
}
