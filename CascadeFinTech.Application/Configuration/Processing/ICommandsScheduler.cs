using System.Threading.Tasks;
using MediatR;
using CascadeFinTech.Application.Configuration.Commands;

namespace CascadeFinTech.Application.Configuration.Processing
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync<T>(ICommand<T> command);
    }
}