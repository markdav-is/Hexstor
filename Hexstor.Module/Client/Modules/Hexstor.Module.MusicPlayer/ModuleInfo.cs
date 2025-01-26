using Oqtane.Models;
using Oqtane.Modules;

namespace Hexstor.Module.MusicPlayer
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "MusicPlayer",
            Description = "A module for playing background music.",
            Version = "1.0.0",
            PackageName = "Hexstor.Module.MusicPlayer"
        };
    }
}
