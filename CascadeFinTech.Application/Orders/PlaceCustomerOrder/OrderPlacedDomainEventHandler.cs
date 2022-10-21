using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CascadeFinTech.Domain.Customers.Orders.Events;
using CascadeFinTech.Domain.Payments;

namespace CascadeFinTech.Application.Orders.PlaceCustomerOrder
{
    public class OrderPlacedDomainEventHandler : INotificationHandler<OrderPlacedEvent>
    {
        private readonly IPaymentRepository _paymentRepository;

        public OrderPlacedDomainEventHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
        {
            var newPayment = new Payment(notification.OrderId);

            await _paymentRepository.AddAsync(newPayment);
        }
    }
}