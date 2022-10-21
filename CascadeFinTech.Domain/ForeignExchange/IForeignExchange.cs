using System.Collections.Generic;

namespace CascadeFinTech.Domain.ForeignExchange
{
    public interface IForeignExchange
    {
        List<ConversionRate> GetConversionRates();
    }
}