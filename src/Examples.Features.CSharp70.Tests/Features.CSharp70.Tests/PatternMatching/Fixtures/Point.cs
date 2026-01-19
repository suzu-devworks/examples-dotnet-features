using Xunit.Abstractions;

namespace Examples.Features.CSharp70.Tests.PatternMatching.Fixtures
{
    public class Point : IXunitSerializable
    {
        [System.Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public Point()
        {
        }

        public Point(int x, int y)
            => (X, Y) = (x, y);

        public int X { get; private set; }
        public int Y { get; private set; }

        public void Deserialize(IXunitSerializationInfo info)
        {
            X = info.GetValue<int>("X");
            Y = info.GetValue<int>("Y");
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue("X", X);
            info.AddValue("Y", Y);
        }
    }
}
