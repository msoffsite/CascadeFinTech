using Newtonsoft.Json;
using CascadeFinTech.Application.Configuration.DomainEvents;
using CascadeFinTech.Domain.Payments;

namespace CascadeFinTech.Application.Payments
{
    public class PaymentCreatedNotification : DomainNotificationBase<PaymentCreatedEvent>
    {
        public PaymentId PaymentId { get; }

        public PaymentCreatedNotification(PaymentCreatedEvent domainEvent) : base(domainEvent)
        {
            this.PaymentId = domainEvent.PaymentId;
        }

        [JsonConstructor]
        public PaymentCreatedNotification(PaymentId paymentId) : base(null)
        {
            this.PaymentId = paymentId;
        }
    }
}