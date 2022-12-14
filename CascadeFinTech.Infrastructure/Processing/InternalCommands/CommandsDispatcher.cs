using System;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using CascadeFinTech.Application.Configuration.Processing;
using CascadeFinTech.Application.Customers;
using CascadeFinTech.Infrastructure.Database;

namespace CascadeFinTech.Infrastructure.Processing.InternalCommands
{
    public class CommandsDispatcher : ICommandsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly OrdersContext _ordersContext;

        public CommandsDispatcher(
            IMediator mediator, 
            OrdersContext ordersContext)
        {
            _mediator = mediator;
            _ordersContext = ordersContext;
        }

        public async Task DispatchCommandAsync(Guid id)
        {
            var internalCommand = await _ordersContext.InternalCommands.SingleOrDefaultAsync(x => x.Id == id);

            Type type = Assembly.GetAssembly(typeof(MarkCustomerAsWelcomedCommand)).GetType(internalCommand.Type);
            dynamic command = JsonConvert.DeserializeObject(internalCommand.Data, type);

            internalCommand.ProcessedDate = DateTime.UtcNow;

            await _mediator.Send(command);
        }
    }
}