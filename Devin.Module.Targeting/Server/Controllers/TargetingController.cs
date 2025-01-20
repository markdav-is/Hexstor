using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Devin.Module.Targeting.Repository;
using Oqtane.Controllers;
using System.Net;

namespace Devin.Module.Targeting.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class TargetingController : ModuleControllerBase
    {
        private readonly ITargetingRepository _TargetingRepository;

        public TargetingController(ITargetingRepository TargetingRepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _TargetingRepository = TargetingRepository;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.Targeting> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return _TargetingRepository.GetTargetings(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Targeting Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.Targeting Get(int id)
        {
            Models.Targeting Targeting = _TargetingRepository.GetTargeting(id);
            if (Targeting != null && IsAuthorizedEntityId(EntityNames.Module, Targeting.ModuleId))
            {
                return Targeting;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Targeting Get Attempt {TargetingId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Targeting Post([FromBody] Models.Targeting Targeting)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, Targeting.ModuleId))
            {
                Targeting = _TargetingRepository.AddTargeting(Targeting);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Targeting Added {Targeting}", Targeting);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Targeting Post Attempt {Targeting}", Targeting);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Targeting = null;
            }
            return Targeting;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Targeting Put(int id, [FromBody] Models.Targeting Targeting)
        {
            if (ModelState.IsValid && Targeting.TargetingId == id && IsAuthorizedEntityId(EntityNames.Module, Targeting.ModuleId) && _TargetingRepository.GetTargeting(Targeting.TargetingId, false) != null)
            {
                Targeting = _TargetingRepository.UpdateTargeting(Targeting);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Targeting Updated {Targeting}", Targeting);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Targeting Put Attempt {Targeting}", Targeting);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Targeting = null;
            }
            return Targeting;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.Targeting Targeting = _TargetingRepository.GetTargeting(id);
            if (Targeting != null && IsAuthorizedEntityId(EntityNames.Module, Targeting.ModuleId))
            {
                _TargetingRepository.DeleteTargeting(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Targeting Deleted {TargetingId}", id);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Targeting Delete Attempt {TargetingId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
