namespace Treenity_AI_Scraper.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Flat<T>(this IEnumerable<T> l, Func<T, IEnumerable<T>> f) => l.SelectMany(i => new T[] { i }.Concat(f(i).Flat(f)));
    }
}
