using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Enums;
using OE.SigMod.Repository;

namespace OE.SigMod.Manager
{
    public class SigModManager : MigratableModuleBase, IInstallable, IPortable
    {
        private ISigModRepository _SigModRepository;
        private readonly ITenantManager _tenantManager;
        private readonly IHttpContextAccessor _accessor;

        public SigModManager(ISigModRepository SigModRepository, ITenantManager tenantManager, IHttpContextAccessor accessor)
        {
            _SigModRepository = SigModRepository;
            _tenantManager = tenantManager;
            _accessor = accessor;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new SigModContext(_tenantManager, _accessor), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new SigModContext(_tenantManager, _accessor), tenant, MigrationType.Down);
        }

        public string ExportModule(Module module)
        {
            string content = "";
            List<Models.SigMod> SigMods = _SigModRepository.GetSigMods(module.ModuleId).ToList();
            if (SigMods != null)
            {
                content = JsonSerializer.Serialize(SigMods);
            }
            return content;
        }

        public void ImportModule(Module module, string content, string version)
        {
            List<Models.SigMod> SigMods = null;
            if (!string.IsNullOrEmpty(content))
            {
                SigMods = JsonSerializer.Deserialize<List<Models.SigMod>>(content);
            }
            if (SigMods != null)
            {
                foreach(var SigMod in SigMods)
                {
                    _SigModRepository.AddSigMod(new Models.SigMod { ModuleId = module.ModuleId, Name = SigMod.Name });
                }
            }
        }
    }
}