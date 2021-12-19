using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using OE.SigMod.Models;

namespace OE.SigMod.Services
{
    public class SigModService : ServiceBase, ISigModService, IService
    {
        public SigModService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("SigMod");

        public async Task<List<Models.SigMod>> GetSigModsAsync(int ModuleId)
        {
            List<Models.SigMod> SigMods = await GetJsonAsync<List<Models.SigMod>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId));
            return SigMods.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.SigMod> GetSigModAsync(int SigModId, int ModuleId)
        {
            return await GetJsonAsync<Models.SigMod>(CreateAuthorizationPolicyUrl($"{Apiurl}/{SigModId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.SigMod> AddSigModAsync(Models.SigMod SigMod)
        {
            return await PostJsonAsync<Models.SigMod>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, SigMod.ModuleId), SigMod);
        }

        public async Task<Models.SigMod> UpdateSigModAsync(Models.SigMod SigMod)
        {
            return await PutJsonAsync<Models.SigMod>(CreateAuthorizationPolicyUrl($"{Apiurl}/{SigMod.SigModId}", EntityNames.Module, SigMod.ModuleId), SigMod);
        }

        public async Task DeleteSigModAsync(int SigModId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{SigModId}", EntityNames.Module, ModuleId));
        }
    }
}
