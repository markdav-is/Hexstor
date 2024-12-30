using MudBlazor;
using Oqtane.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexstor.Module.HexGrid
{
    internal class SettingsViewModel
    {
        public SettingsViewModel(ISettingService settingService, Dictionary<string, string> moduleSettings)
        {
            //convert setting string to int
            if (int.TryParse(settingService.GetSetting(moduleSettings, nameof(Rows), Rows.ToString()), out int rows))
            {
                Rows = rows;
            }
            // cols
            if (int.TryParse(settingService.GetSetting(moduleSettings, nameof(Columns), Columns.ToString()), out int cols))
            {
                Columns = cols;
            }
        }

        public int Rows { get; set; } = 6;
        public int Columns { get; set; } = 6;


        public void SetSettings(ISettingService settingService, Dictionary<string, string> moduleSettings) {


            settingService.SetSetting(moduleSettings, nameof(Rows), Rows.ToString());
            settingService.SetSetting(moduleSettings, nameof(Columns), Columns.ToString());


        }
    }
}
