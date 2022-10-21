using MediatR;
using CascadeFinTech.Application;
using CascadeFinTech.Application.Configuration.Commands;

namespace CascadeFinTech.Infrastructure.Processing.Outbox
{
    public class ProcessOutboxCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}