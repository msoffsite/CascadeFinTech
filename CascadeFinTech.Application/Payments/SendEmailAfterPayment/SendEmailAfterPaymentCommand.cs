using System;
using MediatR;
using Newtonsoft.Json;
using CascadeFinTech.Application.Configuration.Commands;
using CascadeFinTech.Domain.Payments;

namespace CascadeFinTech.Application.Payments.SendEmailAfterPayment
{
    public class SendEmailAfterPaymentCommand : InternalCommandBase<Unit>
    {
        public PaymentId PaymentId { get; }

        [JsonConstructor]
        public SendEmailAfterPaymentCommand(Guid id, PaymentId paymentId) : base(id)
        {
            PaymentId = paymentId;
        }
    }
}