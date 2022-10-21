using MediatR;
using CascadeFinTech.Application;
using CascadeFinTech.Application.Configuration.Commands;
using CascadeFinTech.Infrastructure.Processing.Outbox;

namespace CascadeFinTech.Infrastructure.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}