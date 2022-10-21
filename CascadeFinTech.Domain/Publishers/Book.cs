using System;
using System.Collections.Generic;
using System.Linq;
using CascadeFinTech.Domain.SeedWork;
using CascadeFinTech.Domain.SharedKernel;

namespace CascadeFinTech.Domain.Publishers
{
    public class Publisher : Entity, IAggregateRoot
    {
        public PublisherId Id { get; private set; }

        public string Name { get; private set; }

        private Publisher()
        {

        }
    }
}