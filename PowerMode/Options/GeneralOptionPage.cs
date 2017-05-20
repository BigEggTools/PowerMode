namespace BigEgg.Tools.PowerMode.Options
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows;

    using Microsoft.VisualStudio.Shell;

    [Guid("00000000-0000-0000-0000-000000000000")]
    public class GeneralOptionPage : UIElementDialogPage
    {
        private readonly GeneralOptionPageSettings settings;


        public GeneralOptionPage()
        {
            settings = new GeneralOptionPageSettings();

            PropertyChangedEventManager.AddHandler(settings, SettingModelPropertyChanged, "");
        }


        public GeneralOptionPageSettings Settings { get { return this.settings; } }


        protected override UIElement Child
        {
            get { return new GeneralOptionPageUserControl(this); }
        }


        private void SettingModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GeneralOptionPageSettings.IsEnablePowerMode))
            {
                settings.IsEnableComboMode = settings.IsEnablePowerMode;
                settings.IsEnableParticles = settings.IsEnablePowerMode;
                settings.IsEnableScreenShake = settings.IsEnablePowerMode;
            }
        }
    }
}
