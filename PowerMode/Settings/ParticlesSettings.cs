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


        public ParticlesColorType ParticlesColorType
        {
            get { return particlesColorType; }
            set { SetPropertyAndValidate(ref particlesColorType, value); }
        }

        public Color FixedColor
        {
            get { return fixedColor; }
            set
            {
                SetPropertyAndValidate(ref fixedColor, value);
                RaisePropertyChanged("FixedColorString");
            }
        }

        public string FixedColorString
        {
            get { return string.Join(",", FixedColor.R, FixedColor.G, FixedColor.B); }
        }

        [Range(1, 2000, ErrorMessage = "Maximum Particles Count should be between 1 to 100")]
        public int MaxParticlesCount
        {
            get { return maxParticlesCount; }
            set { SetPropertyAndValidate(ref maxParticlesCount, value); }
        }

        [Range(1, 50, ErrorMessage = "Minimum Spawned Particles should be between 1 to 100")]
        [LessThan(nameof(MaxSpawnedParticles))]
        public int MinSpawnedParticles
        {
            get { return minSpawnedParticles; }
            set { SetPropertyAndValidate(ref minSpawnedParticles, value); }
        }

        [Range(1, 50, ErrorMessage = "Maximum Spawned Particles should be between 1 to 100")]
        public int MaxSpawnedParticles
        {
            get { return maxSpawnedParticles; }
            set { SetPropertyAndValidate(ref maxSpawnedParticles, value); }
        }

        [Range(1, 50, ErrorMessage = "Minimum Particles Size should be between 1 to 100")]
        [LessThan(nameof(MaxParticlesSize))]
        public int MinParticlesSize
        {
            get { return minParticlesSize; }
            set { SetPropertyAndValidate(ref minParticlesSize, value); }
        }

        [Range(1, 50, ErrorMessage = "Maximum Particles Size should be between 1 to 100")]
        public int MaxParticlesSize
        {
            get { return maxParticlesSize; }
            set { SetPropertyAndValidate(ref maxParticlesSize, value); }
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
        }
    }
}
