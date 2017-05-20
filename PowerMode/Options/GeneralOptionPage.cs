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
                    settings = settings.GetFromStorages(Site);
                    PropertyChangedEventManager.AddHandler(settings, SettingModelPropertyChanged, "");
                }

                return settings;
            }
        }


        protected override UIElement Child
        {
            get { return new GeneralOptionPageUserControl(this); }
        }


        private void SettingModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GeneralSettings.IsEnablePowerMode))
            {
                settings.IsEnableComboMode = settings.IsEnablePowerMode;
                settings.IsEnableParticles = settings.IsEnablePowerMode;
                settings.IsEnableScreenShake = settings.IsEnablePowerMode;
            }

            settings.SaveToStorage(Site);
        }
    }
}
