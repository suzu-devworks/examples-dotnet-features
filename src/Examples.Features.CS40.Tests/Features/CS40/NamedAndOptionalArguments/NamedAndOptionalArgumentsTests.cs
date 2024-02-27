using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS40.NamedAndOptionalArguments
{
    public class NamedAndOptionalArgumentsTests
    {
        [Fact]
        public void WhenUsingNamedArguments()
        {
            // C# 4.0 or later
            // C# 3.0 : error CS8024: Feature 'named argument' is not available in C# 3. Please use language version 4 or greater.
            PrintOrderDetails(orderNum: 31, productName: "Red Mug", sellerName: "Gift Shop");
            PrintOrderDetails(productName: "Red Mug", sellerName: "Gift Shop", orderNum: 31);
            PrintOrderDetails("Gift Shop", orderNum: 31, productName: "Red Mug");
            PrintOrderDetails("Gift Shop", "Red Mug", orderNum: 31);

            return;
        }

        private void PrintOrderDetails(string sellerName, string productName, int orderNum)
        {
            sellerName.Is("Gift Shop");
            productName.Is("Red Mug");
            orderNum.Is(31);
        }

        [Fact]
        public void WhenUsingOptionalArguments()
        {
            // C# 3.0 or later
            ExampleMethod(1, "param string", 99);

            // C# 4.0 or later
            ExampleMethod(2);
            ExampleMethod(3, "param string");

            // C# 3.0 : error CS8024: Feature 'named argument' is not available in C# 3. Please use language version 4 or greater.
            ExampleMethod(4, optionalStr: "param string");
            // C# 3.0 : error CS8024: Feature 'named argument' is not available in C# 3. Please use language version 4 or greater.
            ExampleMethod(5, optionalInt: 99);

            return;
        }

        private void ExampleMethod(int required,
                // C# 3.0 : error CS8024: Feature 'optional parameter' is not available in C# 3. Please use language version 4 or greater.
                string optionalStr = "default string",
                // C# 3.0 : error CS8024: Feature 'optional parameter' is not available in C# 3. Please use language version 4 or greater.
                int optionalInt = 10)
        {
            required.Is(x => 1 <= x && x <= 5);
            optionalStr.Is(x => x == "param string" || x == "default string");
            optionalInt.Is(x => x == 99 || x == 10);
        }

    }
}
