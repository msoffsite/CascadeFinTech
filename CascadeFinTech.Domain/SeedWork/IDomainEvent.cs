using System;
using MediatR;

namespace CascadeFinTech.Domain.SeedWork
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}