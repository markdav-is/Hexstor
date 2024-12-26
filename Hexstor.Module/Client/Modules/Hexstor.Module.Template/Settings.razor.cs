using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Models;
using Oqtane.Shared;


namespace Hexstor.Module.Template
{ 
    public partial class Settings: ModuleBase
    {
        [Inject] public ISettingService SettingService { get; set; }
        [Inject] public IStringLocalizer<Settings> Localizer{ get; set; }
		
		private string resourceType = "Hexstor.Module.Template.Settings, Hexstor.Module.Template.Client.Oqtane"; // for localization
        public override string Title => "Template Settings";
        private SettingsViewModel _settingsVM;
        private bool _loading = true;
        private string _value;
        public override List<Resource> Resources => new List<Resource>()
            {
                new Resource { ResourceType = ResourceType.Stylesheet,  Url = "https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap" },
                new Resource { ResourceType = ResourceType.Stylesheet,  Url = "_content/MudBlazor/MudBlazor.min.css" },
                new Resource { ResourceType = ResourceType.Stylesheet,  Url = ModulePath() + "Module.css" },
                new Resource { ResourceType = ResourceType.Script,     Url = "_content/MudBlazor/MudBlazor.min.js", Location = ResourceLocation.Body, Level = ResourceLevel.Site },
                new Resource { ResourceType = ResourceType.Script,      Url = ModulePath() + "Module.js" },
            };
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var moduleSettings = await SettingService.GetModuleSettingsAsync(ModuleState.ModuleId);
                _settingsVM = new SettingsViewModel(SettingService, moduleSettings);
                _loading = false;
            }
            catch (Exception ex)
            {
                AddModuleMessage(ex.Message, MessageType.Error);
            }
        }

        public async Task UpdateSettings()
        {
            try
            {
                Dictionary<string, string> settings = await SettingService.GetModuleSettingsAsync(ModuleState.ModuleId);
                _settingsVM.SetSettings(SettingService, settings);
                await SettingService.UpdateModuleSettingsAsync(settings, ModuleState.ModuleId);
            }
            catch (Exception ex)
            {
                AddModuleMessage(ex.Message, MessageType.Error);
            }
        }
    }
}
