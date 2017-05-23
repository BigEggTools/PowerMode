namespace BigEgg.Tools.PowerMode.Settings
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;
    using System.Linq;

    public class ComboModeSettings : ValidatableModel
    {
        private int comboLevelStreakThreshold = 30;
        private Color powerColor = Color.FromArgb(78, 255, 161);
        private bool isShowStreakCounter = true;
        private int streakCounterOpacity = 153; //  60%
        private int streakCounterShakeStartLevel = 1;
        private int streakTimeout = 5;
        private int exclamationEveryStreak = 10;
        private string exclamationsString = string.Join("; ", new string[] { "Super!", "Radical!", "Fantastic!", "Great!", "OMG", "Whoah!", ":O", "Nice!", "Splendid!", "Wild!", "Grand!", "Impressive!", "Stupendous!", "Extreme!", "Awesome!" });
        private IList<string> exclamations = new List<string>();
        private int particlesStartLevel = 0;
        private int screenShakeStartLevel = 2;


        [Range(10, 100, ErrorMessage = "Streak Threshold to determine Combo Level should be between 10 to 100")]
        public int ComboLevelStreakThreshold
        {
            get { return comboLevelStreakThreshold; }
            set { SetPropertyAndValidate(ref comboLevelStreakThreshold, value); }
        }

        public Color PowerColor
        {
            get { return powerColor; }
            set
            {
                SetPropertyAndValidate(ref powerColor, value);
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
            set { SetPropertyAndValidate(ref isShowStreakCounter, value); }
        }

        [Range(1, 255, ErrorMessage = "Streak Counter Opacity should be between 1 to 100")]
        public int StreakCounterOpacity
        {
            get { return streakCounterOpacity; }
            set { SetPropertyAndValidate(ref streakCounterOpacity, value); }
        }

        [Range(0, 16, ErrorMessage = "Streak Counter Shake Start Level should be between 1 to 16")]
        public int StreakCounterShakeStartLevel
        {
            get { return streakCounterShakeStartLevel; }
            set { SetPropertyAndValidate(ref streakCounterShakeStartLevel, value); }
        }

        [Range(1, 60, ErrorMessage = "Streak Timeout(Seconds) should be between 1 to 60")]
        public int StreakTimeout
        {
            get { return streakTimeout; }
            set { SetPropertyAndValidate(ref streakTimeout, value); }
        }

        [Range(10, 100, ErrorMessage = "Exclamation Every Streak should be between 10 to 100")]
        public int ExclamationEveryStreak
        {
            get { return exclamationEveryStreak; }
            set { SetPropertyAndValidate(ref exclamationEveryStreak, value); }
        }

        [Required(ErrorMessage = "Exclamations is mandatory")]
        public string ExclamationsString
        {
            get { return exclamationsString; }
            set
            {
                SetPropertyAndValidate(ref exclamationsString, value);
                exclamations = exclamationsString.Split(';').Select(x => x.Trim()).ToList();
            }
        }

        public IList<string> Exclamations
        {
            get { return exclamations; }
        }

        [Range(0, 16, ErrorMessage = "Particles Start Level should be between 1 to 16")]
        public int ParticlesStartLevel
        {
            get { return particlesStartLevel; }
            set { SetPropertyAndValidate(ref particlesStartLevel, value); }
        }

        [Range(0, 16, ErrorMessage = "Screen Shake Start Level should be between 1 to 16")]
        public int ScreenShakeStartLevel
        {
            get { return screenShakeStartLevel; }
            set { SetPropertyAndValidate(ref screenShakeStartLevel, value); }
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
            this.ExclamationsString = other.ExclamationsString;
            this.ParticlesStartLevel = other.ParticlesStartLevel;
            this.ScreenShakeStartLevel = other.ScreenShakeStartLevel;
        }
    }
}
