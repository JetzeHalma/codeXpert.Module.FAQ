using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Modules;
using Oqtane.Security;
using Oqtane.Shared;
using codeXpert.Module.FAQ.Repository;

namespace codeXpert.Module.FAQ.Services
{
    public class ServerFAQService : IFAQService, ITransientService
    {
        private readonly IFAQRepository _FAQRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerFAQService(IFAQRepository FAQRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _FAQRepository = FAQRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public async Task<List<Models.FAQ>> GetFAQsAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return (await _FAQRepository.GetFAQsAsync(ModuleId)).ToList();
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FAQ Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public async Task<Models.FAQ> GetFAQAsync(int FAQId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return await _FAQRepository.GetFAQAsync(FAQId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FAQ Get Attempt {FAQId} {ModuleId}", FAQId, ModuleId);
                return null;
            }
        }

        public async Task<Models.FAQ> AddFAQAsync(Models.FAQ FAQ)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, FAQ.ModuleId, PermissionNames.Edit))
            {
                FAQ = await _FAQRepository.AddFAQAsync(FAQ);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "FAQ Added {FAQ}", FAQ);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FAQ Add Attempt {FAQ}", FAQ);
                FAQ = null;
            }
            return FAQ;
        }

        public async Task<Models.FAQ> UpdateFAQAsync(Models.FAQ FAQ)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, FAQ.ModuleId, PermissionNames.Edit))
            {
                FAQ = await _FAQRepository.UpdateFAQAsync(FAQ);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "FAQ Updated {FAQ}", FAQ);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FAQ Update Attempt {FAQ}", FAQ);
                FAQ = null;
            }
            return FAQ;
        }

        public async Task DeleteFAQAsync(int FAQId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                await _FAQRepository.DeleteFAQAsync(FAQId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "FAQ Deleted {FAQId}", FAQId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized FAQ Delete Attempt {FAQId} {ModuleId}", FAQId, ModuleId);
            }
        }
    }
}
