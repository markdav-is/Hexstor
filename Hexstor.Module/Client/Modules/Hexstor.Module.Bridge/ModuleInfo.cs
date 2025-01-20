using Oqtane.Models;
using Oqtane.Modules;

namespace Hexstor.Module.Bridge
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Bridge",
            Description = "The Hexstor Ship Control Panel",
            Version = "1.0.0",
            ServerManagerType = "Hexstor.Module.Template.Manager.TemplateManager, Hexstor.Module.Template.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "Hexstor.Module.Template.Shared.Oqtane,MudBlazor",
            PackageName = "Hexstor.Template" 
        };
    }
}
