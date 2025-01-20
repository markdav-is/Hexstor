using Oqtane.Models;
using Oqtane.Modules;

namespace Devin.Module.Targeting
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Targeting",
            Description = "Provides essential targeting statistics between grid cells. ",
            Version = "1.0.0",
            ServerManagerType = "Devin.Module.Targeting.Manager.TargetingManager, Devin.Module.Targeting.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "Devin.Module.Targeting.Shared.Oqtane",
            PackageName = "Devin.Module.Targeting" 
        };
    }
}
