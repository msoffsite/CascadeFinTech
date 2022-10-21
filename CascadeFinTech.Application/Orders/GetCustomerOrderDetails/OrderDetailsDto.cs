using System;
using System.Collections.Generic;

namespace CascadeFinTech.Application.Orders.GetCustomerOrderDetails
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }

        public decimal Value { get; set; }

        public string Currency { get; set; }

        public bool IsRemoved { get; set; }

        public List<BookDto> Books { get; set; }
    }
}