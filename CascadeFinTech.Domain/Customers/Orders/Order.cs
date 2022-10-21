using System;
using System.Collections.Generic;
using System.Linq;
using CascadeFinTech.Domain.ForeignExchange;
using CascadeFinTech.Domain.Books;
using CascadeFinTech.Domain.SeedWork;
using CascadeFinTech.Domain.SharedKernel;

namespace CascadeFinTech.Domain.Customers.Orders
{
    public class Order : Entity
    {
        internal OrderId Id;

        private bool _isRemoved;

        private MoneyValue _value;

        private MoneyValue _valueInEUR;

        private List<OrderBook> _orderBooks;

        private OrderStatus _status;

        private DateTime _orderDate;

        private DateTime? _orderChangeDate;

        private Order()
        {
            _orderBooks = new List<OrderBook>();
            _isRemoved = false;
        }

        private Order(
            List<OrderBookData> orderBooksData,
            List<BookPriceData> bookPrices,
            string currency, 
            List<ConversionRate> conversionRates
            )
        {
            _orderDate = SystemClock.Now;
            Id = new OrderId(Guid.NewGuid());
            _orderBooks = new List<OrderBook>();

            foreach (var orderBookData in orderBooksData)
            {
                var bookPrice = bookPrices.Single(x => x.BookId == orderBookData.BookId &&
                                                             x.Price.Currency == currency);
                var orderBook = OrderBook.CreateForBook(
                    bookPrice, 
                    orderBookData.Quantity,
                    currency, 
                    conversionRates);

                _orderBooks.Add(orderBook);
            }

            CalculateOrderValue();
            _status = OrderStatus.Placed;
        }

        internal static Order CreateNew(List<OrderBookData> orderBooksData,
            List<BookPriceData> allBookPrices,
            string currency,
            List<ConversionRate> conversionRates)
        {
            return new Order(orderBooksData, allBookPrices, currency, conversionRates);
        }

        internal void Change(
            List<BookPriceData> allBookPrices,
            List<OrderBookData> orderBooksData, 
            List<ConversionRate> conversionRates,
            string currency)
        {
            foreach (var orderBookData in orderBooksData)
            {
                var book = allBookPrices.Single(x => x.BookId == orderBookData.BookId &&
                                                           x.Price.Currency == currency);
                
                var existingBookOrder = _orderBooks.SingleOrDefault(x => x.BookId == orderBookData.BookId);
                if (existingBookOrder != null)
                {
                    var existingOrderBook = _orderBooks.Single(x => x.BookId == existingBookOrder.BookId);
                    
                    existingOrderBook.ChangeQuantity(book, orderBookData.Quantity, conversionRates);
                }
                else
                {
                    var orderBook = OrderBook.CreateForBook(book, orderBookData.Quantity, currency, conversionRates);
                    _orderBooks.Add(orderBook);
                }
            }

            var orderBooksToCheck = _orderBooks.ToList();
            foreach (var existingBook in orderBooksToCheck)
            {
                var book = orderBooksData.SingleOrDefault(x => x.BookId == existingBook.BookId);
                if (book == null)
                {
                    _orderBooks.Remove(existingBook);
                }
            }

            CalculateOrderValue();

            _orderChangeDate = DateTime.UtcNow;
        }

        internal void Remove()
        {
            _isRemoved = true;
        }

        internal bool IsOrderedToday()
        {
           return _orderDate.Date == SystemClock.Now.Date;
        }

        internal MoneyValue GetValue()
        {
            return _value;
        }

        private void CalculateOrderValue()
        {
            _value = _orderBooks.Sum(x => x.Value);

            _valueInEUR = _orderBooks.Sum(x => x.ValueInEUR);
        }
    }
}