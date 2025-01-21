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
using System.ComponentModel;

namespace Hexstor.Module.HexGrid;

public partial class ShipCounter: ModuleControlBase
{

    [Parameter] 
    public Ship Ship { get; set; }
        
    private string GetHeadingClass() { 
        return $"heading{Ship.Heading}"; 
    }

    private string GetStyleLevelClass()
    {
        return $"Style{Ship.StyleCode}Level{Ship.Level}";
    }

    protected override async Task OnInitializedAsync()
    {

        ((INotifyPropertyChanged)SiteState.Properties).PropertyChanged += HandlePropertyChanged;

    }

    private void HandlePropertyChanged(object sender, PropertyChangedEventArgs args)
    {

        if (args.PropertyName == "Command")
        {
            if (SiteState.Properties.Command == "Left")
            {
                Ship.Heading = Ship.Heading - 1;
                if (Ship.Heading < 1)
                {
                    Ship.Heading = 6;
                }
                StateHasChanged();
            }
            if (SiteState.Properties.Command == "Right")
            {
                Ship.Heading = Ship.Heading + 1;
                if (Ship.Heading > 6)
                {
                    Ship.Heading = 1;
                }
                StateHasChanged();
            }

        }

    }

    public void Dispose()
    {
        ((INotifyPropertyChanged)SiteState.Properties).PropertyChanged -= HandlePropertyChanged;
    }

}

