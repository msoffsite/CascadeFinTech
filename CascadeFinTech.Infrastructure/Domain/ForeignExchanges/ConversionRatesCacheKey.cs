using CascadeFinTech.Infrastructure.Caching;

namespace CascadeFinTech.Infrastructure.Domain.ForeignExchanges
{
    public class ConversionRatesCacheKey : ICacheKey<ConversionRatesCache>
    {
        public string CacheKey => "ConversionRatesCache";
    }
}