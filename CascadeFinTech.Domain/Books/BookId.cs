using System;
using CascadeFinTech.Domain.SeedWork;

namespace CascadeFinTech.Domain.Books
{
    public class BookId : TypedIdValueBase
    {
        public BookId(Guid value) : base(value)
        {
        }
    }
}