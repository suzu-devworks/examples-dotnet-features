namespace Examples.Features.CSharp60.Tests.IndexInitializers.Fixtures
{
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
                double det = (_storage[0, 0] * _storage[1, 1] * _storage[2, 2])
                           + (_storage[0, 1] * _storage[1, 2] * _storage[2, 0])
                           + (_storage[0, 2] * _storage[1, 0] * _storage[2, 1])
                           - (_storage[0, 2] * _storage[1, 1] * _storage[2, 0])
                           - (_storage[0, 0] * _storage[1, 2] * _storage[2, 1])
                           - (_storage[0, 1] * _storage[1, 0] * _storage[2, 2]);
                return det;
            }
        }
    }
}
