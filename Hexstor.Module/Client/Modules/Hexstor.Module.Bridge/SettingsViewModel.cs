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

            var attackRange = settingService.GetSetting(moduleSettings, nameof(AttackRange), "3".ToString());
            var rangeShape = settingService.GetSetting(moduleSettings, nameof(RangeShape), RangeShapes.Line.ToString());

            //convert from strings
            ShipStyle = Enum.TryParse(shipStyleString, out ShipStyle style) ? style : ShipStyle.Red;
            StartCorner = Enum.TryParse(startCornerString, out MapCorner corner) ? corner : MapCorner.NW;
            ShipLevel = Int32.TryParse(shipLevelString, out int level) ? level : 1;
            AttackRange = Int32.TryParse(attackRange, out int range) ? range : 3;
            RangeShape = Enum.TryParse(rangeShape, out RangeShapes shape) ? shape : RangeShapes.Line;
        }
        public ShipStyle ShipStyle { get; set; }
        public int ShipLevel { get; set; }
        public MapCorner StartCorner { get; set; }

        // Targetting, Range settings
        public int AttackRange { get; set; } = 3;
        // bit of a stretch, this would be better defined in a Weapon class instead of part of the ship
        public enum RangeShapes
        {
            Line,
            Cone,
            Circle
        }
        public RangeShapes RangeShape { get; set; } = RangeShapes.Line;

        public void SetSettings(ISettingService settingService, Dictionary<string, string> moduleSettings) {

            settingService.SetSetting(moduleSettings, nameof(ShipStyle), ShipStyle.ToString());
            settingService.SetSetting(moduleSettings, nameof(ShipLevel), ShipLevel.ToString());
            settingService.SetSetting(moduleSettings, nameof(StartCorner), StartCorner.ToString());
            settingService.SetSetting(moduleSettings, nameof(AttackRange), AttackRange.ToString());
            settingService.SetSetting(moduleSettings, nameof(RangeShape), RangeShape.ToString());

        }
    }
}
