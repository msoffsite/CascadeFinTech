using System;
using System.Collections.Generic;
using NUnit.Framework;
using CascadeFinTech.Domain.Customers.Orders;
using CascadeFinTech.Domain.Customers.Orders.Events;
using CascadeFinTech.Domain.Customers.Rules;
using CascadeFinTech.Domain.ForeignExchange;
using CascadeFinTech.Domain.Books;
using CascadeFinTech.Domain.SharedKernel;
using CascadeFinTech.UnitTests.SeedWork;

namespace CascadeFinTech.UnitTests.Customers
{
    [TestFixture]
    public class PlaceOrderTests : TestBase
    {
        [Test]
        public void PlaceOrder_WhenAtLeastOneBookIsAdded_IsSuccessful()
        {
            // Arrange
            var customer = CustomerFactory.Create();

            var orderBooksData = new List<OrderBookData>();
            orderBooksData.Add(new OrderBookData(SampleBooks.Book1Id, 2));
            
            var allBookPrices = new List<BookPriceData>
            {
                SampleBookPrices.Book1EUR, SampleBookPrices.Book1USD
            };
            
            const string currency = "EUR";
            var conversionRates = GetConversionRates();
            
            // Act
            customer.PlaceOrder(
                orderBooksData, 
                allBookPrices, 
                currency, 
                conversionRates);

            // Assert
            var orderPlaced = AssertPublishedDomainEvent<OrderPlacedEvent>(customer);
            Assert.That(orderPlaced.Value, Is.EqualTo(MoneyValue.Of(200, "EUR")));
        }

        [Test]
        public void PlaceOrder_WhenNoBookIsAdded_BreaksOrderMustHaveAtLeastOneBookRule()
        {
            // Arrange
            var customer = CustomerFactory.Create();

            var orderBooksData = new List<OrderBookData>();

            var allBookPrices = new List<BookPriceData>
            {
                SampleBookPrices.Book1EUR, SampleBookPrices.Book1USD
            };

            const string currency = "EUR";
            var conversionRates = GetConversionRates();

            // Assert
            AssertBrokenRule<OrderMustHaveAtLeastOneBookRule>(() =>
            {
                // Act
                customer.PlaceOrder(
                    orderBooksData,
                    allBookPrices,
                    currency,
                    conversionRates);
            });
        }

        [Test]
        public void PlaceOrder_GivenTwoOrdersInThatDayAlreadyMade_BreaksCustomerCannotOrderMoreThan2OrdersOnTheSameDayRule()
        {
            // Arrange
            var customer = CustomerFactory.Create();

            var orderBooksData = new List<OrderBookData>();
            orderBooksData.Add(new OrderBookData(SampleBooks.Book1Id, 2));

            var allBookPrices = new List<BookPriceData>
            {
                SampleBookPrices.Book1EUR, SampleBookPrices.Book1USD
            };

            const string currency = "EUR";
            var conversionRates = GetConversionRates();

            SystemClock.Set(new DateTime(2020, 1, 10, 11, 0, 0));
            customer.PlaceOrder(
                orderBooksData,
                allBookPrices,
                currency,
                conversionRates);

            SystemClock.Set(new DateTime(2020, 1, 10, 11, 30, 0));
            customer.PlaceOrder(
                orderBooksData,
                allBookPrices,
                currency,
                conversionRates);

            SystemClock.Set(new DateTime(2020, 1, 10, 12, 00, 0));

            // Assert
            AssertBrokenRule<CustomerCannotOrderMoreThan2OrdersOnTheSameDayRule>(() =>
            {
                // Act
                customer.PlaceOrder(
                    orderBooksData,
                    allBookPrices,
                    currency,
                    conversionRates);
            });
        }

        private static List<ConversionRate> GetConversionRates()
        {

            var conversionRates = new List<ConversionRate>();

            conversionRates.Add(new ConversionRate("USD", "EUR", (decimal)0.88));
            conversionRates.Add(new ConversionRate("EUR", "USD", (decimal)1.13));

            return conversionRates;
        }
    }



    public class SampleBooks
    {
        public static readonly BookId Book1Id = new BookId(Guid.Parse("C36B2014-3A5E-46F7-B83F-0E23453E1363"));

        public static readonly BookId Book2Id = new BookId(Guid.Parse("E1158088-C85A-4217-BC04-4CD71F9F275B"));
    }

    public class SampleBookPrices
    {
        public static readonly BookPriceData Book1EUR = new BookPriceData(
            SampleBooks.Book1Id,
            MoneyValue.Of(100, "EUR"));

        public static readonly BookPriceData Book1USD = new BookPriceData(
            SampleBooks.Book1Id,
            MoneyValue.Of(110, "USD"));
    }
}