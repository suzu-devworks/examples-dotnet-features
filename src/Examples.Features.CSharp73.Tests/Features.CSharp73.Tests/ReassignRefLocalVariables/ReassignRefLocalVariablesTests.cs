using System.Linq;
using Examples.Features.CSharp73.Tests.ReassignRefLocalVariables.Fixtures;
using Xunit;

namespace Examples.Features.CSharp73.Tests.ReassignRefLocalVariables
{
    /// <summary>
    /// Tests for Reassign ref local variables in C# 7.3.
    /// </summary>
    public class ReassignRefLocalVariablesTests
    {
        [Fact]
        public void When_ReassigningRefLocalVariable_Then_ReferenceIsUpdated()
        {
            Figure[] array = Enumerable.Range(0, 10)
                .Select(n => new Figure { Id = n, Name = $"fig.{n}" })
                .ToArray();

            // assign
            ref Figure found = ref array[8];

            Assert.True(found.Id == 8 && found.Name == "fig.8");

            // reassign.
            // C# 7.2 : error CS8320: Feature 'ref reassignment' is not available in C# 7.2. Please use language version 7.3 or greater.
            found = ref array[2];

            Assert.True(found.Id == 2 && found.Name == "fig.2");
        }
    }
}
