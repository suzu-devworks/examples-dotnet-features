using System.Linq;
using ChainingAssertion;
using Examples.Features.CS73.ReassignRefLocalVariables.Fixtures;
using Xunit;

namespace Examples.Features.CS73.ReassignRefLocalVariables
{
    /// <summary>
    /// Tests for Reassign ref local variables in C# 7.3.
    /// </summary>
    public class ReassignRefLocalVariablesTests
    {
        [Fact]
        public void BasicUsage()
        {
            Figure[] array = Enumerable.Range(0, 10)
                .Select(n => new Figure { Id = n, Name = $"fig.{n}" })
                .ToArray();

            // assign
            ref Figure found = ref array[8];

            found.Is(x => x.Id == 8 && x.Name == "fig.8");

            // reassign.
            // C# 7.2 : error CS8320: Feature 'ref reassignment' is not available in C# 7.2. Please use language version 7.3 or greater.
            found = ref array[2];

            found.Is(x => x.Id == 2 && x.Name == "fig.2");

            return;
        }
    }
}
