using CascadeFinTech.Domain.Books;

namespace CascadeFinTech.Domain.Customers.Orders
{
    public class OrderBookData
    {
        public OrderBookData(BookId bookId, int quantity)
        {
            BookId = bookId;
            Quantity = quantity;
        }

        public BookId BookId { get; }

        public int Quantity { get; }
    }
}