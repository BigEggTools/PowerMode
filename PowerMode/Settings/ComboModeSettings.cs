namespace BigEgg.Tools.PowerMode.Settings
{
    using System.Drawing;

    public class ComboModeSettings : Model
    {
        private int levelStreakThreshold = 30;
        private Color powerColor = Color.FromArgb(78, 255, 161);
        private bool isShowStreakCounter = true;
        private int streakCounterOpacity = 153; //  60%
        private int streakCounterShakeStartLevel = 1;
        private int streakTimeout = 3;
        private int exclamationEveryStreak = 10;
        private string exclamations = string.Join("; ", new string[] { "Super!", "Radical!", "Fantastic!", "Great!", "OMG", "Whoah!", ":O", "Nice!", "Splendid!", "Wild!", "Grand!", "Impressive!", "Stupendous!", "Extreme!", "Awesome!" });
        private int particlesStartLevel = 0;
        private int screenShakeStartLevel = 2;


        public int LevelStreakThreshold
        {
            get { return levelStreakThreshold; }
            set { SetProperty(ref levelStreakThreshold, value); }
        }

        public Color PowerColor
        {
            get { return powerColor; }
            set { SetProperty(ref powerColor, value); }
        }

        public bool IsShowStreakCounter
        {
            get { return isShowStreakCounter; }
            set { SetProperty(ref isShowStreakCounter, value); }
        }

        public int StreakCounterOpacity
        {
            get { return streakCounterOpacity; }
            set { SetProperty(ref streakCounterOpacity, value); }
        }

        public int StreakCounterShakeStartLevel
        {
            get { return streakCounterShakeStartLevel; }
            set { SetProperty(ref streakCounterShakeStartLevel, value); }
        }

        public int StreakTimeout
        {
            get { return streakTimeout; }
            set { SetProperty(ref streakTimeout, value); }
        }

        public int ExclamationEveryStreak
        {
            get { return exclamationEveryStreak; }
            set { SetProperty(ref exclamationEveryStreak, value); }
        }

        public string Exclamations
        {
            get { return exclamations; }
            set { SetProperty(ref exclamations, value); }
        }

        public int ParticlesStartLevel
        {
            get { return particlesStartLevel; }
            set { SetProperty(ref particlesStartLevel, value); }
        }

        public int ScreenShakeStartLevel
        {
            get { return screenShakeStartLevel; }
            set { SetProperty(ref screenShakeStartLevel, value); }
        }


        public void CloneFrom(ComboModeSettings other)
        {
            this.LevelStreakThreshold = other.LevelStreakThreshold;
            this.PowerColor = other.PowerColor;
            this.IsShowStreakCounter = other.IsShowStreakCounter;
            this.StreakCounterOpacity = other.StreakCounterOpacity;
            this.StreakCounterShakeStartLevel = other.StreakCounterShakeStartLevel;
            this.StreakTimeout = other.StreakTimeout;
            this.ExclamationEveryStreak = other.ExclamationEveryStreak;
            this.Exclamations = other.Exclamations;
            this.ParticlesStartLevel = other.ParticlesStartLevel;
            this.ScreenShakeStartLevel = other.ScreenShakeStartLevel;
        }
    }
}
