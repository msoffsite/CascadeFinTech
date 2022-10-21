using System;
using MediatR;
using Newtonsoft.Json;
using CascadeFinTech.Application.Configuration.Commands;
using CascadeFinTech.Domain.Customers;

namespace CascadeFinTech.Application.Customers
{
    public class MarkCustomerAsWelcomedCommand : InternalCommandBase<Unit>
    {
        [JsonConstructor]
        public MarkCustomerAsWelcomedCommand(Guid id, CustomerId customerId) : base(id)
        {
            CustomerId = customerId;
        }

        public CustomerId CustomerId { get; }
    }
}