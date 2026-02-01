// OMS.Application/Services/Interfaces/IDiscountEngine.cs
using OMS.Domain.Models;
using OMS.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IDiscountEngine
    {
        Task<DiscountSummary> ApplyDiscountAsync(Order order, CancellationToken ct = default);
    }
}