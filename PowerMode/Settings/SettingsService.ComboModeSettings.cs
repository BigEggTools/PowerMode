namespace BigEgg.Tools.PowerMode.Settings
{
    using System;

    public static partial class SettingsService
    {
        private static ComboModeSettings comboModeSettingsCache = null;
        private static readonly string COMBO_MODE_SETTINGS_CATELOG = "ComboMode";


        public static ComboModeSettings GetComboModeSettings(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }

            if (comboModeSettingsCache == null)
            {
                var store = GetSettingsStore(serviceProvider);
                comboModeSettingsCache = new ComboModeSettings();
                comboModeSettingsCache.ComboLevelStreakThreshold = GetIntegerOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.ComboLevelStreakThreshold)).GetValueOrDefault(comboModeSettingsCache.ComboLevelStreakThreshold);
                comboModeSettingsCache.PowerColor = GetColorOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.PowerColor)).GetValueOrDefault(comboModeSettingsCache.PowerColor);
                comboModeSettingsCache.IsShowStreakCounter = GetBoolOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.IsShowStreakCounter)).GetValueOrDefault(comboModeSettingsCache.IsShowStreakCounter);
                comboModeSettingsCache.StreakCounterOpacity = GetIntegerOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.StreakCounterOpacity)).GetValueOrDefault(comboModeSettingsCache.StreakCounterOpacity);
                comboModeSettingsCache.StreakCounterShakeStartLevel = GetIntegerOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.StreakCounterShakeStartLevel)).GetValueOrDefault(comboModeSettingsCache.StreakCounterShakeStartLevel);
                comboModeSettingsCache.StreakTimeout = GetIntegerOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.StreakTimeout)).GetValueOrDefault(comboModeSettingsCache.StreakTimeout);
                comboModeSettingsCache.ExclamationEveryStreak = GetIntegerOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.ExclamationEveryStreak)).GetValueOrDefault(comboModeSettingsCache.ExclamationEveryStreak);
                var exclamations = GetStringOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.ExclamationsString));
                comboModeSettingsCache.ExclamationsString = string.IsNullOrWhiteSpace(exclamations) ? comboModeSettingsCache.ExclamationsString : exclamations;
                comboModeSettingsCache.ParticlesStartLevel = GetIntegerOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.ParticlesStartLevel)).GetValueOrDefault(comboModeSettingsCache.ParticlesStartLevel);
                comboModeSettingsCache.ScreenShakeStartLevel = GetIntegerOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.ScreenShakeStartLevel)).GetValueOrDefault(comboModeSettingsCache.ScreenShakeStartLevel);
            }

            return comboModeSettingsCache;
        }

        public static void SaveToStorage(ComboModeSettings settings, IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }
            if (settings == null) { throw new ArgumentNullException("settings"); }

            comboModeSettingsCache = null;

            var store = GetSettingsStore(serviceProvider);
            SetOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.ComboLevelStreakThreshold), settings.ComboLevelStreakThreshold);
            SetOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.PowerColor), settings.PowerColorString);
            SetOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.IsShowStreakCounter), settings.IsShowStreakCounter);
            SetOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.StreakCounterOpacity), settings.StreakCounterOpacity);
            SetOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.StreakCounterShakeStartLevel), settings.StreakCounterShakeStartLevel);
            SetOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.StreakTimeout), settings.StreakTimeout);
            SetOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.ExclamationEveryStreak), settings.ExclamationEveryStreak);
            SetOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.ExclamationsString), settings.ExclamationsString);
            SetOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.ParticlesStartLevel), settings.ParticlesStartLevel);
            SetOption(store, COMBO_MODE_SETTINGS_CATELOG, nameof(ComboModeSettings.ScreenShakeStartLevel), settings.ScreenShakeStartLevel);
        }
    }
}
