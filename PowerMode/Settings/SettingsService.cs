namespace BigEgg.Tools.PowerMode.Settings
{
    using System;

    using Microsoft.VisualStudio.Settings;
    using Microsoft.VisualStudio.Shell.Settings;

    public static partial class SettingsService
    {
        private static readonly string COLLECTION_PATH = "PowerMode";


        private static WritableSettingsStore GetSettingsStore(IServiceProvider serviceProvider)
        {
            var shellSettingsManager = new ShellSettingsManager(serviceProvider);
            return shellSettingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
        }

        private static bool? GetBoolOption(SettingsStore store, string optionName)
        {
            if (store == null || !store.CollectionExists(COLLECTION_PATH)) { return null; }

            return store.GetBoolean(COLLECTION_PATH, optionName, false);
        }

        private static void SetBoolOption(WritableSettingsStore store, string optionName, bool value)
        {
            if (!store.CollectionExists(COLLECTION_PATH))
            {
                store.CreateCollection(COLLECTION_PATH);
            }
            store?.SetBoolean(COLLECTION_PATH, optionName, value);
        }
    }
}
