using Newtonsoft.Json;
using CascadeFinTech.Application.Configuration.DomainEvents;
using CascadeFinTech.Domain.Customers;
using CascadeFinTech.Domain.Customers.Orders;
using CascadeFinTech.Domain.Customers.Orders.Events;

namespace CascadeFinTech.Application.Orders.PlaceCustomerOrder
{
    public class OrderPlacedNotification : DomainNotificationBase<OrderPlacedEvent>
    {
        public OrderId OrderId { get; }
        public CustomerId CustomerId { get; }

        public OrderPlacedNotification(OrderPlacedEvent domainEvent) : base(domainEvent)
        {
            this.OrderId = domainEvent.OrderId;
            this.CustomerId = domainEvent.CustomerId;
        }

        [JsonConstructor]
        public OrderPlacedNotification(OrderId orderId, CustomerId customerId) : base(null)
        {
            this.OrderId = orderId;
            this.CustomerId = customerId;
        }
    }
}