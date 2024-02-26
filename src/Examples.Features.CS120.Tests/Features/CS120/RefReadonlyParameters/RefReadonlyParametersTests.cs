using Examples.Features.CS120.RefReadonlyParameters.Fixtures;

namespace Examples.Features.CS120.RefReadonlyParameters;

/// <summary>
/// Tests for <c>ref readonly</c> parameters in C# 12.0.
/// </summary>
public class RefReadonlyParametersTests
{
    [Fact]
    public void BasicUsage()
    {
        int value = 100;

        // ref parameter
        {
            Functions.CallWithRefParam(ref value);
        }

        // ref readonly
        {
            Functions.CallWithRefReadonlyParam(ref value);
            Functions.CallWithRefReadonlyParam(in value);

#pragma warning disable CS9192 // Argument should be passed with ref or in keyword.
            Functions.CallWithRefReadonlyParam(value);
#pragma warning restore CS9192

        }

        // in
        {
#pragma warning disable CS9191 // The ref modifier for argument corresponding to in parameter is equivalent to in. Consider using in instead.
            Functions.CallWithInParam(ref value);
#pragma warning restore CS9191

            Functions.CallWithInParam(in value);
            Functions.CallWithInParam(value);
        }

        // out
        {
            Functions.CallWithOutParam(out value);
        }
    }

    [Fact]
    public void UseGeneric()
    {
        string value = "Hello world.";

        // ref parameter
        {
            GenericFunctions.CallWithRefParam(ref value!);
        }

        // ref readonly
        {
            GenericFunctions.CallWithRefReadonlyParam(ref value!);
            GenericFunctions.CallWithRefReadonlyParam(in value);

#pragma warning disable CS9192 // Argument should be passed with ref or in keyword.
            GenericFunctions.CallWithRefReadonlyParam(value);
#pragma warning restore CS9192

        }

        // in
        {
#pragma warning disable CS9191 // The ref modifier for argument corresponding to in parameter is equivalent to in. Consider using in instead.
            GenericFunctions.CallWithInParam(ref value!);
#pragma warning restore CS9191

            GenericFunctions.CallWithInParam(in value);
            GenericFunctions.CallWithInParam(value);
        }

        // out
        {
            GenericFunctions.CallWithOutParam(out value!);
        }
    }
}
