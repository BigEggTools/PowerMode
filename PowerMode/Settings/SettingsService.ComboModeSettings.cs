namespace BigEgg.Tools.PowerMode.Settings
{
    using System;

    public static partial class SettingsService
    {
        public static void GetFromStorages(ref ComboModeSettings settings, IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }

            var store = GetSettingsStore(serviceProvider);
            var result = new ComboModeSettings();
            result.LevelStreakThreshold = GetIntegerOption(store, nameof(ComboModeSettings.LevelStreakThreshold)).GetValueOrDefault(result.LevelStreakThreshold);
            result.PowerColor = GetColorOption(store, nameof(ComboModeSettings.PowerColor)).GetValueOrDefault(result.PowerColor);
            result.IsShowStreakCounter = GetBoolOption(store, nameof(ComboModeSettings.IsShowStreakCounter)).GetValueOrDefault(result.IsShowStreakCounter);
            result.StreakCounterOpacity = GetIntegerOption(store, nameof(ComboModeSettings.StreakCounterOpacity)).GetValueOrDefault(result.StreakCounterOpacity);
            result.StreakCounterShakeStartLevel = GetIntegerOption(store, nameof(ComboModeSettings.StreakCounterShakeStartLevel)).GetValueOrDefault(result.StreakCounterShakeStartLevel);
            result.StreakTimeout = GetIntegerOption(store, nameof(ComboModeSettings.StreakTimeout)).GetValueOrDefault(result.StreakTimeout);
            result.ExclamationEveryStreak = GetIntegerOption(store, nameof(ComboModeSettings.ExclamationEveryStreak)).GetValueOrDefault(result.ExclamationEveryStreak);
            var exclamations = GetStringOption(store, nameof(ComboModeSettings.Exclamations));
            result.Exclamations = string.IsNullOrWhiteSpace(exclamations) ? result.Exclamations : exclamations;
            result.ParticlesStartLevel = GetIntegerOption(store, nameof(ComboModeSettings.ParticlesStartLevel)).GetValueOrDefault(result.ParticlesStartLevel);
            result.ScreenShakeStartLevel = GetIntegerOption(store, nameof(ComboModeSettings.ScreenShakeStartLevel)).GetValueOrDefault(result.ScreenShakeStartLevel);

            if (settings == null) { settings = new ComboModeSettings(); }
            settings.CloneFrom(result);
        }

        public static void SaveToStorage(ComboModeSettings settings, IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }
            if (settings == null) { throw new ArgumentNullException("settings"); }

            var store = GetSettingsStore(serviceProvider);
            SetOption(store, nameof(ComboModeSettings.LevelStreakThreshold), settings.LevelStreakThreshold);
            SetOption(store, nameof(ComboModeSettings.PowerColor), settings.PowerColor);
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
