using System;

namespace Examples.Features.CS40.DynamicBinding.Fixtures
{
    public class Dispatcher
    {
        public static int Dispatch(IRoad x, IRoad y)
        {
            // Switch the actual method to call based on the dynamic type information of multiple instances.
            if ((x is Roadway) && (y is Roadway))
            {
                return new RoadCalculator().Sum((Roadway)x, (Roadway)y);
            }

            if ((x is Roadway) && (y is Sidewalk))
            {
                return new RoadCalculator().Sum((Roadway)x, (Sidewalk)y);
            }

            if ((x is Sidewalk) && (y is Roadway))
            {
                return new RoadCalculator().Sum((Sidewalk)x, (Roadway)y);
            }

            if ((x is Sidewalk) && (y is Sidewalk))
            {
                return new RoadCalculator().Sum((Sidewalk)x, (Sidewalk)y);
            }

            throw new NotSupportedException();
        }

    }
}
