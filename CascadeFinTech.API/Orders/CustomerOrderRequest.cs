using System.Collections.Generic;
using CascadeFinTech.Application.Orders;

namespace CascadeFinTech.API.Orders
{
    public class CustomerOrderRequest
    {
        public List<BookDto> Books { get; set; }

        public string Currency { get; set; }
    }
}