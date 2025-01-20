using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devin.Module.Targeting.Repository
{
    public interface ITargetingRepository
    {
        IEnumerable<Models.Targeting> GetTargetings(int ModuleId);
        Models.Targeting GetTargeting(int TargetingId);
        Models.Targeting GetTargeting(int TargetingId, bool tracking);
        Models.Targeting AddTargeting(Models.Targeting Targeting);
        Models.Targeting UpdateTargeting(Models.Targeting Targeting);
        void DeleteTargeting(int TargetingId);
    }
}
