using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CascadeFinTech.Application.Configuration.Commands;
using CascadeFinTech.Application.Configuration.Data;
using CascadeFinTech.Domain.Customers;
using CascadeFinTech.Domain.Customers.Orders;
using CascadeFinTech.Domain.ForeignExchange;
using CascadeFinTech.Domain.Books;

namespace CascadeFinTech.Application.Orders.PlaceCustomerOrder
{
    public class PlaceCustomerOrderCommandHandler : ICommandHandler<PlaceCustomerOrderCommand, Guid>
    {
        private readonly ICustomerRepository _customerRepository;
        
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        
        private readonly IForeignExchange _foreignExchange;

        public PlaceCustomerOrderCommandHandler(
            ICustomerRepository customerRepository,
            IForeignExchange foreignExchange, 
            ISqlConnectionFactory sqlConnectionFactory)
        {
            this._customerRepository = customerRepository;
            this._foreignExchange = foreignExchange;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Guid> Handle(PlaceCustomerOrderCommand command, CancellationToken cancellationToken)
        {
            var customer = await this._customerRepository.GetByIdAsync(new CustomerId(command.CustomerId));

            var allBookPrices =
                await BookPriceProvider.GetAllBookPrices(_sqlConnectionFactory.GetOpenConnection());

            var conversionRates = this._foreignExchange.GetConversionRates();

            var orderBooksData = command
                .Books
                .Select(x => new OrderBookData(new BookId(x.Id), x.Quantity))
                .ToList();          
            
            var orderId = customer.PlaceOrder(
                orderBooksData,
                allBookPrices,
                command.Currency,
                conversionRates);

            return orderId.Value;
        }
    }
}