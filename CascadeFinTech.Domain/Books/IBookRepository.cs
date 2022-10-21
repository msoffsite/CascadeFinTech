using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CascadeFinTech.Domain.Books
{
    public interface IBookRepository
    {
        Task<List<Book>> GetByIdsAsync(List<BookId> ids);

        Task<List<Book>> GetAllAsync();
    }
}