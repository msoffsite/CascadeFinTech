using System.Reflection;
using CascadeFinTech.Application.Orders.PlaceCustomerOrder;

namespace CascadeFinTech.Infrastructure.Processing
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(PlaceCustomerOrderCommand).Assembly;
    }
}