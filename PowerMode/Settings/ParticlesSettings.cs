namespace BigEgg.Tools.PowerMode.Settings
{
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;

    public class ParticlesSettings : ValidatableModel
    {
        private ParticlesColorType particlesColorType = ParticlesColorType.Random;
        private Color fixedColor = Color.FromArgb(78, 255, 161);
        private int maxParticlesCount = 500;
        private int minSpawnedParticles = 5;
        private int maxSpawnedParticles = 15;
        private int minParticlesSize = 2;
        private int maxParticlesSize = 4;
        private bool isEnabledPartyMode = true;
        private int partyModeThreshold = 50;
        private int partyModeSpawnedParticles = 200;


        public ParticlesColorType ParticlesColorType
        {
            get { return particlesColorType; }
            set { SetPropertyAndValidate(ref particlesColorType, value); }
        }

        public Color FixedColor
        {
            get { return fixedColor; }
            set { SetPropertyAndValidate(ref fixedColor, value); }
        }

        [Range(1, 2000, ErrorMessage = "Maximum Particles Count should be between 1 to 2000")]
        public int MaxParticlesCount
        {
            get { return maxParticlesCount; }
            set { SetPropertyAndValidate(ref maxParticlesCount, value); }
        }

        [Range(1, 50, ErrorMessage = "Minimum Spawned Particles should be between 1 to 50")]
        [LessThan(nameof(MaxSpawnedParticles))]
        public int MinSpawnedParticles
        {
            get { return minSpawnedParticles; }
            set { SetPropertyAndValidate(ref minSpawnedParticles, value); }
        }

        [Range(1, 50, ErrorMessage = "Maximum Spawned Particles should be between 1 to 50")]
        public int MaxSpawnedParticles
        {
            get { return maxSpawnedParticles; }
            set { SetPropertyAndValidate(ref maxSpawnedParticles, value); }
        }

        [Range(1, 50, ErrorMessage = "Minimum Particles Size should be between 1 to 50")]
        [LessThan(nameof(MaxParticlesSize))]
        public int MinParticlesSize
        {
            get { return minParticlesSize; }
            set { SetPropertyAndValidate(ref minParticlesSize, value); }
        }

        [Range(1, 50, ErrorMessage = "Maximum Particles Size should be between 1 to 50")]
        public int MaxParticlesSize
        {
            get { return maxParticlesSize; }
            set { SetPropertyAndValidate(ref maxParticlesSize, value); }
        }

        public bool IsEnabledPartyMode
        {
            get { return isEnabledPartyMode; }
            set { SetPropertyAndValidate(ref isEnabledPartyMode, value); }
        }

        [Range(10, 200, ErrorMessage = "Party Mode Threshold to determine should show party particles or not.")]
        public int PartyModeThreshold
        {
            get { return partyModeThreshold; }
            set { SetPropertyAndValidate(ref partyModeThreshold, value); }
        }

        [Range(1, 2000, ErrorMessage = "Spawned Particles during party should be between 1 to 50")]
        public int PartyModeSpawnedParticles
        {
            get { return partyModeSpawnedParticles; }
            set { SetPropertyAndValidate(ref partyModeSpawnedParticles, value); }
        }


        public void CloneFrom(ParticlesSettings other)
        {
            this.ParticlesColorType = other.ParticlesColorType;
            this.FixedColor = other.FixedColor;
            this.MaxParticlesCount = other.MaxParticlesCount;
            this.MinSpawnedParticles = other.MinSpawnedParticles;
            this.MaxSpawnedParticles = other.MaxSpawnedParticles;
            this.MinParticlesSize = other.MinParticlesSize;
            this.MaxParticlesSize = other.MaxParticlesSize;
            this.IsEnabledPartyMode = other.IsEnabledPartyMode;
            this.PartyModeThreshold = other.PartyModeThreshold;
            this.PartyModeSpawnedParticles = other.PartyModeSpawnedParticles;
        }
    }
}
