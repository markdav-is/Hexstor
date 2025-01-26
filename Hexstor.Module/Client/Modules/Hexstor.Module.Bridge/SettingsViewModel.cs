using Hexstor.Module.Client.ViewModels;
using MudBlazor;
using Oqtane.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexstor.Module.Bridge
{
    internal class SettingsViewModel
    {
        public SettingsViewModel(ISettingService settingService, Dictionary<string, string> moduleSettings)
        {
            var shipStyleString = settingService.GetSetting(moduleSettings, nameof(ShipStyle), ShipStyle.Red.ToString());
            var shipLevelString = settingService.GetSetting(moduleSettings, nameof(ShipLevel), "1");
            var startCornerString = settingService.GetSetting(moduleSettings, nameof(StartCorner), MapCorner.NW.ToString());

            //convert from strings
            ShipStyle = Enum.TryParse(shipStyleString, out ShipStyle style) ? style : ShipStyle.Red;
            StartCorner = Enum.TryParse(startCornerString, out MapCorner corner) ? corner : MapCorner.NW;
            ShipLevel = Int32.TryParse(shipLevelString, out int level) ? level : 1;
        }
        public ShipStyle ShipStyle { get; set; }
        public int ShipLevel { get; set; }
        public MapCorner StartCorner { get; set; }


        public void SetSettings(ISettingService settingService, Dictionary<string, string> moduleSettings) {

            settingService.SetSetting(moduleSettings, nameof(ShipStyle), ShipStyle.ToString());
            settingService.SetSetting(moduleSettings, nameof(ShipLevel), ShipLevel.ToString());
            settingService.SetSetting(moduleSettings, nameof(StartCorner), StartCorner.ToString());

        }
    }
}
