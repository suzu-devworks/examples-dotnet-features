using System.Collections;

namespace Examples.Features.CS120.CollectionExpressions.Fixtures;

public class ConvertibleCollection : IEnumerable<int>
{
    private readonly List<int> _values = [];

    public IEnumerator<int> GetEnumerator()
        => _values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();
}

public class ConvertibleCollectionWithAdd : IEnumerable<int>
{
    private readonly List<int> _values = [];

    public IEnumerator<int> GetEnumerator()
        => _values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();

    public void Add(int value)
        => _values.Add(value);
}
