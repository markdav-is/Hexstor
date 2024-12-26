using System.Collections.Generic;
using Oqtane.Models;
using Oqtane.Themes;
using Oqtane.Shared;

namespace Hexstor.Theme.Space
{
    public class ThemeInfo : ITheme
    {
        public Oqtane.Models.Theme Theme => new Oqtane.Models.Theme
        {
            Name = "Hexstor Space",
            Version = "1.0.0",
            PackageName = "Hexstor.Theme.Space",
            ThemeSettingsType = "Hexstor.Theme.Space.ThemeSettings, Hexstor.Theme.Space.Client.Oqtane",
            ContainerSettingsType = "Hexstor.Theme.Space.ContainerSettings, Hexstor.Theme.Space.Client.Oqtane",
            Resources = new List<Resource>()
            {
                // Bootstrap for Oqtane
                // Check out https://github.com/leigh-pointer/MudOqtaneRazorControls 
		        // obtained from https://cdnjs.com/libraries
                new Resource { ResourceType = ResourceType.Stylesheet, Url = "https://cdnjs.cloudflare.com/ajax/libs/bootswatch/5.3.3/cyborg/bootstrap.min.css",
                    Integrity = "sha512-M+Wrv9LTvQe81gFD2ZE3xxPTN5V2n1iLCXsldIxXvfs6tP+6VihBCwCMBkkjkQUZVmEHBsowb9Vqsq1et1teEg==",
                    CrossOrigin = "anonymous" },
                new Resource { ResourceType = ResourceType.Script, Url = "https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.3/js/bootstrap.bundle.min.js",
                    Integrity = "sha512-7Pi/otdlbbCR+LnW+F7PwFcSDJOuUJB3OxtEHbg4vSMvzvJjde4Po1v4BR9Gdc9aXNUNFVUY+SK51wWT8WF0Gg==",
                    CrossOrigin = "anonymous", Location = ResourceLocation.Body },

					
                //MudBlazor
                new Resource { ResourceType = ResourceType.Stylesheet, Url = "https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap" },
                new Resource { ResourceType = ResourceType.Stylesheet, Url = "_content/MudBlazor/MudBlazor.min.css" },
                new Resource { ResourceType = ResourceType.Script,     Url = "_content/MudBlazor/MudBlazor.min.js", Location = ResourceLocation.Body, Level = ResourceLevel.Site },

                new Resource { ResourceType = ResourceType.Stylesheet, Url = "~/Theme.css"},
            }
        };
    }
}
