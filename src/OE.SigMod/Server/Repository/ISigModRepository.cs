using System.Collections.Generic;
using OE.SigMod.Models;

namespace OE.SigMod.Repository
{
    public interface ISigModRepository
    {
        IEnumerable<Models.SigMod> GetSigMods(int ModuleId);
        Models.SigMod GetSigMod(int SigModId);
        Models.SigMod GetSigMod(int SigModId, bool tracking);
        Models.SigMod AddSigMod(Models.SigMod SigMod);
        Models.SigMod UpdateSigMod(Models.SigMod SigMod);
        void DeleteSigMod(int SigModId);
    }
}
