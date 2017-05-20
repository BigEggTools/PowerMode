namespace BigEgg.Tools.PowerMode.Options
{
    public class GeneralOptionPageSettings : Model
    {
        private bool isEnablePowerMode = true;
        private bool isEnableComboMode = true;
        private bool isEnableParticles = true;
        private bool isEnableScreenShake = true;
        private bool isEnableAudio = false;


        public bool IsEnablePowerMode
        {
            get { return isEnablePowerMode; }
            set { SetProperty(ref isEnablePowerMode, value); }
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
    }
}
