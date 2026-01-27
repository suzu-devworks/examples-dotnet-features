using Xunit;

namespace Examples.Features.CSharp40.Tests.NamedAndOptionalArguments
{
    public class NamedAndOptionalArgumentsTests
    {
        [Fact]
        public void When_UsingNamedArguments_Then_CanBeCalledInAnyOrder()
        {
            // C# 3.0 : error CS8024: Feature 'named argument' is not available in C# 3. Please use language version 4 or greater.
            // C# 4.0 or later
            PrintOrderDetails(orderNum: 31, productName: "Red Mug", sellerName: "Gift Shop");
            PrintOrderDetails(productName: "Red Mug", sellerName: "Gift Shop", orderNum: 31);
            PrintOrderDetails("Gift Shop", orderNum: 31, productName: "Red Mug");
            PrintOrderDetails("Gift Shop", "Red Mug", orderNum: 31);
        }

        private static void PrintOrderDetails(string sellerName, string productName, int orderNum)
        {
            Assert.Equal("Gift Shop", sellerName);
            Assert.Equal("Red Mug", productName);
            Assert.Equal(31, orderNum);
        }

        [Fact]
        public void When_UsingOptionalArguments_Then_CanBeOmitted()
        {
            // C# 3.0 or later
            ExampleMethod(1, "param string", 99);

            // C# 4.0 or later
            ExampleMethod(2);
            ExampleMethod(3, "param string");
            ExampleMethod(4, optionalStr: "param string");
            ExampleMethod(5, optionalInt: 99);
        }

        private static void ExampleMethod(int required, string optionalStr = "default string", int optionalInt = 10)
        {
            Assert.True(1 <= required && required <= 5);
            Assert.True(optionalStr == "param string" || optionalStr == "default string");
            Assert.True(optionalInt == 99 || optionalInt == 10);
        }

    }
}
