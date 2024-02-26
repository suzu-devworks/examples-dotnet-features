namespace Examples.Features.CS72.InModifierOnParameters.Fixtures
{
    public struct NoReadOnly
    {
        public int Value;

        public NoReadOnly(int value) => Value = value;

        public void SetValue(int value) => Value = value;
    }

    // C# 7.1 : error CS8302: Feature 'readonly structs' is not available in C# 7.1. Please use language version 7.2 or greater.
    public readonly struct ReadOnly
    {
        // error CS8340: Instance fields of readonly structs must be readonly.
        //public int X;
        public readonly int Value;

        public ReadOnly(int value) => Value = value;

        // error CS0191: A readonly field cannot be assigned to (except in a constructor or init-only setter of the type in which the field is defined or a variable initializer)
        // public void M(int value) => X = value;
        public void SetValue(int value) => _ = value;
    }

}
