using Oqtane.Models;
using Oqtane.Modules;

namespace Hexstor.Module.Ink
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Ink",
            Description = "plays ink",
            Version = "1.0.3",
            ServerManagerType = "Hexstor.Module.Ink.Manager.InkManager, Hexstor.Module.Ink.Server.Oqtane",
            ReleaseVersions = "1.0.3",
            Dependencies = "Hexstor.Module.Ink.Shared.Oqtane",
            PackageName = "Hexstor.Module.Ink"
        };
    }
}
