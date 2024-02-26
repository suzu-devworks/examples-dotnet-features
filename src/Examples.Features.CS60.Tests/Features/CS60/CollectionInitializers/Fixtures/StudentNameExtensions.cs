using System.Collections.Generic;

namespace Examples.Features.CS60.CollectionInitializers
{

    public static class StudentNameExtensions
    {
        public static void Add(this IList<StudentName> list, int id, string firstName, string lastName)
        {
            list.Add(new StudentName() { FirstName = firstName, FamilyName = lastName, Id = id });
            return;
        }

        public static void Add(this Queue<StudentName> queue, int id, string firstName, string lastName)
        {
            queue.Enqueue(new StudentName() { FirstName = firstName, FamilyName = lastName, Id = id });
            return;
        }
    }
}

