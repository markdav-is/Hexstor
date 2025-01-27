using Oqtane.Models;
using Oqtane.Modules;

namespace Hexstor.Module.GIF
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "GIF",
            Description = "The Hexstor GIF Module",
            Version = "1.0.3",
            ServerManagerType = "Hexstor.Module.Template.Manager.TemplateManager, Hexstor.Module.Template.Server.Oqtane",
            ReleaseVersions = "1.0.3",
            Dependencies = "Hexstor.Module.Template.Shared.Oqtane",
            PackageName = "Hexstor.Template" 
        };
    }
}
