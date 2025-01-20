using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using Devin.Module.Targeting.Repository;

namespace Devin.Module.Targeting.Services
{
    public class ServerTargetingService : ITargetingService
    {
        private readonly ITargetingRepository _TargetingRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerTargetingService(ITargetingRepository TargetingRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _TargetingRepository = TargetingRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.Targeting>> GetTargetingsAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_TargetingRepository.GetTargetings(ModuleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Targeting Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public Task<Models.Targeting> GetTargetingAsync(int TargetingId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_TargetingRepository.GetTargeting(TargetingId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Targeting Get Attempt {TargetingId} {ModuleId}", TargetingId, ModuleId);
                return null;
            }
        }

        public Task<Models.Targeting> AddTargetingAsync(Models.Targeting Targeting)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, Targeting.ModuleId, PermissionNames.Edit))
            {
                Targeting = _TargetingRepository.AddTargeting(Targeting);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Targeting Added {Targeting}", Targeting);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Targeting Add Attempt {Targeting}", Targeting);
                Targeting = null;
            }
            return Task.FromResult(Targeting);
        }

        public Task<Models.Targeting> UpdateTargetingAsync(Models.Targeting Targeting)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, Targeting.ModuleId, PermissionNames.Edit))
            {
                Targeting = _TargetingRepository.UpdateTargeting(Targeting);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Targeting Updated {Targeting}", Targeting);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Targeting Update Attempt {Targeting}", Targeting);
                Targeting = null;
            }
            return Task.FromResult(Targeting);
        }

        public Task DeleteTargetingAsync(int TargetingId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                _TargetingRepository.DeleteTargeting(TargetingId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Targeting Deleted {TargetingId}", TargetingId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Targeting Delete Attempt {TargetingId} {ModuleId}", TargetingId, ModuleId);
            }
            return Task.CompletedTask;
        }
    }
}
