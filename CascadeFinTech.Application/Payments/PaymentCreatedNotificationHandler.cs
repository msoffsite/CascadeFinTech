using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CascadeFinTech.Application.Configuration.Processing;
using CascadeFinTech.Application.Payments.SendEmailAfterPayment;

namespace CascadeFinTech.Application.Payments
{
    public class PaymentCreatedNotificationHandler : INotificationHandler<PaymentCreatedNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public PaymentCreatedNotificationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(PaymentCreatedNotification request, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(
                new SendEmailAfterPaymentCommand(Guid.NewGuid(), request.PaymentId));
        }
    }
}