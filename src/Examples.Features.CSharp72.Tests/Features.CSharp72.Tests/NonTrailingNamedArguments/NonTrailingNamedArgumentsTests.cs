using Xunit;

namespace Examples.Features.CSharp72.Tests.NonTrailingNamedArguments
{
    /// <summary>
    /// Tests for Non-trailing named arguments in C# 7.2.
    /// </summary>
    public class NonTrailingNamedArgumentsTests
    {
        [Fact]
        public void When_UsingNonTrailingNamedArguments_Then_AllMethodCallVariationsWork()
        {
            // The method can be called in the normal way, by using positional arguments.
            PrintOrderDetails("Gift Shop", 31, "Red Mug");

            // C# 7.1
            PrintOrderDetails(orderNum: 31, productName: "Red Mug", sellerName: "Gift Shop");
            PrintOrderDetails(productName: "Red Mug", sellerName: "Gift Shop", orderNum: 31);
            PrintOrderDetails(productName: "Red Mug", orderNum: 31, sellerName: "Gift Shop");
            PrintOrderDetails("Gift Shop", 31, productName: "Red Mug");
            PrintOrderDetails("Gift Shop", orderNum: 31, productName: "Red Mug");

            // C# 7.2 or later
            // C# 7.1 : error CS1738: Named argument specifications must appear after all fixed arguments have been specified. Please use language version 7.2 or greater to allow non-trailing named arguments.
            PrintOrderDetails(sellerName: "Gift Shop", 31, "Red Mug");
            PrintOrderDetails(sellerName: "Gift Shop", orderNum: 31, "Red Mug");
            PrintOrderDetails("Gift Shop", orderNum: 31, "Red Mug");

            // error CS8323: Named argument 'orderNum' is used out-of - position but is followed by an unnamed argument
            //PrintOrderDetails(orderNum: 31, "Red Mug", "Gift Shop");
            // error CS8323: Named argument 'productName' is used out-of - position but is followed by an unnamed argument
            //PrintOrderDetails(productName: "Red Mug", 31, sellerName: "Gift Shop");
            // error CS8323: Named argument 'orderNum' is used out-of - position but is followed by an unnamed argument
            //PrintOrderDetails(orderNum: 31, productName: "Red Mug", "Gift Shop");

            void PrintOrderDetails(string sellerName, int orderNum, string productName)
            {
                Assert.Equal("Gift Shop", sellerName);
                Assert.Equal(31, orderNum);
                Assert.Equal("Red Mug", productName);
            }
        }
    }
}
