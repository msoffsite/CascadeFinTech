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
            this._orders = new List<Order>();
        }
         
        private Customer(string email, string name)
        {
            this.Id = new CustomerId(Guid.NewGuid());
            _email = email;
            _name = name;
            _welcomeEmailWasSent = false;
            _orders = new List<Order>();

            this.AddDomainEvent(new CustomerRegisteredEvent(this.Id));
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

            this._orders.Add(order);

            this.AddDomainEvent(new OrderPlacedEvent(order.Id, this.Id, order.GetValue()));

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

            var order = this._orders.Single(x => x.Id == orderId);
            order.Change(existingBooks, newOrderBooksData, conversionRates, currency);

            this.AddDomainEvent(new OrderChangedEvent(orderId));
        }

        public void RemoveOrder(OrderId orderId)
        {
            var order = this._orders.Single(x => x.Id == orderId);
            order.Remove();

            this.AddDomainEvent(new OrderRemovedEvent(orderId));
        }

        public void MarkAsWelcomedByEmail()
        {
            this._welcomeEmailWasSent = true;
        }
    }
}