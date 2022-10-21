using System;
using CascadeFinTech.Domain.Customers.Orders;
using CascadeFinTech.Domain.SeedWork;

namespace CascadeFinTech.Domain.Payments
{
    public class PaymentCreatedEvent : DomainEventBase
    {
        public PaymentCreatedEvent(PaymentId paymentId, OrderId orderId)
        {
            PaymentId = paymentId;
            OrderId = orderId;
        }

        public PaymentId PaymentId { get; }

        public OrderId OrderId { get; }
    }
}