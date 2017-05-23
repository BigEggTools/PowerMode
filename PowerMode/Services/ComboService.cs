namespace BigEgg.Tools.PowerMode.Services
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Settings;
    using BigEgg.Tools.PowerMode.Utils;

    public class ComboService
    {
        private static IDictionary<int, Color> levelColor;
        private static readonly int MAX_LEVEL = 16;
        private static readonly int LEVEL_COLOR_SPIN_ANGLE = 30;
        private static readonly int DEFAULT_FONT_SIZE = 20;
        private static readonly int LEVEL_FONT_SIZE = 1;
        private static readonly float LEVEL_PEN_WIDTH = 0.5f;
        private static readonly int DEFAULT_EXCLAMATION_FONT_SIZE = 8;
        private static readonly float LEVEL_EXCLAMATION_FONT_SIZE = 0.25f;


        public static int GetPowerLevel(int streakCount)
        {
            int comboLevelStreakThreshold = GetSettings().ComboLevelStreakThreshold;
            return Math.Min(streakCount / comboLevelStreakThreshold, MAX_LEVEL);
        }

        public static bool AnimationOnStreakCouunter(int streakCount)
        {
            int streakCounterShakeStartLevel = GetSettings().StreakCounterShakeStartLevel;
            return GetPowerLevel(streakCount) > streakCounterShakeStartLevel;
        }

        public static Color GetPowerLevelColor(int streakCount)
        {
            var powerColor = GetSettings().PowerColor;

            if (levelColor == null) { levelColor = new Dictionary<int, Color>(); }
            if (levelColor.Count == 0 || levelColor[0] != powerColor)
            {
                levelColor.Clear();
                for (int i = 0; i <= MAX_LEVEL; i++)
                {
                    levelColor.Add(i, powerColor.SpinColor(LEVEL_COLOR_SPIN_ANGLE * i));
                }
            }

            var level = GetPowerLevel(streakCount);
            return levelColor[level];
        }

        public static int GetPowerLevelPenWidth(int streakCount)
        {
            return (int)(GetPowerLevel(streakCount) * LEVEL_PEN_WIDTH);
        }

        public static int GetPowerLevelFontSize(int streakCount)
        {
            var level = GetPowerLevel(streakCount);
            return DEFAULT_FONT_SIZE + level * LEVEL_FONT_SIZE;
        }

        public static int GetPowerLevelExclamationFontSize(int streakCount)
        {
            var level = GetPowerLevel(streakCount);
            return DEFAULT_EXCLAMATION_FONT_SIZE + (int)(level * LEVEL_EXCLAMATION_FONT_SIZE);
        }

        public static bool ShowExclamation(int streakCount)
        {
            int exclamationEveryStreak = GetSettings().ExclamationEveryStreak;
            return streakCount > 0 && streakCount % exclamationEveryStreak == 0;
        }

        public static string GetExclamation()
        {
            var exclamations = GetSettings().Exclamations;
            return RandomUtils.NextString(exclamations);
        }


        private static ComboModeSettings GetSettings()
        {
            return SettingsService.GetComboModeSettings(ServiceProvider.GlobalProvider);
        }
    }
}
