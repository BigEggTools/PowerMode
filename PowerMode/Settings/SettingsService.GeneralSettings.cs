namespace BigEgg.Tools.PowerMode.Settings
{
    using System;

    public static partial class SettingsService
    {
        public static void GetFromStorages(ref GeneralSettings settings, IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }

            var store = GetSettingsStore(serviceProvider);
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

            var store = GetSettingsStore(serviceProvider);
            SetBoolOption(store, nameof(GeneralSettings.IsEnablePowerMode), settings.IsEnablePowerMode);
            SetBoolOption(store, nameof(GeneralSettings.IsEnableParticles), settings.IsEnableParticles);
            SetBoolOption(store, nameof(GeneralSettings.IsEnableScreenShake), settings.IsEnableScreenShake);
            SetBoolOption(store, nameof(GeneralSettings.IsEnableComboMode), settings.IsEnableComboMode);
            SetBoolOption(store, nameof(GeneralSettings.IsEnableAudio), settings.IsEnableAudio);
        }
    }
}
