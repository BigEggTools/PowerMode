namespace BigEgg.Tools.PowerMode.Options.ViewModels
{
    using BigEgg.Tools.PowerMode.Settings;

    public class GeneralOptionViewModel : Model
    {
        public GeneralSettings Settings { get; set; }

        public AchievementsSettings Achievements { get; set; }
    }
}
