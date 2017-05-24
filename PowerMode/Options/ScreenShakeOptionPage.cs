namespace BigEgg.Tools.PowerMode.Options
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Settings;

    public class ScreenShakeOptionPage : UIElementDialogPage
    {
        private ScreenShakeSettings settings;


        ~ScreenShakeOptionPage()
        {
            PropertyChangedEventManager.RemoveHandler(settings, SettingModelPropertyChanged, "");
        }


        public ScreenShakeSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = SettingsService.GetScreenShakeSettings(Site);
                    PropertyChangedEventManager.AddHandler(settings, SettingModelPropertyChanged, "");
                }

                return settings;
            }
        }


        protected override UIElement Child
        {
            get { return new ScreenShakeOptionPageUserControl(this); }
        }

        protected override void OnApply(PageApplyEventArgs e)
        {
            if (settings.HasErrors)
            {
                e.ApplyBehavior = ApplyKind.CancelNoNavigate;
                return;
            }

            SettingsService.SaveToStorage(settings, Site);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            var newSettings = SettingsService.GetScreenShakeSettings(Site);
            settings.CloneFrom(newSettings);
        }


        private void SettingModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }
    }
}
