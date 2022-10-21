using CascadeFinTech.Domain.SeedWork;
using CascadeFinTech.Domain.SharedKernel;

namespace CascadeFinTech.Domain.Books
{
    public class BookPriceData : ValueObject
    {
        public BookPriceData(BookId bookId, MoneyValue price)
        {
            BookId = bookId;
            Price = price;
        }

        public BookId BookId { get; }

        public MoneyValue Price { get; }
    }
}