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

}

