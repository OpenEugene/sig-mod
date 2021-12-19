using Oqtane.Models;
using Oqtane.Modules;

namespace OE.SigMod
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "SigMod",
            Description = "SigMod",
            Version = "1.0.0",
            ServerManagerType = "OE.SigMod.Manager.SigModManager, OE.SigMod.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "OE.SigMod.Shared.Oqtane",
            PackageName = "OE.SigMod" 
        };
    }
}
