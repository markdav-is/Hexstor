using Oqtane.Models;
using Oqtane.Modules;

namespace Hexstor.Module.MusicPlayer
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Music Player",
            Description = "The Hexstor Music Module",
            Version = "1.0.4",
            ServerManagerType = "Hexstor.Module.Template.Manager.TemplateManager, Hexstor.Module.Template.Server.Oqtane",
            ReleaseVersions = "1.0.4",
            Dependencies = "Hexstor.Module.Music.Shared.Oqtane",
            PackageName = "Hexstor.Template"
        };
    }
}
