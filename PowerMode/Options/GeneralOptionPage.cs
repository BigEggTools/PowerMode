namespace BigEgg.Tools.PowerMode.Options
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Services;
    using BigEgg.Tools.PowerMode.Settings;

    public class GeneralOptionPage : UIElementDialogPage
    {
        private GeneralSettings settings;


        ~GeneralOptionPage()
        {
            PropertyChangedEventManager.RemoveHandler(settings, SettingModelPropertyChanged, "");
        }


        public GeneralSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = new GeneralSettings();
                    settings.CloneFrom(SettingsService.GetGeneralSettings());
                    PropertyChangedEventManager.AddHandler(settings, SettingModelPropertyChanged, "");
                }

                return settings;
            }
        }

        public AchievementsSettings Achievements { get { return AchievementsService.GetAchievements(); } }


        public void ResetMaxComboStreak()
        {
            var achievements = AchievementsService.GetAchievements();
            achievements.MaxComboStreak = 0;
            AchievementsService.SaveToStorage(achievements);
        }


        protected override UIElement Child
        {
            get { return new GeneralOptionPageUserControl(this); }
        }

        protected override void SaveSetting(PropertyDescriptor property)
        {
            SettingsService.SaveToStorage(settings);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            var newSettings = SettingsService.GetGeneralSettings();
            settings.CloneFrom(newSettings);
        }


        private void SettingModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GeneralSettings.IsEnablePowerMode))
            {
                settings.IsEnableComboMode = settings.IsEnablePowerMode;
                settings.IsEnableParticles = settings.IsEnablePowerMode;
                settings.IsEnableScreenShake = settings.IsEnablePowerMode;
            }
        }
    }
}
