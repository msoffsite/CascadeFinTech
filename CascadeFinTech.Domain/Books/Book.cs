using System;
using System.Collections.Generic;
using System.Linq;
using CascadeFinTech.Domain.SeedWork;
using CascadeFinTech.Domain.SharedKernel;
using CascadeFinTech.Domain.Authors;
using CascadeFinTech.Domain.Publishers;

namespace CascadeFinTech.Domain.Books
{
    public class Book : Entity, IAggregateRoot
    {
        public BookId Id { get; private set; }

        public string Title { get; private set; }

        public Author _author;
        public Publisher _publisher;
        private List<BookPrice> _prices;

        private Book()
        {

        }
    }
}