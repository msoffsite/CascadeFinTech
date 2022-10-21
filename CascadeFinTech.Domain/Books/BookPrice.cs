using CascadeFinTech.Domain.SharedKernel;

namespace CascadeFinTech.Domain.Books
{
    public class BookPrice
    {
        public MoneyValue Value { get; private set; }

        private BookPrice()
        {
            
        }
    }
}