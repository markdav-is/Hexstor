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
using Hexstor.Module.Client.ViewModels;
using System.Reflection.Metadata;
using System.Reflection;

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
        new Resource { ResourceType = ResourceType.Script,      Url = "_content/MudBlazor/MudBlazor.min.js", Location = Oqtane.Shared.ResourceLocation.Body, Level = ResourceLevel.Site },
        new Resource { ResourceType = ResourceType.Script,      Url = ModulePath() + "Module.js" },
    };	
    private bool IsLoaded;
    private SettingsViewModel _settingsVM;
    private string _shipCode;
    private string _shipImage;


    public bool isRangeFinderShown { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var moduleSettings = await SettingService.GetModuleSettingsAsync(ModuleState.ModuleId);
            _settingsVM = new SettingsViewModel(SettingService, moduleSettings);
            _shipCode = _settingsVM.ShipStyle.ToString().Substring(0, 1);
            _shipImage = $"/images/Ship{_shipCode}/Ship_LVL_{_settingsVM.ShipLevel}.png";
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
        await ToggleRangeFinder(false);
        SiteState.Properties.Command = new Command
        {
            Type = CommandType.Play,
            PlayerId = ModuleState.ModuleId,
            StartCorner = _settingsVM.StartCorner,
            ShipStyle = _settingsVM.ShipStyle,
        };
    }

    private async Task Left()
    {
        await ToggleRangeFinder(false);
        SiteState.Properties.Command = new Command
        {
            Type = CommandType.Left,
            PlayerId = ModuleState.ModuleId,
        };
    }

    private async Task Right()
    {
        await ToggleRangeFinder(false);
        SiteState.Properties.Command = new Command
        {
            Type = CommandType.Right,
            PlayerId = ModuleState.ModuleId,
        };
    }

    private async Task Forward()
    {
        await ToggleRangeFinder(false);
        SiteState.Properties.Command = new Command
        {
            Type = CommandType.Forward,
            PlayerId = ModuleState.ModuleId,
        };
    }

    private async Task Fire()
    {
        await ToggleRangeFinder(false);
        SiteState.Properties.Command = new Command
        {
            Type = CommandType.Fire,
            PlayerId = ModuleState.ModuleId,
        };
    }

    private async Task ToggleRangeFinder(bool? setVisible = true)
    {
        isRangeFinderShown = (bool)((setVisible.HasValue) ? setVisible : !isRangeFinderShown);
        SiteState.Properties.Command = new Command
        {
            Type = CommandType.ToggleRangeFinder,
            PlayerId = ModuleState.ModuleId,
            AttackRange = _settingsVM.AttackRange,
            AttackShape = _settingsVM.RangeShape,
            RangeFinderVisible = isRangeFinderShown
        };
        
    }


    static bool IsSuccessStatusCode(HttpStatusCode statusCode) { 
        return (int)statusCode >= 200 && (int)statusCode <= 299; 
    }
}

