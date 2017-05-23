namespace BigEgg.Tools.PowerMode.Settings
{
    using System;

    public static partial class SettingsService
    {
        private static GeneralSettings generalSettingsCache = null;


        public static void GetFromStorages(ref GeneralSettings settings, IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }

            if (generalSettingsCache == null)
            {
                var store = GetSettingsStore(serviceProvider);
                var generalSettingsCache = new GeneralSettings();
                generalSettingsCache.IsEnablePowerMode = GetBoolOption(store, nameof(GeneralSettings.IsEnablePowerMode)).GetValueOrDefault(generalSettingsCache.IsEnablePowerMode);
                generalSettingsCache.IsEnableParticles = GetBoolOption(store, nameof(GeneralSettings.IsEnableParticles)).GetValueOrDefault(generalSettingsCache.IsEnableParticles);
                generalSettingsCache.IsEnableScreenShake = GetBoolOption(store, nameof(GeneralSettings.IsEnableScreenShake)).GetValueOrDefault(generalSettingsCache.IsEnableScreenShake);
                generalSettingsCache.IsEnableComboMode = GetBoolOption(store, nameof(GeneralSettings.IsEnableComboMode)).GetValueOrDefault(generalSettingsCache.IsEnableComboMode);
                generalSettingsCache.IsEnableAudio = GetBoolOption(store, nameof(GeneralSettings.IsEnableAudio)).GetValueOrDefault(generalSettingsCache.IsEnableAudio);
            }

            if (settings == null) { settings = new GeneralSettings(); }
            settings.CloneFrom(generalSettingsCache);
        }

        public static void SaveToStorage(GeneralSettings settings, IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }
            if (settings == null) { throw new ArgumentNullException("settings"); }

            generalSettingsCache = null;

            var store = GetSettingsStore(serviceProvider);
            SetOption(store, nameof(GeneralSettings.IsEnablePowerMode), settings.IsEnablePowerMode);
            SetOption(store, nameof(GeneralSettings.IsEnableParticles), settings.IsEnableParticles);
            SetOption(store, nameof(GeneralSettings.IsEnableScreenShake), settings.IsEnableScreenShake);
            SetOption(store, nameof(GeneralSettings.IsEnableComboMode), settings.IsEnableComboMode);
            SetOption(store, nameof(GeneralSettings.IsEnableAudio), settings.IsEnableAudio);
        }
    }
}
