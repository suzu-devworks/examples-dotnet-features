namespace Examples.Features.CS120.RefReadonlyParameters.Fixtures;

public static class Functions
{
    public static void CallWithRefParam(ref int value)
    {
        // ok;
        value = 0;
    }

    public static void CallWithRefReadonlyParam(ref readonly int value)
    {
        // CS8331: Cannot assign to variable or use it as the right hand side of a ref assignment because it is a readonly variable
        // value = 0;
    }

    public static void CallWithInParam(in int value)
    {
        // CS8331: Cannot assign to variable or use it as the right hand side of a ref assignment because it is a readonly variable
        // value = 0;
    }

    public static void CallWithOutParam(out int value)
    {
        // ok.
        value = 0;
    }
}


public static class GenericFunctions
{
    public static void CallWithRefParam<T>(ref T value, T defaultValue = default!)
    {
        // ok;
        value = defaultValue;
    }

    public static void CallWithRefReadonlyParam<T>(ref readonly T value, T defaultValue = default!)
    {
        // CS8331: Cannot assign to variable or use it as the right hand side of a ref assignment because it is a readonly variable
        // value = 0;
    }

    public static void CallWithInParam<T>(in T value, T defaultValue = default!)
    {
        // CS8331: Cannot assign to variable or use it as the right hand side of a ref assignment because it is a readonly variable
        // value = 0;
    }

    public static void CallWithOutParam<T>(out T value, T defaultValue = default!)
    {
        // ok.
        value = defaultValue;
    }
}
