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

namespace Hexstor.Module.HexGrid;

public partial class HexTile : ModuleControlBase
{
	    
    private bool IsLoaded;

    [Parameter]
    public Hex Hex { get; set; }


}

