using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CascadeFinTech.Application.Configuration.Commands;
using CascadeFinTech.Application.Configuration.Data;
using CascadeFinTech.Application.Orders.PlaceCustomerOrder;
using CascadeFinTech.Domain.Customers;
using CascadeFinTech.Domain.Customers.Orders;
using CascadeFinTech.Domain.ForeignExchange;
using CascadeFinTech.Domain.Books;

namespace CascadeFinTech.Application.Orders.ChangeCustomerOrder
{
    internal sealed class ChangeCustomerOrderCommandHandler : ICommandHandler<ChangeCustomerOrderCommand,Unit>
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly IForeignExchange _foreignExchange;

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal ChangeCustomerOrderCommandHandler(
            ICustomerRepository customerRepository,
            IForeignExchange foreignExchange, 
            ISqlConnectionFactory sqlConnectionFactory)
        {
            this._customerRepository = customerRepository;
            _foreignExchange = foreignExchange;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Unit> Handle(ChangeCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await this._customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            var orderId = new OrderId(request.OrderId);

            var conversionRates = this._foreignExchange.GetConversionRates();
            var orderBooks = request
                    .Books
                    .Select(x => new OrderBookData(new BookId(x.Id), x.Quantity))
                    .ToList();

            var allBookPrices =
                await BookPriceProvider.GetAllBookPrices(_sqlConnectionFactory.GetOpenConnection());

            customer.ChangeOrder(
                orderId,
                allBookPrices, 
                orderBooks, 
                conversionRates, 
                request.Currency);

            return Unit.Value;
        }
    }
}
