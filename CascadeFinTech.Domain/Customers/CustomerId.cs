using System;
using CascadeFinTech.Domain.SeedWork;

namespace CascadeFinTech.Domain.Customers
{
    public class CustomerId : TypedIdValueBase
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}