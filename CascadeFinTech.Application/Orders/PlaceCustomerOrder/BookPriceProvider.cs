using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using CascadeFinTech.Domain.Books;
using CascadeFinTech.Domain.SharedKernel;

namespace CascadeFinTech.Application.Orders.PlaceCustomerOrder
{
    public static class BookPriceProvider
    {
        public static async Task<List<BookPriceData>> GetAllBookPrices(IDbConnection connection)
        {
            var bookPrices = await connection.QueryAsync<BookPriceResponse>("SELECT " +
                                                                                  $"[BookPrice].BookId AS [{nameof(BookPriceResponse.BookId)}], " +
                                                                                  $"[BookPrice].Value AS [{nameof(BookPriceResponse.Value)}], " +
                                                                                  $"[BookPrice].Currency AS [{nameof(BookPriceResponse.Currency)}] " +
                                                                                  "FROM orders.viewBookPrice AS [BookPrice]");

            return bookPrices.AsList()
                .Select(x => new BookPriceData(
                    new BookId(x.BookId),
                    MoneyValue.Of(x.Value, x.Currency)))
                .ToList();
        }

        private sealed class BookPriceResponse
        {
            public Guid BookId { get; set; }

            public decimal Value { get; set; }

            public string Currency { get; set; }
        }
    }
}