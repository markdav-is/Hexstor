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
        // Default settings
        public int Rows { get; set; } = 6;
        public int Columns { get; set; } = 6;
        public bool ShowSpritesheetButton { get; set; } = true;

        public SettingsViewModel(ISettingService settingService, Dictionary<string, string> moduleSettings)
        {
            // Convert setting string to int for rows
            if (int.TryParse(settingService.GetSetting(moduleSettings, nameof(Rows), Rows.ToString()), out int rows))
            {
                Rows = rows;
            }

            // Convert setting string to int for columns
            if (int.TryParse(settingService.GetSetting(moduleSettings, nameof(Columns), Columns.ToString()), out int cols))
            {
                Columns = cols;
            }

            // New setting for spritesheet button visibility
            if (bool.TryParse(settingService.GetSetting(moduleSettings, nameof(ShowSpritesheetButton), ShowSpritesheetButton.ToString()), out bool showButton))
            {
                ShowSpritesheetButton = showButton;
            }
        }

        public void SetSettings(ISettingService settingService, Dictionary<string, string> moduleSettings)
        {
            // Save settings back to moduleSettings dictionary
            settingService.SetSetting(moduleSettings, nameof(Rows), Rows.ToString());
            settingService.SetSetting(moduleSettings, nameof(Columns), Columns.ToString());
            settingService.SetSetting(moduleSettings, nameof(ShowSpritesheetButton), ShowSpritesheetButton.ToString());
        }
    }
}
