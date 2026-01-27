using Xunit;

namespace Examples.Features.CSharp72.Tests.PrivateProtectedAccessModifier
{
    /// <summary>
    /// Tests for <c>private protected</c> access modifier in C# 7.2.
    /// </summary>
    public class PrivateProtectedAccessModifierTests
    {
        [Fact]
        public void When_UsingPrivateProtectedAccessModifier_Then_AllMethodCallVariationsWork()
        {
            // the current assembly access.

            Assert.IsType<Based.PublicClass>(new Based.PublicClass());

            // error CS0122: 'PrivateProtectedAccessModifierTests.Based.ProtectedClass' is inaccessible due to its protection level
            // Assert.IsType<Based.ProtectedClass>(new Based.ProtectedClass())s;

            Assert.IsType<Based.InternalClass>(new Based.InternalClass());

            Assert.IsType<Based.ProtectedInternalClass>(new Based.ProtectedInternalClass());

            // error CS0122: 'PrivateProtectedAccessModifierTests.Based.PrivateClass' is inaccessible due to its protection level
            // Assert.IsType<Based.PrivateClass>(new Based.PrivateClass());

            // error CS0122: 'PrivateProtectedAccessModifierTests.Based.UnspecifiedClass' is inaccessible due to its protection level
            // Assert.IsType<Based.UnspecifiedClass>(new Based.UnspecifiedClass());

            // error CS0122: 'PrivateProtectedAccessModifierTests.Based.PrivateProtected' is inaccessible due to its protection level[/ workspaces / examples - dotnet - features / src / Examples.Features.CSharp72.Tests.Tests / Examples.Features.CSharp72.Tests.Tests.csproj]
            // Assert.IsType<Based.PrivateProtected>(new Based.PrivateProtected());

            var instance = new Derived();
            instance.DoBaseAccess();
            instance.DoDerivedAccess();
        }

        public class Based
        {
            public class PublicClass { }

            protected class ProtectedClass { }

            internal class InternalClass { }

            protected internal class ProtectedInternalClass { };

            private class PrivateClass { }

#pragma warning disable IDE0040 // Add accessibility modifier
            class UnspecifiedClass { }
#pragma warning restore IDE0040

            // C# 7.2 or later
            // C# 7.1 : error CS8302: Feature 'private protected' is not available in C# 7.1. Please use language version 7.2 or greater.
            private protected class PrivateProtected { }

            public void DoBaseAccess()
            {
                // the containing class access.

                Assert.IsType<PublicClass>(new PublicClass());
                Assert.IsType<ProtectedClass>(new ProtectedClass());
                Assert.IsType<InternalClass>(new InternalClass());
                Assert.IsType<ProtectedInternalClass>(new ProtectedInternalClass());
                Assert.IsType<PrivateClass>(new PrivateClass());
                Assert.IsType<UnspecifiedClass>(new UnspecifiedClass());
                Assert.IsType<PrivateProtected>(new PrivateProtected());
            }

        }

        public class Derived : Based
        {
            public void DoDerivedAccess()
            {
                // types derived from the containing class within the current assembly access.

                Assert.IsType<PublicClass>(new PublicClass());
                Assert.IsType<ProtectedClass>(new ProtectedClass());
                Assert.IsType<InternalClass>(new InternalClass());
                Assert.IsType<ProtectedInternalClass>(new ProtectedInternalClass());

                // error CS0122: 'PrivateProtectedAccessModifierTests.Based.PrivateClass' is inaccessible due to its protection level
                // Assert.IsType<PrivateClass>(new PrivateClass());
                // error CS0122: 'PrivateProtectedAccessModifierTests.Based.UnspecifiedClass' is inaccessible due to its protection level
                // Assert.IsType<UnspecifiedClass>(new UnspecifiedClass()); // private?

                Assert.IsType<PrivateProtected>(new PrivateProtected());
            }

        }

    }
}
