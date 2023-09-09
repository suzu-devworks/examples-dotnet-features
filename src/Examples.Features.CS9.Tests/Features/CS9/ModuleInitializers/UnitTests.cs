using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ChainingAssertion;
using Xunit;

#nullable enable

namespace Examples.Features.CS9.ModuleInitializers
{
    public class ModuleInitializersTests
    {
        [Theory]
        [InlineData("TYPE-1", "This Class is DerivedA.")]
        [InlineData("TYPE-2", "DerivedB is this.")]
        public void TestName(string input, string expected)
        {
            var instance1 = BaseClass.Create(input);
            instance1.IsNotNull();
            instance1!.GetMessage().Is(expected);
        }
    }
}
