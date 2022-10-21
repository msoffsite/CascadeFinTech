using System;
using CascadeFinTech.Domain.SeedWork;

namespace CascadeFinTech.Domain.Authors
{
    public class AuthorId : TypedIdValueBase
    {
        public AuthorId(Guid value) : base(value)
        {
        }
    }
}