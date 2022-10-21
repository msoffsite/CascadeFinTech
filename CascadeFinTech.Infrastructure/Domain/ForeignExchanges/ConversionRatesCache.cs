using System.Collections.Generic;
using CascadeFinTech.Domain.ForeignExchange;

namespace CascadeFinTech.Infrastructure.Domain.ForeignExchanges
{
    public class ConversionRatesCache
    {
        public List<ConversionRate> Rates { get; }

        public ConversionRatesCache(List<ConversionRate> rates)
        {
            Rates = rates;
        }
    }
}