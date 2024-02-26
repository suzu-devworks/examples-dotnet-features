using System;
using System.Collections.Generic;

namespace Examples.Features.CS90.ModuleInitializers.Fixtures
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
}
