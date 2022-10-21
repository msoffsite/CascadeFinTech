using System;
using System.Collections.Generic;
using MediatR;
using CascadeFinTech.Application.Configuration.Commands;
using CascadeFinTech.Domain.Books;

namespace CascadeFinTech.Application.Orders.ChangeCustomerOrder
{
    public class ChangeCustomerOrderCommand : CommandBase<Unit>
    {
        public Guid CustomerId { get; }

        public Guid OrderId { get; }

        public string Currency { get; }

        public List<BookDto> Books { get; }

        public ChangeCustomerOrderCommand(
            Guid customerId, 
            Guid orderId,
            List<BookDto> books, 
            string currency)
        {
            CustomerId = customerId;
            OrderId = orderId;
            Currency = currency;
            Books = books;
        }
    }
}
