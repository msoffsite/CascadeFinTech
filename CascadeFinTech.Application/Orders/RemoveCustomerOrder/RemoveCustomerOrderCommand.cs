using System;
using MediatR;
using CascadeFinTech.Application.Configuration.Commands;

namespace CascadeFinTech.Application.Orders.RemoveCustomerOrder
{
    public class RemoveCustomerOrderCommand : CommandBase
    {
        public Guid CustomerId { get; }

        public Guid OrderId { get; }

        public RemoveCustomerOrderCommand(
            Guid customerId,
            Guid orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}