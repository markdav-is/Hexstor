using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Services;
using Oqtane.Shared;

namespace Devin.Module.Targeting.Services
{
    public class TargetingService : ServiceBase, ITargetingService
    {
        public TargetingService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("Targeting");

        public async Task<List<Models.Targeting>> GetTargetingsAsync(int ModuleId)
        {
            List<Models.Targeting> Targetings = await GetJsonAsync<List<Models.Targeting>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.Targeting>().ToList());
            return Targetings.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.Targeting> GetTargetingAsync(int TargetingId, int ModuleId)
        {
            return await GetJsonAsync<Models.Targeting>(CreateAuthorizationPolicyUrl($"{Apiurl}/{TargetingId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.Targeting> AddTargetingAsync(Models.Targeting Targeting)
        {
            return await PostJsonAsync<Models.Targeting>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, Targeting.ModuleId), Targeting);
        }

        public async Task<Models.Targeting> UpdateTargetingAsync(Models.Targeting Targeting)
        {
            return await PutJsonAsync<Models.Targeting>(CreateAuthorizationPolicyUrl($"{Apiurl}/{Targeting.TargetingId}", EntityNames.Module, Targeting.ModuleId), Targeting);
        }

        public async Task DeleteTargetingAsync(int TargetingId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{TargetingId}", EntityNames.Module, ModuleId));
        }
    }
}
