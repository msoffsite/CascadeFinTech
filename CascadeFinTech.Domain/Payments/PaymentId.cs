using System;
using CascadeFinTech.Domain.SeedWork;

namespace CascadeFinTech.Domain.Payments
{
    public class PaymentId : TypedIdValueBase
    {
        public PaymentId(Guid value) : base(value)
        {
        }
    }
}