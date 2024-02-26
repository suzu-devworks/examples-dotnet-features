using Xunit;

namespace Examples.Features.CS72.PrivateProtectedAccessModifier
{
    /// <summary>
    /// Tests for <c>private protected</c> access modifier in C# 7.2.
    /// </summary>
    public class PrivateProtectedAccessModifierTests
    {
        [Fact]
        public void BasicUsage()
        {
            // the current assembly access.

            _ = new Based.PublicClass();

            // error CS0122: 'PrivateProtectedAccessModifierTests.Based.ProtectedClass' is inaccessible due to its protection level
            //_ = new Based.ProtectedClass();

            _ = new Based.InternalClass();

            _ = new Based.ProtectedInternalClass();

            // error CS0122: 'PrivateProtectedAccessModifierTests.Based.PrivateClass' is inaccessible due to its protection level
            //_ = new Based.PrivateClass();

            // error CS0122: 'PrivateProtectedAccessModifierTests.Based.UnspecifiedClass' is inaccessible due to its protection level
            //_ = new Based.UnspecifiedClass();

            // error CS0122: 'PrivateProtectedAccessModifierTests.Based.PrivateProtected' is inaccessible due to its protection level[/ workspaces / examples - dotnet - features / src / Examples.Features.CS72.Tests / Examples.Features.CS72.Tests.csproj]
            //_ = new Based.PrivateProtected();

            var instance = new Derived();

            instance.DoBaseAccess();

            instance.DoDerivedAccess();

            return;
        }

        public class Based
        {
            public class PublicClass { }

            protected class ProtectedClass { }

            internal class InternalClass { }

            protected internal class ProtectedInternalClass { };

            private class PrivateClass { }

            class UnspecifiedClass { }

            // C# 7.2 or later
            // C# 7.1 : error CS8302: Feature 'private protected' is not available in C# 7.1. Please use language version 7.2 or greater.
            private protected class PrivateProtected { }

            public void DoBaseAccess()
            {
                // the containing class access.

                _ = new PublicClass();

                _ = new ProtectedClass();

                _ = new InternalClass();

                _ = new ProtectedInternalClass();

                _ = new PrivateClass();

                _ = new UnspecifiedClass();

                _ = new PrivateProtected();

                return;
            }

        }

        public class Derived : Based
        {
            public void DoDerivedAccess()
            {
                // types derived from the containing class within the current assembly access.

                _ = new PublicClass();

                _ = new ProtectedClass();

                _ = new InternalClass();

                _ = new ProtectedInternalClass();

                // error CS0122: 'PrivateProtectedAccessModifierTests.Based.PrivateClass' is inaccessible due to its protection level
                //_ = new PrivateClass();

                // error CS0122: 'PrivateProtectedAccessModifierTests.Based.UnspecifiedClass' is inaccessible due to its protection level
                //_ = new UnspecifiedClass(); // private?

                _ = new PrivateProtected();

                return;
            }

        }

    }
}
