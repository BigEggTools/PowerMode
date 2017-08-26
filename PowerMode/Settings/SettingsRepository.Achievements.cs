namespace BigEgg.Tools.PowerMode.Settings
{
    using System;

    public static partial class SettingsRepository
    {
        private static readonly string ACHIEVEMENT_SETTINGS_CATELOG = "Achievement";

        public static AchievementsSettings GetAchievements(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }

            var store = GetSettingsStore(serviceProvider);
            var settings = new AchievementsSettings
            {
                MaxComboStreak = GetIntegerOption(store, ACHIEVEMENT_SETTINGS_CATELOG, nameof(AchievementsSettings.MaxComboStreak)).GetValueOrDefault(0)
            };

            return settings;
        }

        public static void SaveToStorage(AchievementsSettings achievements, IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }
            if (achievements == null) { throw new ArgumentNullException("achievements"); }

            var store = GetSettingsStore(serviceProvider);
            SetOption(store, ACHIEVEMENT_SETTINGS_CATELOG, nameof(AchievementsSettings.MaxComboStreak), achievements.MaxComboStreak);
        }
    }
}
