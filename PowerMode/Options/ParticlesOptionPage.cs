namespace BigEgg.Tools.PowerMode.Options
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Services;
    using BigEgg.Tools.PowerMode.Settings;

    public class ParticlesOptionPage : UIElementDialogPage
    {
        private ParticlesSettings settings;


        ~ParticlesOptionPage()
        {
            PropertyChangedEventManager.RemoveHandler(settings, SettingModelPropertyChanged, "");
        }


        public ParticlesSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = new ParticlesSettings();
                    settings.CloneFrom(SettingsService.GetParticlesSettings());
                    PropertyChangedEventManager.AddHandler(settings, SettingModelPropertyChanged, "");
                }

                return settings;
            }
        }


        protected override UIElement Child
        {
            get { return new ParticlesOptionPageUserControl(this); }
        }

        protected override void OnApply(PageApplyEventArgs e)
        {
            if (settings.HasErrors)
            {
                e.ApplyBehavior = ApplyKind.CancelNoNavigate;
                return;
            }

            SettingsService.SaveToStorage(settings);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            var newSettings = SettingsService.GetParticlesSettings();
            settings.CloneFrom(newSettings);
        }


        private void SettingModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }
    }
}
