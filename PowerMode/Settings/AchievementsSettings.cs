namespace BigEgg.Tools.PowerMode.Settings
{
    public class AchievementsSettings : Model
    {
        private int maxComboStreak;


        public int MaxComboStreak
        {
            get { return maxComboStreak; }
            set { SetProperty(ref maxComboStreak, value); }
        }


        public void CloneFrom(AchievementsSettings other)
        {
            this.MaxComboStreak = other.MaxComboStreak;
        }
    }
}
