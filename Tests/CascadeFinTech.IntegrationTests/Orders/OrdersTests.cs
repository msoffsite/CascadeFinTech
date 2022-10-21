using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using NUnit.Framework;
using CascadeFinTech.Application.Customers.IntegrationHandlers;
using CascadeFinTech.Application.Customers.RegisterCustomer;
using CascadeFinTech.Application.Orders;
using CascadeFinTech.Application.Orders.GetCustomerOrderDetails;
using CascadeFinTech.Application.Orders.PlaceCustomerOrder;
using CascadeFinTech.Application.Payments;
using CascadeFinTech.Domain.Customers;
using CascadeFinTech.Domain.Customers.Orders;
using CascadeFinTech.Infrastructure.Processing;
using CascadeFinTech.IntegrationTests.SeedWork;

namespace CascadeFinTech.IntegrationTests.Orders
{
    [TestFixture]
    public class OrdersTests : TestBase
    {
        [Test]
        public async Task PlaceOrder_Test()
        {
            var customerEmail = "email@email.com";
            var customer = await CommandsExecutor.Execute(new RegisterCustomerCommand(customerEmail, "Sample Customer"));

            List<BookDto> books = new List<BookDto>();
            var bookId = Guid.Parse("2B333A66-9D78-4967-AF79-2D64CE6F4479");
            books.Add(new BookDto(bookId, 2));
            var orderId = await CommandsExecutor.Execute(new PlaceCustomerOrderCommand(customer.Id, books, "EUR"));

            var orderDetails = await QueriesExecutor.Execute(new GetCustomerOrderDetailsQuery(orderId));

            Assert.That(orderDetails, Is.Not.Null);
            Assert.That(orderDetails.Books.Count, Is.EqualTo(1));
            Assert.That(orderDetails.Books[0].Quantity, Is.EqualTo(2));
            Assert.That(orderDetails.Books[0].Id, Is.EqualTo(bookId));

            var connection = new SqlConnection(ConnectionString);
            var messagesList = await OutboxMessagesHelper.GetOutboxMessages(connection);
            
            Assert.That(messagesList.Count, Is.EqualTo(3));
            
            var customerRegisteredNotification =
                OutboxMessagesHelper.Deserialize<CustomerRegisteredNotification>(messagesList[0]);

            Assert.That(customerRegisteredNotification.CustomerId, Is.EqualTo(new CustomerId(customer.Id)));

            var orderPlaced =
                OutboxMessagesHelper.Deserialize<OrderPlacedNotification>(messagesList[1]);

            Assert.That(orderPlaced.OrderId, Is.EqualTo(new OrderId(orderId)));

            var paymentCreated =
                OutboxMessagesHelper.Deserialize<PaymentCreatedNotification>(messagesList[2]);

            Assert.That(paymentCreated, Is.Not.Null);
        }
    }
}