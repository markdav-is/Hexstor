using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Oqtane.Models;
using Oqtane.Modules;
using Oqtane.Shared;
using Oqtane.Services;

using Hexstor.Module.Template.Services;

namespace Hexstor.Module.Bridge;

public partial class Index : ModuleBase
{
    	
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public ISettingService SettingService { get; set; }
	
    public override List<Resource> Resources => new List<Resource>()
    {
        new Resource { ResourceType = ResourceType.Stylesheet,  Url = "https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap" },
        new Resource { ResourceType = ResourceType.Stylesheet,  Url = "_content/MudBlazor/MudBlazor.min.css" },
        new Resource { ResourceType = ResourceType.Stylesheet,  Url = ModulePath() + "Module.css" },
        new Resource { ResourceType = ResourceType.Script,      Url = "_content/MudBlazor/MudBlazor.min.js", Location = ResourceLocation.Body, Level = ResourceLevel.Site },
        new Resource { ResourceType = ResourceType.Script,      Url = ModulePath() + "Module.js" },
    };	
    private bool IsLoaded;
    private SettingsViewModel _settingsVM; 

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var moduleSettings = await SettingService.GetModuleSettingsAsync(ModuleState.ModuleId);
            _settingsVM = new SettingsViewModel(SettingService, moduleSettings);
           
            IsLoaded = true;
        }
        catch (Exception ex)
        {
            await logger.LogError(ex, "Error Loading Settings {Error}", ex.Message);
            AddModuleMessage("Error loading Settings", MessageType.Error);
        }
    }

    private async Task Play()
    {
        SiteState.Properties.Command = "Play";
    }

    private async Task Left()
    {
        SiteState.Properties.Command = "Left";

    }

    private async Task Right()
    {
        SiteState.Properties.Command = "Right";

    }

    private async Task Forward()
    {
        SiteState.Properties.Command = "Forward";

    }

    private async Task Fire()
    {
        SiteState.Properties.Command = "Fire";
    }


    static bool IsSuccessStatusCode(HttpStatusCode statusCode) { 
        return (int)statusCode >= 200 && (int)statusCode <= 299; 
    }
}

