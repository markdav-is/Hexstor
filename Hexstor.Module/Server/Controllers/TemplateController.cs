using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Hexstor.Module.Template.Repository;
using Oqtane.Controllers;
using System.Net;
using System.Threading.Tasks;
using System;

namespace Hexstor.Module.Template.Controllers;

[Route(ControllerRoutes.ApiRoute)]
public class TemplateController : ModuleControllerBase
{
    private readonly TemplateRepository _TemplateRepository;

    public TemplateController(TemplateRepository TemplateRepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
    {
        _TemplateRepository = TemplateRepository;
    }

    // GET: api/<controller>?moduleid=x
    [HttpGet]
    [Authorize(Roles = RoleNames.Registered)]
    public async Task<ActionResult<IEnumerable<Models.Template>>> Get(){
        try{
            var data = _TemplateRepository.GetTemplates();
            return Ok(data);
        }
        catch(Exception ex){
            var errorMessage = $"Repository Error Get Attempt Template";
            _logger.Log(LogLevel.Error, this, LogFunction.Read, errorMessage);
            return StatusCode(500);
        }
    }

    // GET api/<controller>/5
    [HttpGet("{id}")]
    [Authorize(Roles = RoleNames.Registered)]
    public async Task<ActionResult<Models.Template>> Get(int id)
    {
        try {
            var data = _TemplateRepository.GetTemplate(id);
            return Ok(data);
        }
        catch (Exception ex)       { 
            _logger.Log(LogLevel.Error, this, LogFunction.Read, "Failed Template Get Attempt {id}", id);
            return StatusCode(500);
        }
    }

    // POST api/<controller>
    [HttpPost]
    [Authorize(Roles = RoleNames.Registered)]
    public async Task<ActionResult<Models.Template>> Post([FromBody] Models.Template Template)
    {
        if (ModelState.IsValid)
        {
            try{
                Template = _TemplateRepository.AddTemplate(Template);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Template Added {Template}", Template);
            }
            catch (Exception ex) {
                _logger.Log(LogLevel.Error, this, LogFunction.Read, "Failed Template Add Attempt {Template} Message {Message} ", Template,ex.Message);
                return StatusCode(500);
            }
        }
        else
        {
            _logger.Log(LogLevel.Error, this, LogFunction.Create, "Invaid Template Post Attempt {Template}", Template);
            return BadRequest();
        }
        return Ok(Template);
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    [Authorize(Roles = RoleNames.Registered)]
    public async Task<ActionResult<Models.Template>> Put(int id, [FromBody] Models.Template Template)
    {
        if (ModelState.IsValid && _TemplateRepository.GetTemplate(Template.TemplateId, false) != null)
        {
            Template = _TemplateRepository.UpdateTemplate(Template);
            _logger.Log(LogLevel.Information, this, LogFunction.Update, "Template Updated {Template}", Template);
            return Ok(Template);
        }
        else
        {
            _logger.Log(LogLevel.Error, this, LogFunction.Update, "Unauthorized Template Put Attempt {Template}", Template);
            return BadRequest();
        }
    }

    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    [Authorize(Roles = RoleNames.Registered)]
    public async Task<ActionResult> Delete(int id)
    {
        var data = _TemplateRepository.GetTemplate(id);
        if (data is null)
        {
            _logger.Log(LogLevel.Error, this, LogFunction.Delete, "Failed Template Delete Attempt {TemplateId}", id);
            return NotFound();
        }

        _TemplateRepository.DeleteTemplate(id);
        _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Template Deleted {TemplateId}", id);
        return Ok();
    
    }
}