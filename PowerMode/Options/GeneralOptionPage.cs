namespace BigEgg.Tools.PowerMode.Options
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Settings;

    [Guid("00000000-0000-0000-0000-000000000000")]
    public class GeneralOptionPage : UIElementDialogPage
    {
        private GeneralSettings settings;


        public GeneralOptionPage()
        {
        }


        public GeneralSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    SettingsService.GetFromStorages(ref settings, Site);
                    PropertyChangedEventManager.AddHandler(settings, SettingModelPropertyChanged, "");
                }

                return settings;
            }
        }


        protected override UIElement Child
        {
            get { return new GeneralOptionPageUserControl(this); }
        }

        protected override void SaveSetting(PropertyDescriptor property)
        {
            SettingsService.SaveToStorage(settings, Site);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            SettingsService.GetFromStorages(ref settings, Site);
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
