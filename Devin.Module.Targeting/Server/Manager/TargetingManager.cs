using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Enums;
using Oqtane.Repository;
using Devin.Module.Targeting.Repository;
using System.Threading.Tasks;

namespace Devin.Module.Targeting.Manager
{
    public class TargetingManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly ITargetingRepository _TargetingRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public TargetingManager(ITargetingRepository TargetingRepository, IDBContextDependencies DBContextDependencies)
        {
            _TargetingRepository = TargetingRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new TargetingContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new TargetingContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.Targeting> Targetings = _TargetingRepository.GetTargetings(module.ModuleId).ToList();
            if (Targetings != null)
            {
                content = JsonSerializer.Serialize(Targetings);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.Targeting> Targetings = null;
            if (!string.IsNullOrEmpty(content))
            {
                Targetings = JsonSerializer.Deserialize<List<Models.Targeting>>(content);
            }
            if (Targetings != null)
            {
                foreach(var Targeting in Targetings)
                {
                    _TargetingRepository.AddTargeting(new Models.Targeting { ModuleId = module.ModuleId, Name = Targeting.Name });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var Targeting in _TargetingRepository.GetTargetings(pageModule.ModuleId))
           {
               if (Targeting.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "DevinTargeting",
                       EntityId = Targeting.TargetingId.ToString(),
                       Title = Targeting.Name,
                       Body = Targeting.Name,
                       ContentModifiedBy = Targeting.ModifiedBy,
                       ContentModifiedOn = Targeting.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
