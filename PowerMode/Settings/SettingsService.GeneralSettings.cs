namespace BigEgg.Tools.PowerMode.Settings
{
    using System;

    public static partial class SettingsService
    {
        private static GeneralSettings generalSettingsCache = null;
        private static readonly string GENERAL_SETTINGS_CATELOG = "General";


        public static GeneralSettings GetGeneralSettings(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }

            if (generalSettingsCache == null)
            {
                var store = GetSettingsStore(serviceProvider);
                generalSettingsCache = new GeneralSettings();
                generalSettingsCache.IsEnablePowerMode = GetBoolOption(store, GENERAL_SETTINGS_CATELOG, nameof(GeneralSettings.IsEnablePowerMode)).GetValueOrDefault(generalSettingsCache.IsEnablePowerMode);
                generalSettingsCache.IsEnableParticles = GetBoolOption(store, GENERAL_SETTINGS_CATELOG, nameof(GeneralSettings.IsEnableParticles)).GetValueOrDefault(generalSettingsCache.IsEnableParticles);
                generalSettingsCache.IsEnableScreenShake = GetBoolOption(store, GENERAL_SETTINGS_CATELOG, nameof(GeneralSettings.IsEnableScreenShake)).GetValueOrDefault(generalSettingsCache.IsEnableScreenShake);
                generalSettingsCache.IsEnableComboMode = GetBoolOption(store, GENERAL_SETTINGS_CATELOG, nameof(GeneralSettings.IsEnableComboMode)).GetValueOrDefault(generalSettingsCache.IsEnableComboMode);
                generalSettingsCache.IsEnableAudio = GetBoolOption(store, GENERAL_SETTINGS_CATELOG, nameof(GeneralSettings.IsEnableAudio)).GetValueOrDefault(generalSettingsCache.IsEnableAudio);
            }

            return generalSettingsCache;
        }

        public static void SaveToStorage(GeneralSettings settings, IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }
            if (settings == null) { throw new ArgumentNullException("settings"); }

            generalSettingsCache = null;

            var store = GetSettingsStore(serviceProvider);
            SetOption(store, GENERAL_SETTINGS_CATELOG, nameof(GeneralSettings.IsEnablePowerMode), settings.IsEnablePowerMode);
            SetOption(store, GENERAL_SETTINGS_CATELOG, nameof(GeneralSettings.IsEnableParticles), settings.IsEnableParticles);
            SetOption(store, GENERAL_SETTINGS_CATELOG, nameof(GeneralSettings.IsEnableScreenShake), settings.IsEnableScreenShake);
            SetOption(store, GENERAL_SETTINGS_CATELOG, nameof(GeneralSettings.IsEnableComboMode), settings.IsEnableComboMode);
            SetOption(store, GENERAL_SETTINGS_CATELOG, nameof(GeneralSettings.IsEnableAudio), settings.IsEnableAudio);
        }
    }
}
