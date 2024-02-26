using System;

namespace Examples.Features.CS40.DynamicBinding.Fixtures
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
