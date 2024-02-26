using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Examples.Features.CS50.AsynchronousMethods
{
    /// <summary>
    /// Tests for Asynchronous methods in C# 5.0.
    /// </summary>
    public class AsynchronousMethodsTests
    {
        [Fact]
        public async Task BasicUsage()
        {
            var builder = new StringBuilder();

            await Task.Run(() => builder.AppendLine("async task starting."))
                .ContinueWith((task, state)
                    => builder.AppendFormat("IsCompleted={0}:{1}", task.IsCompleted, state),
                    "state");

            builder.ToString().Should()
                .StartWith("async task starting.")
                .And.EndWith("IsCompleted=True:state");

            return;
        }
    }
}
