using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS72.NonTrailingNamedArguments
{
    /// <summary>
    /// Tests for Non-trailing named arguments in C# 7.2.
    /// </summary>
    public class NonTrailingNamedArgumentsTests
    {
        [Fact]
        public void BasicUsage()
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

            return;

            void PrintOrderDetails(string sellerName, int orderNum, string productName)
            {
                sellerName.Is("Gift Shop");
                orderNum.Is(31);
                productName.Is("Red Mug");

                return;
            }
        }
    }
}
