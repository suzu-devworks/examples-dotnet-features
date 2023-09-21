namespace Examples.Features.CS7minor2.InModifierParameters
{

    public struct LargeStructure
    {
        public int Values;

        public string DoFunc() { return Values.ToString(); }
    }

    public readonly struct ReadonlyLargeStructure
    {
        public readonly int Values;

        public ReadonlyLargeStructure(int values)
        {
            Values = values;
        }

        public string DoFunc() { return Values.ToString(); }
    }

}
