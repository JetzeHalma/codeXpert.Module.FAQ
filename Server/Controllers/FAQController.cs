using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using codeXpert.Module.FAQ.Repository;
using Oqtane.Controllers;
using System.Net;

namespace codeXpert.Module.FAQ.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class FAQController : ModuleControllerBase
    {
        private readonly IFAQRepository _FAQRepository;

        public FAQController(IFAQRepository FAQRepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _FAQRepository = FAQRepository;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.FAQ> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return _FAQRepository.GetFAQs(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FAQ Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.FAQ Get(int id)
        {
            Models.FAQ FAQ = _FAQRepository.GetFAQ(id);
            if (FAQ != null && IsAuthorizedEntityId(EntityNames.Module, FAQ.ModuleId))
            {
                return FAQ;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FAQ Get Attempt {FAQId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.FAQ Post([FromBody] Models.FAQ FAQ)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, FAQ.ModuleId))
            {
                FAQ = _FAQRepository.AddFAQ(FAQ);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "FAQ Added {FAQ}", FAQ);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FAQ Post Attempt {FAQ}", FAQ);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                FAQ = null;
            }
            return FAQ;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.FAQ Put(int id, [FromBody] Models.FAQ FAQ)
        {
            if (ModelState.IsValid && FAQ.FAQId == id && IsAuthorizedEntityId(EntityNames.Module, FAQ.ModuleId) && _FAQRepository.GetFAQ(FAQ.FAQId, false) != null)
            {
                FAQ = _FAQRepository.UpdateFAQ(FAQ);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "FAQ Updated {FAQ}", FAQ);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FAQ Put Attempt {FAQ}", FAQ);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                FAQ = null;
            }
            return FAQ;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.FAQ FAQ = _FAQRepository.GetFAQ(id);
            if (FAQ != null && IsAuthorizedEntityId(EntityNames.Module, FAQ.ModuleId))
            {
                _FAQRepository.DeleteFAQ(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "FAQ Deleted {FAQId}", id);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FAQ Delete Attempt {FAQId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
