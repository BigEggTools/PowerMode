namespace BigEgg.Tools.PowerMode.Settings
{
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;

    public class ComboModeSettings : Model
    {
        private int comboLevelStreakThreshold = 30;
        private Color powerColor = Color.FromArgb(78, 255, 161);
        private bool isShowStreakCounter = true;
        private int streakCounterOpacity = 153; //  60%
        private int streakCounterShakeStartLevel = 1;
        private int streakTimeout = 5;
        private int exclamationEveryStreak = 10;
        private string exclamations = string.Join("; ", new string[] { "Super!", "Radical!", "Fantastic!", "Great!", "OMG", "Whoah!", ":O", "Nice!", "Splendid!", "Wild!", "Grand!", "Impressive!", "Stupendous!", "Extreme!", "Awesome!" });
        private int particlesStartLevel = 0;
        private int screenShakeStartLevel = 2;


        [Range(10, 100, ErrorMessage = "Streak Threshold to determine Combo Level should be between 10 to 100")]
        public int ComboLevelStreakThreshold
        {
            get { return comboLevelStreakThreshold; }
            set { SetProperty(ref comboLevelStreakThreshold, value); }
        }

        public Color PowerColor
        {
            get { return powerColor; }
            set
            {
                SetProperty(ref powerColor, value);
                RaisePropertyChanged("PowerColorString");
            }
        }

        public string PowerColorString
        {
            get { return string.Join(",", PowerColor.R, PowerColor.G, PowerColor.B); }
        }

        public bool IsShowStreakCounter
        {
            get { return isShowStreakCounter; }
            set { SetProperty(ref isShowStreakCounter, value); }
        }

        [Range(1, 255, ErrorMessage = "Streak Counter Opacity should be between 1 to 100")]
        public int StreakCounterOpacity
        {
            get { return streakCounterOpacity; }
            set { SetProperty(ref streakCounterOpacity, value); }
        }

        [Range(0, 16, ErrorMessage = "Streak Counter Shake Start Level should be between 1 to 16")]
        public int StreakCounterShakeStartLevel
        {
            get { return streakCounterShakeStartLevel; }
            set { SetProperty(ref streakCounterShakeStartLevel, value); }
        }

        [Range(1, 60, ErrorMessage = "Streak Timeout(Seconds) should be between 1 to 60")]
        public int StreakTimeout
        {
            get { return streakTimeout; }
            set { SetProperty(ref streakTimeout, value); }
        }

        [Range(10, 100, ErrorMessage = "Exclamation Every Streak should be between 10 to 100")]
        public int ExclamationEveryStreak
        {
            get { return exclamationEveryStreak; }
            set { SetProperty(ref exclamationEveryStreak, value); }
        }

        [Required(ErrorMessage = "Exclamations is mandatory")]
        public string Exclamations
        {
            get { return exclamations; }
            set { SetProperty(ref exclamations, value); }
        }

        [Range(0, 16, ErrorMessage = "Particles Start Level should be between 1 to 16")]
        public int ParticlesStartLevel
        {
            get { return particlesStartLevel; }
            set { SetProperty(ref particlesStartLevel, value); }
        }

        [Range(0, 16, ErrorMessage = "Screen Shake Start Level should be between 1 to 16")]
        public int ScreenShakeStartLevel
        {
            get { return screenShakeStartLevel; }
            set { SetProperty(ref screenShakeStartLevel, value); }
        }


        public void CloneFrom(ComboModeSettings other)
        {
            this.ComboLevelStreakThreshold = other.ComboLevelStreakThreshold;
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
