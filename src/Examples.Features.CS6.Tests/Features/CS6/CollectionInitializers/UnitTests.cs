using System.Collections.Generic;
using ChainingAssertion;
using Xunit;

// for C# 6.0

namespace Examples.Features.CS6.CollectionInitializers
{
    /// <summary>
    /// Tests for C# 6.0, Extension Add methods in collection initializers.
    /// </summary>
    public class UnitTests
    {

        [Fact]
        public void WhenInitializingExtensionAddMethods()
        {
            // Called List<T>#Add(T item) method.
            var initializedInMemberAddMethod = new List<StudentName>
            {
                new StudentName { Id = 211, FirstName =  "Sachin", FamilyName = "Karnik" },
                new StudentName { Id =317, FirstName = "Dina", FamilyName = "Salimzianova"},
                new StudentName { Id =198, FirstName = "Andy", FamilyName = "Ruth"},
            };
            initializedInMemberAddMethod.Count.Is(3);
            initializedInMemberAddMethod[0].FirstName.Is("Sachin");
            initializedInMemberAddMethod[1].FirstName.Is("Dina");
            initializedInMemberAddMethod[2].FirstName.Is("Andy");

            // Called Extensions.Add(this IList<StudentName> list, int id, string firstName, string lastName) method.
            var initializedInExtensionAddMethod = new List<StudentName>
            {
                { 211, "Sachin", "Karnik" },
                { 317, "Dina", "Salimzianova"},
                { 198, "Andy", "Ruth"},
            };
            initializedInExtensionAddMethod.Count.Is(3);
            initializedInExtensionAddMethod[0].FirstName.Is("Sachin");
            initializedInExtensionAddMethod[1].FirstName.Is("Dina");
            initializedInExtensionAddMethod[2].FirstName.Is("Andy");

            return;
        }

        [Fact]
        public void WhenInitializingExtensionAddMethods_WithStatic()
        {
            var initializedInExtensionAddMethod = Data.InitializedStatic;
            initializedInExtensionAddMethod.Count.Is(3);
            initializedInExtensionAddMethod[0].FirstName.Is("Sachin");
            initializedInExtensionAddMethod[1].FirstName.Is("Dina");
            initializedInExtensionAddMethod[2].FirstName.Is("Andy");

            return;
        }

        private static class Data
        {
            // Called Extensions.Add(this IList<StudentName> list, int id, string firstName, string lastName) method.
            public static readonly IList<StudentName> InitializedStatic = new List<StudentName>
            {
                { 211, "Sachin", "Karnik" },
                { 317, "Dina", "Salimzianova"},
                { 198, "Andy", "Ruth"},
            };
        }

        [Fact]
        public void WhenInitializingExtensionAddMethods_WithQueue()
        {
            // Called Extensions.Add(this Queue<StudentName> list, int id, string firstName, string lastName) method.
            var initialized = new Queue<StudentName>
            {
                { 211, "Sachin", "Karnik" },
                { 317, "Dina", "Salimzianova"},
                { 198, "Andy", "Ruth"},
            };
            initialized.Count.Is(3);
            initialized.Dequeue().FirstName.Is("Sachin");
            initialized.Dequeue().FirstName.Is("Dina");
            initialized.Dequeue().FirstName.Is("Andy");

            return;
        }

        [Fact]
        public void WhenInitializingExtensionAddMethods_WithClassMember()
        {
            var student = new Student
            {
                // error CS0200 Property or indexer 'Student.Names' cannot be assigned to -- it is read only .
                //Names = new List<StudentName>() { },

                // Called Extensions.Add(this IList<StudentName> list, int id, string firstName, string lastName) method.
                Names =
                {
                    { 211, "Sachin", "Karnik" },
                    { 317, "Dina", "Salimzianova"},
                    { 198, "Andy", "Ruth"},
                }
            };

        }

    }

}
