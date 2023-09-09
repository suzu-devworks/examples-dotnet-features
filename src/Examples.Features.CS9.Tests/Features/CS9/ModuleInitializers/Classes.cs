using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Examples.Features.CS9.ModuleInitializers
{
    public abstract class BaseClass
    {
        public BaseClass(int id, string name) => (Id, Name) = (id, name);

        public int Id { get; }
        public string Name { get; }

        public abstract string GetMessage();

        public static BaseClass? Create(string type)
        {
            var instance = Factories.TryGetValue(type, out var factory) ? factory() : null;

            return instance;
        }

        protected static void Register(string type, Func<BaseClass> factory)
        {
            Factories.Add(type, factory);
        }

        private readonly static Dictionary<string, Func<BaseClass>> Factories = new();

    }

    public class DerivedA : BaseClass
    {
        public DerivedA() : base(1, "DerivedA")
        {
        }

        public override string GetMessage() => $"This Class is DerivedA.";

        [ModuleInitializer]
        internal static void Init() => Register("TYPE-1", () => new DerivedA());
    }

    public class DerivedB : BaseClass
    {

        public DerivedB() : base(2, "DerivedB")
        {
        }

        public override string GetMessage() => $"DerivedB is this.";

        [ModuleInitializer]
        internal static void Init() => Register("TYPE-2", () => new DerivedB());

    }

}

