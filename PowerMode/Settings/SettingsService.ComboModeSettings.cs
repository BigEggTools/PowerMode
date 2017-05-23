namespace BigEgg.Tools.PowerMode.Settings
{
    using System;

    public static partial class SettingsService
    {
        private static ComboModeSettings comboModeSettingsCache = null;


        public static ComboModeSettings GetComboModeSettings(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }

            if (comboModeSettingsCache == null)
            {
                var store = GetSettingsStore(serviceProvider);
                comboModeSettingsCache = new ComboModeSettings();
                comboModeSettingsCache.ComboLevelStreakThreshold = GetIntegerOption(store, nameof(ComboModeSettings.ComboLevelStreakThreshold)).GetValueOrDefault(comboModeSettingsCache.ComboLevelStreakThreshold);
                comboModeSettingsCache.PowerColor = GetColorOption(store, nameof(ComboModeSettings.PowerColor)).GetValueOrDefault(comboModeSettingsCache.PowerColor);
                comboModeSettingsCache.IsShowStreakCounter = GetBoolOption(store, nameof(ComboModeSettings.IsShowStreakCounter)).GetValueOrDefault(comboModeSettingsCache.IsShowStreakCounter);
                comboModeSettingsCache.StreakCounterOpacity = GetIntegerOption(store, nameof(ComboModeSettings.StreakCounterOpacity)).GetValueOrDefault(comboModeSettingsCache.StreakCounterOpacity);
                comboModeSettingsCache.StreakCounterShakeStartLevel = GetIntegerOption(store, nameof(ComboModeSettings.StreakCounterShakeStartLevel)).GetValueOrDefault(comboModeSettingsCache.StreakCounterShakeStartLevel);
                comboModeSettingsCache.StreakTimeout = GetIntegerOption(store, nameof(ComboModeSettings.StreakTimeout)).GetValueOrDefault(comboModeSettingsCache.StreakTimeout);
                comboModeSettingsCache.ExclamationEveryStreak = GetIntegerOption(store, nameof(ComboModeSettings.ExclamationEveryStreak)).GetValueOrDefault(comboModeSettingsCache.ExclamationEveryStreak);
                var exclamations = GetStringOption(store, nameof(ComboModeSettings.Exclamations));
                comboModeSettingsCache.Exclamations = string.IsNullOrWhiteSpace(exclamations) ? comboModeSettingsCache.Exclamations : exclamations;
                comboModeSettingsCache.ParticlesStartLevel = GetIntegerOption(store, nameof(ComboModeSettings.ParticlesStartLevel)).GetValueOrDefault(comboModeSettingsCache.ParticlesStartLevel);
                comboModeSettingsCache.ScreenShakeStartLevel = GetIntegerOption(store, nameof(ComboModeSettings.ScreenShakeStartLevel)).GetValueOrDefault(comboModeSettingsCache.ScreenShakeStartLevel);
            }

            return comboModeSettingsCache;
        }

        public static void SaveToStorage(ComboModeSettings settings, IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }
            if (settings == null) { throw new ArgumentNullException("settings"); }

            comboModeSettingsCache = null;

            var store = GetSettingsStore(serviceProvider);
            SetOption(store, nameof(ComboModeSettings.ComboLevelStreakThreshold), settings.ComboLevelStreakThreshold);
            SetOption(store, nameof(ComboModeSettings.PowerColor), settings.PowerColorString);
            SetOption(store, nameof(ComboModeSettings.IsShowStreakCounter), settings.IsShowStreakCounter);
            SetOption(store, nameof(ComboModeSettings.StreakCounterOpacity), settings.StreakCounterOpacity);
            SetOption(store, nameof(ComboModeSettings.StreakCounterShakeStartLevel), settings.StreakCounterShakeStartLevel);
            SetOption(store, nameof(ComboModeSettings.StreakTimeout), settings.StreakTimeout);
            SetOption(store, nameof(ComboModeSettings.ExclamationEveryStreak), settings.ExclamationEveryStreak);
            SetOption(store, nameof(ComboModeSettings.Exclamations), settings.Exclamations);
            SetOption(store, nameof(ComboModeSettings.ParticlesStartLevel), settings.ParticlesStartLevel);
            SetOption(store, nameof(ComboModeSettings.ScreenShakeStartLevel), settings.ScreenShakeStartLevel);
        }
    }
}
