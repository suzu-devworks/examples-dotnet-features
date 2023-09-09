namespace Examples.Features.CS7minor1.PatternMatching
{
    public class Base { }

    public class Derived1 : Base
    {
        public string SayDerived1() => nameof(Derived1);
    }

    public class Derived2 : Base
    {
        public string SayDerived2() => nameof(Derived2);
    }

    public class Derived3 : Base
    {
        public string SayDerived3() => nameof(Derived3);
    }

}
