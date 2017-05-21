namespace BigEgg.Tools.PowerMode.Settings
{
    using System;

    using Microsoft.VisualStudio.Settings;
    using Microsoft.VisualStudio.Shell.Settings;

    public static class SettingsService
    {
        private static readonly string COLLECTION_PATH = "PowerMode";


        public static void GetFromStorages(ref GeneralSettings settings, IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }

            var shellSettingsManager = new ShellSettingsManager(serviceProvider);
            var store = shellSettingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

            var result = new GeneralSettings();
            result.IsEnablePowerMode = GetBoolOption(store, nameof(GeneralSettings.IsEnablePowerMode)).GetValueOrDefault(result.IsEnablePowerMode);
            result.IsEnableParticles = GetBoolOption(store, nameof(GeneralSettings.IsEnableParticles)).GetValueOrDefault(result.IsEnableParticles);
            result.IsEnableScreenShake = GetBoolOption(store, nameof(GeneralSettings.IsEnableScreenShake)).GetValueOrDefault(result.IsEnableScreenShake);
            result.IsEnableComboMode = GetBoolOption(store, nameof(GeneralSettings.IsEnableComboMode)).GetValueOrDefault(result.IsEnableComboMode);
            result.IsEnableAudio = GetBoolOption(store, nameof(GeneralSettings.IsEnableAudio)).GetValueOrDefault(result.IsEnableAudio);

            if (settings == null) { settings = new GeneralSettings(); }
            settings.CloneFrom(result);
        }

        public static void SaveToStorage(GeneralSettings settings, IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }
            if (settings == null) { throw new ArgumentNullException("settings"); }

            var shellSettingsManager = new ShellSettingsManager(serviceProvider);
            var store = shellSettingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

            SetBoolOption(store, nameof(GeneralSettings.IsEnablePowerMode), settings.IsEnablePowerMode);
            SetBoolOption(store, nameof(GeneralSettings.IsEnableParticles), settings.IsEnableParticles);
            SetBoolOption(store, nameof(GeneralSettings.IsEnableScreenShake), settings.IsEnableScreenShake);
            SetBoolOption(store, nameof(GeneralSettings.IsEnableComboMode), settings.IsEnableComboMode);
            SetBoolOption(store, nameof(GeneralSettings.IsEnableAudio), settings.IsEnableAudio);
        }



        private static bool? GetBoolOption(SettingsStore store, string optionName)
        {
            if (store == null || !store.CollectionExists(COLLECTION_PATH)) { return null; }

            return store.GetBoolean(COLLECTION_PATH, optionName, false);
        }

        private static void SetBoolOption(WritableSettingsStore store, string optionName, bool value)
        {
            if (!store.CollectionExists(COLLECTION_PATH))
            {
                store.CreateCollection(COLLECTION_PATH);
            }
            store?.SetBoolean(COLLECTION_PATH, optionName, value);
        }
    }
}
