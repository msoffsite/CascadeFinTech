using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CascadeFinTech.Domain.Books;
using CascadeFinTech.Infrastructure.Database;
using CascadeFinTech.Infrastructure.SeedWork;

namespace CascadeFinTech.Infrastructure.Domain.Books
{
    public class BookRepository : IBookRepository
    {
        private readonly OrdersContext _context;
        public BookRepository(OrdersContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Book>> GetByIdsAsync(List<BookId> ids)
        {
            return await this._context
                .Books
                .IncludePaths("_prices")
                .Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await this._context
                .Books
                .IncludePaths("_prices")
                .ToListAsync();
        }
    }
}