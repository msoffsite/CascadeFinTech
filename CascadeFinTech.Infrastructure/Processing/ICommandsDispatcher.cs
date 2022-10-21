using System;
using System.Threading.Tasks;

namespace CascadeFinTech.Infrastructure.Processing
{
    public interface ICommandsDispatcher
    {
        Task DispatchCommandAsync(Guid id);
    }
}
