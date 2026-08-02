namespace Examples.Features.CSharp150.Tests.ClosedHierarchies;

public class ClosedHierarchiesTests
{
    public closed record class GateState;
    public record class Closed : GateState;
    public record class Open(float Percent) : GateState;

    // In another assembly
    //{
    //  using Examples.Features.CSharp150.Tests.ClosedHierarchies.ClosedHierarchiesTests;
    //
    //  // error CS9382: 'UnitTest1.ExtraGateState': cannot use a closed type 'ClosedHierarchiesTests.GateState' from another assembly as a base type.
    //  // public record OtherAssemblyGateState : GateState { } // Error
    //
    //  // The same-assembly restriction applies only to direct descendants of the closed class.
    //  public record OtherAssemblyClosed : Closed { }  // OK
    //  public record OtherAssemblyOpen() : Open(0.1f) { }  // OK
    //}

    [Fact]
    public void When_DefiningSwitchWhereControllingIsClosedClass_Then_DefaultNotRequired()
    {
        string Describe(GateState state) => state switch
        {
            Closed => "closed",
            Open(var percent) => $"{percent}% open",
            // No warning: every direct descendant of 'GateState' is handled.
        };

        Assert.Equal("closed", Describe(new Closed()));
    }
}

