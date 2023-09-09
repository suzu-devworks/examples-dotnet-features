using System.Linq;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS7minor3.RefLocalVariables
{
    /// <summary>
    /// Tests for C# 7.3 Reassign ref local variables.
    /// </summary>
    public class UnitTests
    {
        [Fact]
        public void WhenReassigned()
        {

            Figure[] array = Enumerable.Range(0, 10).Select(n => new Figure { Id = n, Name = $"fig.{n}" }).ToArray();
            ref Figure found = ref array[8];

            // reassign.
            found = ref array[2];

            // Update over reference to found.
            found = new Figure { Id = 999, Name = "new." };

            array[2].Id.Is(999);
            array[2].Name.Is("new.");

            return;
        }

    }

}
