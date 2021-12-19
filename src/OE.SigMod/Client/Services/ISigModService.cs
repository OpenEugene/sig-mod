using System.Collections.Generic;
using System.Threading.Tasks;
using OE.SigMod.Models;

namespace OE.SigMod.Services
{
    public interface ISigModService 
    {
        Task<List<Models.SigMod>> GetSigModsAsync(int ModuleId);

        Task<Models.SigMod> GetSigModAsync(int SigModId, int ModuleId);

        Task<Models.SigMod> AddSigModAsync(Models.SigMod SigMod);

        Task<Models.SigMod> UpdateSigModAsync(Models.SigMod SigMod);

        Task DeleteSigModAsync(int SigModId, int ModuleId);
    }
}
