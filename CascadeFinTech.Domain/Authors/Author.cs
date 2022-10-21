using System;
using System.Collections.Generic;
using System.Linq;
using CascadeFinTech.Domain.SeedWork;
using CascadeFinTech.Domain.SharedKernel;

namespace CascadeFinTech.Domain.Authors
{
    public class Author : Entity, IAggregateRoot
    {
        public AuthorId Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        private Author()
        {

        }
    }
}