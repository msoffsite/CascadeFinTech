using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using CascadeFinTech.Domain.Customers.Orders;
using CascadeFinTech.Domain.SeedWork;

namespace CascadeFinTech.Domain.Customers.Rules
{
    public class OrderMustHaveAtLeastOneBookRule : IBusinessRule
    {
        private readonly List<OrderBookData> _orderBookData;

        public OrderMustHaveAtLeastOneBookRule(List<OrderBookData> orderBookData)
        {
            _orderBookData = orderBookData;
        }

        public bool IsBroken() => !_orderBookData.Any();

        public string Message => "Order must have at least one book";
    }
}