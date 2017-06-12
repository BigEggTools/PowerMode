namespace BigEgg.Tools.PowerMode.Services
{
    using System;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Settings;

    public class SettingsService
    {
        private static GeneralSettings generalSettingsCache = null;
        private static ComboModeSettings comboModeSettingsCache = null;
        private static ParticlesSettings particlesSettingsCache = null;
        private static ScreenShakeSettings screenShakeSettingsCache = null;


        public static GeneralSettings GetGeneralSettings()
        {
            if (generalSettingsCache == null)
            {
                generalSettingsCache = SettingsRepository.GetGeneralSettings(ServiceProvider.GlobalProvider);
            }

            return generalSettingsCache;
        }

        public static ComboModeSettings GetComboModeSettings()
        {
            if (comboModeSettingsCache == null)
            {
                comboModeSettingsCache = SettingsRepository.GetComboModeSettings(ServiceProvider.GlobalProvider);
            }

            return comboModeSettingsCache;
        }

        public static ParticlesSettings GetParticlesSettings()
        {
            if (particlesSettingsCache == null)
            {
                particlesSettingsCache = SettingsRepository.GetParticlesSettings(ServiceProvider.GlobalProvider);
            }

            return particlesSettingsCache;
        }

        public static ScreenShakeSettings GetScreenShakeSettings()
        {
            if (screenShakeSettingsCache == null)
            {
                screenShakeSettingsCache = SettingsRepository.GetScreenShakeSettings(ServiceProvider.GlobalProvider);
            }

            return screenShakeSettingsCache;
        }

        public static void SaveToStorage(GeneralSettings settings)
        {
            if (settings == null) { throw new ArgumentNullException("settings"); }

            SettingsRepository.SaveToStorage(settings, ServiceProvider.GlobalProvider);
            generalSettingsCache.CloneFrom(settings);
        }

        public static void SaveToStorage(ComboModeSettings settings)
        {
            if (settings == null) { throw new ArgumentNullException("settings"); }

            SettingsRepository.SaveToStorage(settings, ServiceProvider.GlobalProvider);

            comboModeSettingsCache.CloneFrom(settings);
        }

        public static void SaveToStorage(ParticlesSettings settings)
        {
            if (settings == null) { throw new ArgumentNullException("settings"); }

            SettingsRepository.SaveToStorage(settings, ServiceProvider.GlobalProvider);
            particlesSettingsCache.CloneFrom(settings);
        }

        public static void SaveToStorage(ScreenShakeSettings settings)
        {
            if (settings == null) { throw new ArgumentNullException("settings"); }

            SettingsRepository.SaveToStorage(settings, ServiceProvider.GlobalProvider);

            screenShakeSettingsCache.CloneFrom(settings);
        }
    }
}
