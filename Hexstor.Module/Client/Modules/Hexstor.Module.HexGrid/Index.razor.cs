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
using Hexstor.Module.Client.ViewModels;
using Hexstor.Module.Template.Services;
using System.ComponentModel;
using System.Linq;

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
    private List<Hex> _hexes = new List<Hex>();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var moduleSettings = await SettingService.GetModuleSettingsAsync(ModuleState.ModuleId);
            _settingsVM = new SettingsViewModel(SettingService, moduleSettings);

            // build array of hexes
            for (int row = 0; row < _settingsVM.Rows; row++)
            {
                for (int col = 0; col < _settingsVM.Columns; col++)
                {
                    _hexes.Add(new Hex {DoubCoord = new DoubCoord(row, col)});
                }
            }
            IsLoaded = true;
        }
        catch (Exception ex)
        {
            await logger.LogError(ex, "Error Loading Template {Error}", ex.Message);
            AddModuleMessage(Localizer["Message.LoadError"], MessageType.Error);
        }

        ((INotifyPropertyChanged)SiteState.Properties).PropertyChanged += HandlePropertyChanged;

    }

    private void HandlePropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "Command")
        {
            if (SiteState.Properties.Command == "Forward")
            {
                MoveShipForward();
            }
            if (SiteState.Properties.Command == "Play")
            {
                ResetShip();
            }

        }
    }

    private void MoveShipForward()
    {
        // find hex with a ship
        var shipHex = _hexes.FirstOrDefault(h => h.Ship != null);
        if (shipHex == null)
        {
            return;  // no ship, no work to do
        }
        // find the ship
        var ship = shipHex.Ship;

        // find the next hex depending on the heading using the double cooordinate system
        var forwardCoord = shipHex.DoubCoord.Forward(ship.Heading);
        var newHex = _hexes.FirstOrDefault(
            h => h.DoubCoord.Row == forwardCoord.Row
            && h.DoubCoord.Col == forwardCoord.Col);

        if (newHex == null)
        {
            return;  // can't fly off the map
        }

        // make a copy of the ship and add it to the new hex
        newHex.Ship = new Ship
        {
            Heading = ship.Heading,
            Style = ship.Style,
            Level = ship.Level
        };

        // remove the ship from the current hex
        shipHex.Ship = null;
        StateHasChanged();
    }

    private void ResetShip() {
        // remove all ships from the hexes
        foreach (var hex in _hexes.Where(h => h.Ship != null))
        {
            hex.Ship = null;
        }
        // add a ship to the first hex
        _hexes.First().Ship = new Ship { Heading = 3, Style = ShipStyle.Red, Level = 2 };
        StateHasChanged();
    }


    public void Dispose()
    {
        ((INotifyPropertyChanged)SiteState.Properties).PropertyChanged -= HandlePropertyChanged;
    }


}

