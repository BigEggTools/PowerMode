namespace BigEgg.Tools.PowerMode.Options
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    [Guid("00000000-0000-0000-0000-000000000000")]
    public class GeneralOptionPageSettings : OptionPageSettingsBase
    {
        private bool isEnablePowerMode = true;
        private bool isEnableComboMode = true;
        private bool isEnableParticles = true;
        private bool isEnableScreenShake = true;
        private bool isEnableAudio = false;


        public bool IsEnablePowerMode
        {
            get { return isEnablePowerMode; }
            set
            {
                SetProperty(ref isEnablePowerMode, value);
                SetAllProperties(value);
            }
        }

        public bool IsEnableComboMode
        {
            get { return isEnableComboMode; }
            set { SetProperty(ref isEnableComboMode, value); }
        }

        public bool IsEnableParticles
        {
            get { return isEnableParticles; }
            set { SetProperty(ref isEnableParticles, value); }
        }

        public bool IsEnableScreenShake
        {
            get { return isEnableScreenShake; }
            set { SetProperty(ref isEnableScreenShake, value); }
        }

        public bool IsEnableAudio
        {
            get { return isEnableAudio; }
            set { SetProperty(ref isEnableAudio, value); }
        }


        protected override UIElement Child
        {
            get { return new GeneralOptionPageUserControl(this); }
        }


        private void SetAllProperties(bool value)
        {
            IsEnableComboMode = value;
            IsEnableParticles = value;
            IsEnableScreenShake = value;
        }
    }
}
