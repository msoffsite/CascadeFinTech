using System;
using System.Collections.Generic;
using MediatR;
using CascadeFinTech.Application.Configuration.Commands;

namespace CascadeFinTech.Application.Orders.PlaceCustomerOrder
{
    public class PlaceCustomerOrderCommand : CommandBase<Guid>
    {
        public Guid CustomerId { get; }

        public List<BookDto> Books { get; }

        public string Currency { get; }

        public PlaceCustomerOrderCommand(
            Guid customerId, 
            List<BookDto> books, 
            string currency)
        {
            this.CustomerId = customerId;
            this.Books = books;
            this.Currency = currency;
        }
    }
}