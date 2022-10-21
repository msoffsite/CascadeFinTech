using System;
using System.Collections.Generic;
using System.Linq;
using CascadeFinTech.Domain.ForeignExchange;
using CascadeFinTech.Domain.Books;
using CascadeFinTech.Domain.SeedWork;
using CascadeFinTech.Domain.SharedKernel;

namespace CascadeFinTech.Domain.Customers.Orders
{
    public class OrderBook : Entity
    {
        public int Quantity { get; private set; }

        public BookId BookId { get; private set; }

        internal MoneyValue Value { get; private set; }

        internal MoneyValue ValueInEUR { get; private set; }

        private OrderBook()
        {

        }

        private OrderBook(
            BookPriceData bookPrice,
            int quantity,
            string currency,
            List<ConversionRate> conversionRates)
        {
            BookId = bookPrice.BookId;
            Quantity = quantity;

            CalculateValue(bookPrice, currency, conversionRates);
        }

        internal static OrderBook CreateForBook(
            BookPriceData bookPrice, int quantity, string currency,
            List<ConversionRate> conversionRates)
        {
            return new OrderBook(bookPrice, quantity, currency, conversionRates);
        }

        internal void ChangeQuantity(BookPriceData bookPrice, int quantity, List<ConversionRate> conversionRates)
        {
            Quantity = quantity;

            CalculateValue(bookPrice, Value.Currency, conversionRates);
        }

        private void CalculateValue(BookPriceData bookPrice, string currency, List<ConversionRate> conversionRates)
        {
            Value = Quantity * bookPrice.Price;
            if (currency == "EUR")
            {
                ValueInEUR = Quantity * bookPrice.Price;
            }
            else
            {
                var conversionRate = conversionRates.Single(x => x.SourceCurrency == currency && x.TargetCurrency == "EUR");
                ValueInEUR = conversionRate.Convert(Value);
            }
        }
    }
}