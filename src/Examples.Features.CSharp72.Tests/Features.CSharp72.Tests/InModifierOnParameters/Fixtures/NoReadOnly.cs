namespace Examples.Features.CSharp72.Tests.InModifierOnParameters.Fixtures
{
    public struct NoReadOnly
    {
        public int Value;

        public NoReadOnly(int value) => Value = value;

        public void SetValue(int value) => Value = value;
    }
}
