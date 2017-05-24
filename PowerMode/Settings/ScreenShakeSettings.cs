namespace BigEgg.Tools.PowerMode.Settings
{
    using System.ComponentModel.DataAnnotations;

    public class ScreenShakeSettings : ValidatableModel
    {
        private int minIntensity = 1;
        private int maxIntensity = 3;

        [Range(1, 10, ErrorMessage = "Minimum Intensity of the shake should be between 1 to 10")]
        [LessThan(nameof(MaxIntensity))]
        public int MinIntensity
        {
            get { return minIntensity; }
            set { SetPropertyAndValidate(ref minIntensity, value); }
        }

        [Range(1, 10, ErrorMessage = "Maximum Intensity of the shake should be between 1 to 10")]
        public int MaxIntensity
        {
            get { return maxIntensity; }
            set { SetPropertyAndValidate(ref maxIntensity, value); }
        }


        public void CloneFrom(ScreenShakeSettings other)
        {
            this.MinIntensity = other.MinIntensity;
            this.MaxIntensity = other.MaxIntensity;
        }
    }
}
