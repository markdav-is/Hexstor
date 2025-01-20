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
using Hexstor.Module.Shared.Models;
using Hexstor.Module.Template.Services;
using Microsoft.Extensions.Logging;

namespace Hexstor.Module.HexGrid;

public partial class Index : ModuleBase
{

    [Inject] public TemplateService TemplateService { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IStringLocalizer<Index> Localizer { get; set; }
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
    private Dictionary<DoubCoord, Hex> _gridCells = [];

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var moduleSettings = await SettingService.GetModuleSettingsAsync(ModuleState.ModuleId);
            _settingsVM = new SettingsViewModel(SettingService, moduleSettings);
            for (int row = 0; row < _settingsVM.Rows; row++)
            {
                for (int col = 0; col < _settingsVM.Columns; col++)
                {
                    var hex = new Hex
                    {
                        DoubCoord = new DoubCoord(row, col)
                    };
                    if (row == 0 && col == 0)
                    {
                        hex.Ship = new Ship
                        {
                            Heading = 3
                        };
                    }
                    _gridCells.Add(hex.DoubCoord, hex);
                }
            }

            IsLoaded = true;
        }
        catch (Exception ex)
        {
            await logger.LogError(ex, "Error Loading Template {Error}", ex.Message);
            AddModuleMessage(Localizer["Message.LoadError"], MessageType.Error);
        }
    }

    private async Task HexSelected(Hex hex)
    {
        foreach (var coord in _gridCells.Keys)
        {
            _gridCells[coord].Selected = false;
        }
        _gridCells[hex.DoubCoord].Selected = true;
        await InvokeAsync(StateHasChanged);
        await logger.LogInformation("Selected Coords {DoubCoord}",hex.DoubCoord.ToString());
    }

}

