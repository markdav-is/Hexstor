using Oqtane.Models;
using Oqtane.Modules;
using Oqtane.Shared;
using System.Collections.Generic;


namespace Hexstor.Module.Lottie
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Lottie",
            Description = "plays lotties",
            Version = "1.0.3",
            ServerManagerType = "Trailblazor.Module.Lottie.Manager.LottieManager, Trailblazor.Module.Lottie.Server.Oqtane",
            ReleaseVersions = "1.0.3",
            Dependencies = "Trailblazor.Module.Lottie.Shared.Oqtane",
            PackageName = "Trailblazor.Module.Lottie",
            Resources = new List<Resource>()
            {
                //load the lottie-web component library
                new Resource { ResourceType = ResourceType.Script,
                    ES6Module=true, Location=ResourceLocation.Body,
                    Url = "https://unpkg.com/@lottiefiles/dotlottie-wc@latest/dist/dotlottie-wc.js" }
            }
        };
    }
}
