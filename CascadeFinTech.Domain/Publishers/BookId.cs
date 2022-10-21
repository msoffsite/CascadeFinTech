using System;
using CascadeFinTech.Domain.SeedWork;

namespace CascadeFinTech.Domain.Publishers
{
    public class PublisherId : TypedIdValueBase
    {
        public PublisherId(Guid value) : base(value)
        {
        }
    }
}