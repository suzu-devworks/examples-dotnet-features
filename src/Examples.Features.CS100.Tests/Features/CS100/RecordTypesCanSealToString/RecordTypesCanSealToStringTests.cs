namespace Examples.Features.CS100.RecordTypesCanSealToString;

/// <summary>
/// Tests for Record types can seal <c>ToString()</c> in C# 10.0.
/// </summary>
public class RecordTypesCanSealToStringTests
{
    [Fact]
    public void BasicUsage()
    {
        var normal = new NormalRecord(123);
        var target = new SealedRecord(123);
        var derived = new DerivedRecord(123, 456);

        var original = normal.ToString();

        var actual1 = target.ToString();
        var actual2 = derived.ToString();

        using (new AssertionScope())
        {
            original.Should().Be("NormalRecord { Value = 123 }");

            actual1.Should().Be("(・ω≦) ﾃﾍﾍﾟﾛ - SealedRecord: { Value = 123 }");
            actual2.Should().Be("(・ω≦) ﾃﾍﾍﾟﾛ - DerivedRecord: { Value = 123 }");
        }

        return;
    }

    private record NormalRecord(int Value);

    private record SealedRecord(int Value)
    {
        // C# 9.0 : error CS8773: Feature 'sealed ToString in record' is not available in C# 9.0. Please use language version 10.0 or greater.
        public sealed override string ToString()
            => $"(・ω≦) ﾃﾍﾍﾟﾛ - {GetType().Name}: {{ Value = {Value} }}";
    }

    private record DerivedRecord(int Value1, int Value2) : SealedRecord(Value1)
    {
        // error CS0239: 'RecordTypesCanSealToStringTests.DerivedRecord.ToString()': cannot override inherited member 'RecordTypesCanSealToStringTests.SealedRecord.ToString()' because it is sealed
        // public override string ToString()
        //     => base.ToString();
    }

}

