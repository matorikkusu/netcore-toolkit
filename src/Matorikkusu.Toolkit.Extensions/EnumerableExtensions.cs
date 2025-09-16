namespace Matorikkusu.Toolkit.Extensions;

public static class EnumerableExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
    {
        return enumerable is null || !enumerable.Any();
    }
        
    public static bool ContainsDuplicates<T1, T2>(this IEnumerable<T1> source, Func<T1, T2> selector)
    {
        var duplicates = new HashSet<T2>();
        return source.Any(x => !duplicates.Add(selector(x)));
    }

    public static IEnumerable<T1> GetDuplicates<T1, T2>(this IEnumerable<T1> source, Func<T1, T2> selector)
    {
        return source.GroupBy(selector)
            .Where(g => g.Count() > 1)
            .SelectMany(g => g);
    }

    public static IEnumerable<T> GetUniqueList<T>(this IEnumerable<T> source)
    {
        var hash = new HashSet<T>();
        foreach (var str in source) hash.Add(str);
        return hash;
    }
}