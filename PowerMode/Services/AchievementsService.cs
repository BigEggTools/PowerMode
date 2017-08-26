namespace BigEgg.Tools.PowerMode.Services
{
    using System;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Settings;

    public class AchievementsService
    {
        private static AchievementsSettings achievementsSettingsCache = null;


        public static AchievementsSettings GetAchievements()
        {
            if (achievementsSettingsCache == null)
            {
                achievementsSettingsCache = SettingsRepository.GetAchievements(ServiceProvider.GlobalProvider);
            }

            return achievementsSettingsCache;
        }

        public static void SaveToStorage(AchievementsSettings settings)
        {
            if (settings == null) { throw new ArgumentNullException("settings"); }

            SettingsRepository.SaveToStorage(settings, ServiceProvider.GlobalProvider);
            achievementsSettingsCache.CloneFrom(settings);
        }
    }
}
