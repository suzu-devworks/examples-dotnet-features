using System.Collections.Generic;

namespace Examples.Features.CS6.CollectionInitializers
{
    public class Student
    {
        public int Id { get; set; }
        public IList<StudentName> Names { get; } = new List<StudentName>();
    }

    public class StudentName
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
    }

}
