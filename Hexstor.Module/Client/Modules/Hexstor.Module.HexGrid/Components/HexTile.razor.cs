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
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;

namespace Hexstor.Module.HexGrid;

public partial class HexTile : ModuleControlBase
{
	    
    private bool IsLoaded;

    [Parameter]
    public Hex Hex { get; set; }

    [Parameter]
    public EventCallback<Hex> OnClicked { get; set; }

    private async Task Clicked(MouseEventArgs _)
    {
        await logger.LogInformation("Clicked Coords {DoubCoord}", Hex.DoubCoord.ToString());
        await OnClicked.InvokeAsync(Hex);
    }

}

