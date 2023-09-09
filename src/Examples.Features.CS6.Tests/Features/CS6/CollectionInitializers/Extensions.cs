using System.Collections.Generic;

namespace Examples.Features.CS6.CollectionInitializers
{

    public static class Extensions
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

