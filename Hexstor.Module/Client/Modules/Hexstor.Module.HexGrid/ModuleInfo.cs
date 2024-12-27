using Oqtane.Models;
using Oqtane.Modules;

namespace Hexstor.Module.HexGrid
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "HexGrid",
            Description = "The Hexstor HexGrid Module",
            Version = "1.0.0",
            ServerManagerType = "Hexstor.Module.Template.Manager.TemplateManager, Hexstor.Module.Template.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "Hexstor.Module.Template.Shared.Oqtane,MudBlazor",
            PackageName = "Hexstor.Template" 
        };
    }
}
