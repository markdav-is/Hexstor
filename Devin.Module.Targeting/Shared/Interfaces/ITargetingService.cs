using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devin.Module.Targeting.Services
{
    public interface ITargetingService 
    {
        Task<List<Models.Targeting>> GetTargetingsAsync(int ModuleId);

        Task<Models.Targeting> GetTargetingAsync(int TargetingId, int ModuleId);

        Task<Models.Targeting> AddTargetingAsync(Models.Targeting Targeting);

        Task<Models.Targeting> UpdateTargetingAsync(Models.Targeting Targeting);

        Task DeleteTargetingAsync(int TargetingId, int ModuleId);
    }
}
