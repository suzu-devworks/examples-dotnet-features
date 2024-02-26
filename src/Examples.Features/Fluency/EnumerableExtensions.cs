namespace Examples.Fluency;

public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var element in source)
        {
            action.Invoke(element);
        }
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<(T, int)> action)
    {
        foreach (var element in source.Select((x, i) => (x, i)))
        {
            action.Invoke(element);
        }
    }

}
