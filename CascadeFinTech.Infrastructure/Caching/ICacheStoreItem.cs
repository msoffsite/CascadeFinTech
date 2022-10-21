namespace CascadeFinTech.Infrastructure.Caching
{
    public interface ICacheStoreItem
    {
        string CacheKey { get; }
    }
}