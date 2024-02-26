namespace Examples.Features.CS72.InModifierOnParameters.Fixtures
{
    public struct Counter
    {
        private int _count;

        public int IncrementedCount => ++_count;
    }

}
