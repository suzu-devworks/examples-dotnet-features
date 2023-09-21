namespace Examples.Features.CS6.IndexInitializers
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

        // compatible with C#2.0.
#pragma warning disable IDE0032 // Use auto property

        private int _id;
        public int ID { get { return _id; } set { _id = value; } }

        private string _firstName;
        public string FirstName { get { return _firstName; } set { _firstName = value; } }

        private string _lastName;
        public string LastName { get { return _lastName; } set { _lastName = value; } }

#pragma warning restore IDE0032 // Use auto property
    }

    public class Matrix
    {
        private readonly double[,] _storage = new double[3, 3];

        public double this[int row, int column]
        {
            // The embedded array will throw out of range exceptions as appropriate.
            get { return _storage[row, column]; }
            set { _storage[row, column] = value; }
        }

        public double Det
        {
            get
            {
                // det  = a(11)*a(22)*a(33)
                //      + a(12)*a(23)*a(31)
                //      + a(13)*a(21)*a(32)
                //      - a(13)*a(22)*a(31)
                //      - a(11)*a(23)*a(32)
                //      - a(12)*a(21)*a(33)
                double result = (_storage[0, 0] * _storage[1, 1] * _storage[2, 2])
                                + (_storage[0, 1] * _storage[1, 2] * _storage[2, 0])
                                + (_storage[0, 2] * _storage[1, 0] * _storage[2, 1])
                                - (_storage[0, 2] * _storage[1, 1] * _storage[2, 0])
                                - (_storage[0, 0] * _storage[1, 2] * _storage[2, 1])
                                - (_storage[0, 1] * _storage[1, 0] * _storage[2, 2]);
                return result;
            }
        }
    }


}
