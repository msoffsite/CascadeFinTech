using CascadeFinTech.Domain.SeedWork;

namespace CascadeFinTech.Domain.Customers.Orders.Events
{
    public class OrderChangedEvent : DomainEventBase
    {
        public OrderId OrderId { get; }

        public OrderChangedEvent(OrderId orderId)
        {
            this.OrderId = orderId;
        }
    }
}