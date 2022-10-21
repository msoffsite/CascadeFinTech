using System;
using System.Collections.Generic;
using System.Linq;
using CascadeFinTech.Domain.Customers.Orders;
using CascadeFinTech.Domain.Customers.Orders.Events;
using CascadeFinTech.Domain.Customers.Rules;
using CascadeFinTech.Domain.ForeignExchange;
using CascadeFinTech.Domain.Books;
using CascadeFinTech.Domain.SeedWork;

namespace CascadeFinTech.Domain.Customers
{
    public class Customer : Entity, IAggregateRoot
    {
        public CustomerId Id { get; private set; }

        private string _email;

        private string _name;

        private readonly List<Order> _orders;

        private bool _welcomeEmailWasSent;

        private Customer()
        {
            _orders = new List<Order>();
        }
         
        private Customer(string email, string name)
        {
            Id = new CustomerId(Guid.NewGuid());
            _email = email;
            _name = name;
            _welcomeEmailWasSent = false;
            _orders = new List<Order>();

            AddDomainEvent(new CustomerRegisteredEvent(Id));
        }

        public static Customer CreateRegistered(
            string email, 
            string name,
            ICustomerUniquenessChecker customerUniquenessChecker)
        {
            CheckRule(new CustomerEmailMustBeUniqueRule(customerUniquenessChecker, email));

            return new Customer(email, name);
        }

        public OrderId PlaceOrder(
            List<OrderBookData> orderBooksData,
            List<BookPriceData> allBookPrices,
            string currency, 
            List<ConversionRate> conversionRates)
        {
            CheckRule(new CustomerCannotOrderMoreThan2OrdersOnTheSameDayRule(_orders));
            CheckRule(new OrderMustHaveAtLeastOneBookRule(orderBooksData));

            var order = Order.CreateNew(orderBooksData, allBookPrices, currency, conversionRates);

            _orders.Add(order);

            AddDomainEvent(new OrderPlacedEvent(order.Id, Id, order.GetValue()));

            return order.Id;
        }

        public void ChangeOrder(
            OrderId orderId, 
            List<BookPriceData> existingBooks,
            List<OrderBookData> newOrderBooksData,
            List<ConversionRate> conversionRates,
            string currency)
        {
            CheckRule(new OrderMustHaveAtLeastOneBookRule(newOrderBooksData));

            var order = _orders.Single(x => x.Id == orderId);
            order.Change(existingBooks, newOrderBooksData, conversionRates, currency);

            AddDomainEvent(new OrderChangedEvent(orderId));
        }

        public void RemoveOrder(OrderId orderId)
        {
            var order = _orders.Single(x => x.Id == orderId);
            order.Remove();

            AddDomainEvent(new OrderRemovedEvent(orderId));
        }

        public void MarkAsWelcomedByEmail()
        {
            _welcomeEmailWasSent = true;
        }
    }
}