namespace BigEgg.Tools.PowerMode.Options
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Settings;

    public class ComboModeOptionPage : UIElementDialogPage
    {
        private ComboModeSettings settings;


        ~ComboModeOptionPage()
        {
            PropertyChangedEventManager.RemoveHandler(settings, SettingModelPropertyChanged, "");
        }


        public ComboModeSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = SettingsService.GetComboModeSettings(Site);
                    PropertyChangedEventManager.AddHandler(settings, SettingModelPropertyChanged, "");
                }

                return settings;
            }
        }


        protected override UIElement Child
        {
            get { return new ComboModeOptionPageUserControl(this); }
        }

        protected override void SaveSetting(PropertyDescriptor property)
        {
            if (settings.HasErrors) { return; }

            SettingsService.SaveToStorage(settings, Site);
        }

        protected override void OnApply(PageApplyEventArgs e)
        {
            if (settings.HasErrors)
            {
                e.ApplyBehavior = ApplyKind.CancelNoNavigate;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            var newSettings = SettingsService.GetComboModeSettings(Site);
            settings.CloneFrom(newSettings);
        }


        private void SettingModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }
    }
}
