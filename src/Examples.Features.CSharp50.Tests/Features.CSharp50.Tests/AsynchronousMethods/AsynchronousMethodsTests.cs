using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Examples.Features.CSharp50.Tests.AsynchronousMethods
{
    /// <summary>
    /// Tests for Asynchronous methods in C# 5.0.
    /// </summary>
    public class AsynchronousMethodsTests
    {
        [Fact]
        public async Task When_UsingAsyncMethod_Then_CanBeWrittenSmoothlyUsingTask()
        {
            var builder = new StringBuilder();

            await Task.Run(() => builder.AppendLine("async task starting."))
                .ContinueWith((task, state)
                    => builder.AppendFormat("IsCompleted={0}:{1}", task.IsCompleted, state),
                    "state");

            var actual = builder.ToString();
            Assert.StartsWith("async task starting.", actual);
            Assert.EndsWith("IsCompleted=True:state", actual);
        }
    }
}
