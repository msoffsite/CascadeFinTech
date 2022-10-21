using System;
using System.Runtime.InteropServices.ComTypes;

namespace CascadeFinTech.Application.Orders
{
    public class BookDto
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }        

        public string Title { get; set; }

        public BookDto()
        {
            
        }

        public BookDto(Guid id, int quantity)
        {
            this.Id = id;
            this.Quantity = quantity;
        }
    }
}