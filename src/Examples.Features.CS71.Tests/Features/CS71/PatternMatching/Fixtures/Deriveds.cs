namespace Examples.Features.CS71.PatternMatching.Fixtures
{
    public class Based { }

    public class Derived1 : Based
    {
        public string SayDerived1() => nameof(Derived1);
    }

    public class Derived2 : Based
    {
        public string SayDerived2() => nameof(Derived2);
    }

    public class Derived3 : Based
    {
        public string SayDerived3() => nameof(Derived3);
    }

}
