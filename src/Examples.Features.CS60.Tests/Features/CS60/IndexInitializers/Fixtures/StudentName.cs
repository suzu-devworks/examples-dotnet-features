namespace Examples.Features.CS60.IndexInitializers.Fixtures
{
    public class StudentName
    {
        public StudentName() { }
        public StudentName(int id, string firstName, string lastName)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
