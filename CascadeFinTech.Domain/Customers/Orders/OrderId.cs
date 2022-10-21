using System;
using CascadeFinTech.Domain.SeedWork;

namespace CascadeFinTech.Domain.Customers.Orders
{
    public class OrderId : TypedIdValueBase
    {
        public OrderId(Guid value) : base(value)
        {
        }
    }
}