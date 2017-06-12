namespace BigEgg.Tools.PowerMode.Settings
{
    using System;

    public static partial class SettingsRepository
    {
        private static readonly string PARTICLES_SETTINGS_CATELOG = "Particles";


        public static ParticlesSettings GetParticlesSettings(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }

            ParticlesSettings particlesSettingsCache = null;
            var store = GetSettingsStore(serviceProvider);
            particlesSettingsCache = new ParticlesSettings();
            var particlesColorType = GetStringOption(store, PARTICLES_SETTINGS_CATELOG, nameof(ParticlesSettings.ParticlesColorType));
            particlesSettingsCache.ParticlesColorType = string.IsNullOrWhiteSpace(particlesColorType) ? particlesSettingsCache.ParticlesColorType : (ParticlesColorType)Enum.Parse(typeof(ParticlesColorType), particlesColorType);
            particlesSettingsCache.FixedColor = GetColorOption(store, PARTICLES_SETTINGS_CATELOG, nameof(ParticlesSettings.FixedColor)).GetValueOrDefault(particlesSettingsCache.FixedColor);
            particlesSettingsCache.MaxParticlesCount = GetIntegerOption(store, PARTICLES_SETTINGS_CATELOG, nameof(ParticlesSettings.MaxParticlesCount)).GetValueOrDefault(particlesSettingsCache.MaxParticlesCount);
            particlesSettingsCache.MinSpawnedParticles = GetIntegerOption(store, PARTICLES_SETTINGS_CATELOG, nameof(ParticlesSettings.MinSpawnedParticles)).GetValueOrDefault(particlesSettingsCache.MinSpawnedParticles);
            particlesSettingsCache.MaxSpawnedParticles = GetIntegerOption(store, PARTICLES_SETTINGS_CATELOG, nameof(ParticlesSettings.MaxSpawnedParticles)).GetValueOrDefault(particlesSettingsCache.MaxSpawnedParticles);
            particlesSettingsCache.MinParticlesSize = GetIntegerOption(store, PARTICLES_SETTINGS_CATELOG, nameof(ParticlesSettings.MinParticlesSize)).GetValueOrDefault(particlesSettingsCache.MinParticlesSize);
            particlesSettingsCache.MaxParticlesSize = GetIntegerOption(store, PARTICLES_SETTINGS_CATELOG, nameof(ParticlesSettings.MaxParticlesSize)).GetValueOrDefault(particlesSettingsCache.MaxParticlesSize);

            return particlesSettingsCache;
        }

        public static void SaveToStorage(ParticlesSettings settings, IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) { throw new ArgumentNullException("serviceProvider"); }
            if (settings == null) { throw new ArgumentNullException("settings"); }

            var store = GetSettingsStore(serviceProvider);
            SetOption(store, PARTICLES_SETTINGS_CATELOG, nameof(ParticlesSettings.ParticlesColorType), settings.ParticlesColorType.ToString());
            SetOption(store, PARTICLES_SETTINGS_CATELOG, nameof(ParticlesSettings.FixedColor), settings.FixedColor);
            SetOption(store, PARTICLES_SETTINGS_CATELOG, nameof(ParticlesSettings.MaxParticlesCount), settings.MaxParticlesCount);
            SetOption(store, PARTICLES_SETTINGS_CATELOG, nameof(ParticlesSettings.MinSpawnedParticles), settings.MinSpawnedParticles);
            SetOption(store, PARTICLES_SETTINGS_CATELOG, nameof(ParticlesSettings.MaxSpawnedParticles), settings.MaxSpawnedParticles);
            SetOption(store, PARTICLES_SETTINGS_CATELOG, nameof(ParticlesSettings.MinParticlesSize), settings.MinParticlesSize);
            SetOption(store, PARTICLES_SETTINGS_CATELOG, nameof(ParticlesSettings.MaxParticlesSize), settings.MaxParticlesSize);
        }
    }
}
