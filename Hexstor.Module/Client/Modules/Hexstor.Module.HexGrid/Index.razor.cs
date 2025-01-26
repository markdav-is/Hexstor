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
using Oqtane.Documentation;

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

            if (SiteState.Properties.Command is Command)
            {
                var command = (Command)SiteState.Properties.Command;
                if (command.Type == CommandType.Forward)
                {
                    MoveShipForward(command.PlayerId);
                }
                if (command.Type == CommandType.Play)
                {
                    ResetShip(command);
                }
                if (command.Type == CommandType.ToggleRangeFinder)
                {
                    ToggleRangeFinder(command.PlayerId, null);
                }

            }
        }
    }
    private void MoveShipForward(int playerId)
    {
        // find hex with a ship
        var shipHex = FindHexByPlayer(playerId);

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
            Level = ship.Level,
            PlayerId = ship.PlayerId
        };

        // remove the ship from the current hex
        shipHex.Ship = null;
        StateHasChanged();
    }

    private void ResetShip(Command command) {
        // remove all ships from the hexes for the player
        foreach (var hex in _hexes.Where(h => h.Ship != null && h.Ship.PlayerId==command.PlayerId))
        {
            hex.Ship = null;
        }
        // this math is wrong but it works for now
        int ne = (_settingsVM.Columns*_settingsVM.Rows)-(_settingsVM.Rows - 1);
        int sw = (_settingsVM.Rows);
        switch (command.StartCorner)
        {
            case MapCorner.NW:
                _hexes.First().Ship = new Ship { 
                        Heading = 3, 
                        Style = command.ShipStyle, 
                        Level = 1, 
                        PlayerId = command.PlayerId };
                break;
            case MapCorner.NE:
                _hexes[ne].Ship = new Ship { 
                        Heading = 5, 
                        Style = command.ShipStyle, 
                        Level = 2, 
                        PlayerId = command.PlayerId };
                break;
            case MapCorner.SW:
                _hexes[sw].Ship = new Ship { 
                        Heading = 2, 
                        Style = command.ShipStyle, 
                        Level = 2, 
                        PlayerId = command.PlayerId };
                break;
            case MapCorner.SE:
                _hexes.Last().Ship = new Ship { 
                        Heading = 6, 
                        Style = command.ShipStyle, 
                        Level = 2, 
                        PlayerId = command.PlayerId };
                break;
        }
        StateHasChanged();
    }

    private void HexClicked(Hex hex)
    {
        foreach (var h in _hexes.Where(h => h.Id != hex.Id))
        {
            h.Selected = false;
        }
    }

    // Toggle highlighting hexes in range of players ship
    private void ToggleRangeFinder(int playerId, Ship.RangeShapes? rangeShape)
    {
        var shipHex = FindHexByPlayer(playerId);
        if (shipHex == null)
        {
            return;
        }
        var highlightHex = !shipHex.Ship.isRangeFinderShown;
        // Stretch goal: add cone then circle targetting
        var targetShape = rangeShape ?? shipHex.Ship.RangeShape; 
        if (targetShape == Ship.RangeShapes.Line)
        {
            var nextInLine = shipHex;
            var shipHeading = shipHex.Ship.Heading; 
            for (var i = 1; i < shipHex.Ship.AttackRange; i++)
            {
                var forwardCoord = nextInLine.DoubCoord.Forward(shipHeading);
                nextInLine = _hexes.FirstOrDefault(
                    h => h.DoubCoord.Row == forwardCoord.Row
                    && h.DoubCoord.Col == forwardCoord.Col);
                if (nextInLine == null)
                {
                    break;
                }
                nextInLine.Selected = highlightHex;
            }
        }
        shipHex.Ship.isRangeFinderShown = highlightHex;
        StateHasChanged();
    }
    // helper function return first hex containing Players ship
    private Hex FindHexByPlayer(int playerId)
    {
        return _hexes.FirstOrDefault(h => h.Ship != null
            && h.Ship.PlayerId == playerId);
    }
    public void Dispose()
    {
        ((INotifyPropertyChanged)SiteState.Properties).PropertyChanged -= HandlePropertyChanged;
    }


}

