namespace BigEgg.Tools.PowerMode.Settings
{
    using System;

    public static partial class SettingsService
    {
        private static ScreenShakeSettings screenShakeSettingsCache = null;
        private static readonly string SCREEN_SHAKE_SETTINGS_CATELOG = "General";


        public static ScreenShakeSettings GetScreenShakeSettings(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }

            if (screenShakeSettingsCache == null)
            {
                var store = GetSettingsStore(serviceProvider);
                screenShakeSettingsCache = new ScreenShakeSettings();
                screenShakeSettingsCache.MinIntensity = GetIntegerOption(store, SCREEN_SHAKE_SETTINGS_CATELOG, nameof(ScreenShakeSettings.MinIntensity)).GetValueOrDefault(screenShakeSettingsCache.MinIntensity);
                screenShakeSettingsCache.MaxIntensity = GetIntegerOption(store, SCREEN_SHAKE_SETTINGS_CATELOG, nameof(ScreenShakeSettings.MaxIntensity)).GetValueOrDefault(screenShakeSettingsCache.MaxIntensity);
            }

            return screenShakeSettingsCache;
        }

        public static void SaveToStorage(ScreenShakeSettings settings, IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }
            if (settings == null) { throw new ArgumentNullException("settings"); }

            screenShakeSettingsCache = null;

            var store = GetSettingsStore(serviceProvider);
            SetOption(store, SCREEN_SHAKE_SETTINGS_CATELOG, nameof(ScreenShakeSettings.MinIntensity), settings.MinIntensity);
            SetOption(store, SCREEN_SHAKE_SETTINGS_CATELOG, nameof(ScreenShakeSettings.MaxIntensity), settings.MaxIntensity);
        }
    }
}
