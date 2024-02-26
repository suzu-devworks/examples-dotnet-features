using System.Collections.Generic;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS60.CollectionInitializers
{
    /// <summary>
    /// Tests for Extension Add methods in collection initializers in C# 6.0.
    /// </summary>
    public class CollectionInitializersTests
    {

        [Fact]
        public void WhenInitializingList()
        {
            var initializedInMemberAddMethod = new List<StudentName>
            {
                // Called List<T>#Add(T item) method.
                new StudentName { Id = 211, FirstName =  "Sachin", FamilyName = "Karnik" },
                new StudentName { Id = 317, FirstName = "Dina", FamilyName = "Salimzianova"},
                new StudentName { Id = 198, FirstName = "Andy", FamilyName = "Ruth"},
            };
            initializedInMemberAddMethod.Count.Is(3);
            initializedInMemberAddMethod[0].FirstName.Is("Sachin");
            initializedInMemberAddMethod[1].FirstName.Is("Dina");
            initializedInMemberAddMethod[2].FirstName.Is("Andy");

            var initializedInExtensionAddMethod = new List<StudentName>
            {
                // Called StudentNameExtensions.Add(this IList<StudentName> list, int id, string firstName, string lastName) method.
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
        public void WhenInitializingStaticList()
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
            public static readonly IList<StudentName> InitializedStatic = new List<StudentName>
            {
                // Called StudentNameExtensions.Add(this IList<StudentName> list, int id, string firstName, string lastName) method.
                { 211, "Sachin", "Karnik" },
                { 317, "Dina", "Salimzianova"},
                { 198, "Andy", "Ruth"},
            };
        }

        [Fact]
        public void WhenInitializingQueue()
        {
            var initialized = new Queue<StudentName>
            {
                // Called StudentNameExtensions.Add(this Queue<StudentName> list, int id, string firstName, string lastName) method.
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
        public void WhenInitializingClassMember()
        {
            var student = new Student
            {
                // error CS0200 Property or indexer 'Student.Names' cannot be assigned to -- it is read only .
                //Names = new List<StudentName>() { },

                Names =
                {
                // Called StudentNameExtensions.Add(this IList<StudentName> list, int id, string firstName, string lastName) method.
                    { 211, "Sachin", "Karnik" },
                    { 317, "Dina", "Salimzianova"},
                    { 198, "Andy", "Ruth"},
                }
            };

        }

        public class Student
        {
            public Student()
            {
                Names = new List<StudentName>();
            }

            public int Id { get; set; }

            public IList<StudentName> Names { get; private set; }
        }

    }

}
