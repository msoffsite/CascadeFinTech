using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using CascadeFinTech.Application.Configuration.Data;
using CascadeFinTech.Application.Configuration.Queries;

namespace CascadeFinTech.Application.Orders.GetCustomerOrderDetails
{
    internal sealed class GetCustomerOrderDetailsQueryHandler : IQueryHandler<GetCustomerOrderDetailsQuery, OrderDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal GetCustomerOrderDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<OrderDetailsDto> Handle(GetCustomerOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "[Order].[Id], " +
                               "[Order].[IsRemoved], " +
                               "[Order].[Value], " +
                               "[Order].[Currency] " +
                               "FROM orders.viewOrder AS [Order] " +
                               "WHERE [Order].Id = @OrderId";
            var order = await connection.QuerySingleOrDefaultAsync<OrderDetailsDto>(sql, new { request.OrderId });

            const string sqlBooks = "SELECT " +
                               "[Order].[BookId] AS [Id], " +
                               "[Order].[Quantity], " +
                               "[Order].[Title], " +
                               "[Order].[Value], " +
                               "[Order].[Currency] " +
                               "FROM orders.viewOrderBook AS [Order] " +
                               "WHERE [Order].OrderId = @OrderId";
            var books = await connection.QueryAsync<BookDto>(sqlBooks, new { request.OrderId });

            order.Books = books.AsList();

            return order;
        }
    }
}