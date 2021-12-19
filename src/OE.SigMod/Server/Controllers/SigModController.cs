using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using OE.SigMod.Repository;
using Oqtane.Controllers;
using System.Net;

namespace OE.SigMod.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class SigModController : ModuleControllerBase
    {
        private readonly ISigModRepository _SigModRepository;

        public SigModController(ISigModRepository SigModRepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _SigModRepository = SigModRepository;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.SigMod> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && ModuleId == AuthEntityId(EntityNames.Module))
            {
                return _SigModRepository.GetSigMods(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized SigMod Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.SigMod Get(int id)
        {
            Models.SigMod SigMod = _SigModRepository.GetSigMod(id);
            if (SigMod != null && SigMod.ModuleId == AuthEntityId(EntityNames.Module))
            {
                return SigMod;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized SigMod Get Attempt {SigModId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.SigMod Post([FromBody] Models.SigMod SigMod)
        {
            if (ModelState.IsValid && SigMod.ModuleId == AuthEntityId(EntityNames.Module))
            {
                SigMod = _SigModRepository.AddSigMod(SigMod);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "SigMod Added {SigMod}", SigMod);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized SigMod Post Attempt {SigMod}", SigMod);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                SigMod = null;
            }
            return SigMod;
        }

        // PUT api/<controller>/5
        [ValidateAntiForgeryToken]
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.SigMod Put(int id, [FromBody] Models.SigMod SigMod)
        {
            if (ModelState.IsValid && SigMod.ModuleId == AuthEntityId(EntityNames.Module) && _SigModRepository.GetSigMod(SigMod.SigModId, false) != null)
            {
                SigMod = _SigModRepository.UpdateSigMod(SigMod);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "SigMod Updated {SigMod}", SigMod);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized SigMod Put Attempt {SigMod}", SigMod);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                SigMod = null;
            }
            return SigMod;
        }

        // DELETE api/<controller>/5
        [ValidateAntiForgeryToken]
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.SigMod SigMod = _SigModRepository.GetSigMod(id);
            if (SigMod != null && SigMod.ModuleId == AuthEntityId(EntityNames.Module))
            {
                _SigModRepository.DeleteSigMod(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "SigMod Deleted {SigModId}", id);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized SigMod Delete Attempt {SigModId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
