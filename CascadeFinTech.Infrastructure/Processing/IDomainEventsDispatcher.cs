using System.Threading.Tasks;

namespace CascadeFinTech.Infrastructure.Processing
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}