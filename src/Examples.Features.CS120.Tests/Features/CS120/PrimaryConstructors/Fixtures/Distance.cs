namespace Examples.Features.CS120.PrimaryConstructors.Fixtures;

// Magnitude:
//  The mathematical magnitude, determined by Pythagorean theorem.
// ```math
//  AB^2 = (x2 - x1)^2 + (y2 - y1)^2
// ```

// Direction:
//  The angle between the x-axis and the line passing through the O(0, 0) and the coordinate P(x, y).
// ```math
// 	\theta = ATAN2(y, x)
// ```

public readonly struct Distance(double dx, double dy)
{
    public readonly double Magnitude { get; } = Math.Sqrt(dx * dx + dy * dy);
    public readonly double Direction { get; } = Math.Atan2(dy, dx);

    public void Translate(double deltaX, double deltaY)
    {
        // CS9114: A primary constructor parameter of a readonly type cannot be assigned to.
        // dx += deltaX;
        // dy += deltaY;
        throw new NotSupportedException("CS9114");
    }
}

internal struct MutableDistance(double dx, double dy)
{
    public readonly double Magnitude => Math.Sqrt(dx * dx + dy * dy);
    public readonly double Direction => Math.Atan2(dy, dx);

    public void Translate(double deltaX, double deltaY)
    {
        // capture variables.
        dx += deltaX;
        dy += deltaY;
    }

    public MutableDistance() : this(0, 0) { }
}

#pragma warning disable IDE1006 // Naming rule violation
internal record struct RecordDistance(double dx, double dy)
{
    public readonly double Magnitude => Math.Sqrt(dx * dx + dy * dy);
    public readonly double Direction => Math.Atan2(dy, dx);

    public void Translate(double deltaX, double deltaY)
    {
        // property access.
        this.dx += deltaX;
        this.dy += deltaY;
    }

    public RecordDistance() : this(0, 0) { }
}
#pragma warning restore IDE1006 // Naming rule violation


public interface IService
{
    Distance GetDistance();
}

internal class ActionResult<T>(Distance distance)
{
    public Distance Distance => distance;
}

internal class ExampleController(IService service) //: ControllerBase
{
    //[HttpGet]
    public ActionResult<Distance> Get()
    {
        return new(service.GetDistance());
    }

}
