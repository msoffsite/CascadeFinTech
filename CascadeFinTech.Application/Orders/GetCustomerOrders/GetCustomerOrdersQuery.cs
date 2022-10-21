using System;
using System.Collections.Generic;
using MediatR;
using CascadeFinTech.Application.Configuration.Queries;

namespace CascadeFinTech.Application.Orders.GetCustomerOrders
{
    public class GetCustomerOrdersQuery : IQuery<List<OrderDto>>
    {
        public Guid CustomerId { get; }

        public GetCustomerOrdersQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}