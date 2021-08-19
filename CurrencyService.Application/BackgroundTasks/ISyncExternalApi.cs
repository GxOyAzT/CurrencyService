using System.Threading;
using System.Threading.Tasks;

namespace CurrencyService.Application.BackgroundTasks
{
    public interface ISyncExternalApi
    {
        Task Start(CancellationToken cancellationToken);
    }
}