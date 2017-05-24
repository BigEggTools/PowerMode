namespace BigEgg.Tools.PowerMode.Settings
{
    using System;
    using System.Drawing;
    using System.Linq;

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

        private static bool? GetBoolOption(SettingsStore store, string catelogName, string optionName)
        {
            if (store == null ||
                !store.CollectionExists(COLLECTION_PATH) ||
                !store.PropertyExists(COLLECTION_PATH, CombineCatelogAndOptionName(catelogName, optionName))) { return null; }

            return store.GetBoolean(COLLECTION_PATH, CombineCatelogAndOptionName(catelogName, optionName));
        }

        private static void SetOption(WritableSettingsStore store, string catelogName, string optionName, bool value)
        {
            if (!store.CollectionExists(COLLECTION_PATH)) { store.CreateCollection(COLLECTION_PATH); }
            store?.SetBoolean(COLLECTION_PATH, CombineCatelogAndOptionName(catelogName, optionName), value);
        }

        private static int? GetIntegerOption(SettingsStore store, string catelogName, string optionName)
        {
            if (store == null ||
                !store.CollectionExists(COLLECTION_PATH) ||
                !store.PropertyExists(COLLECTION_PATH, CombineCatelogAndOptionName(catelogName, optionName))) { return null; }

            return store.GetInt32(COLLECTION_PATH, CombineCatelogAndOptionName(catelogName, optionName));
        }

        private static void SetOption(WritableSettingsStore store, string catelogName, string optionName, int value)
        {
            if (!store.CollectionExists(COLLECTION_PATH)) { store.CreateCollection(COLLECTION_PATH); }
            store?.SetInt32(COLLECTION_PATH, CombineCatelogAndOptionName(catelogName, optionName), value);
        }

        private static string GetStringOption(SettingsStore store, string catelogName, string optionName)
        {
            if (store == null ||
                !store.CollectionExists(COLLECTION_PATH) ||
                !store.PropertyExists(COLLECTION_PATH, CombineCatelogAndOptionName(catelogName, optionName))) { return null; }

            return store.GetString(COLLECTION_PATH, CombineCatelogAndOptionName(catelogName, optionName));
        }

        private static void SetOption(WritableSettingsStore store, string catelogName, string optionName, string value)
        {
            if (!store.CollectionExists(COLLECTION_PATH)) { store.CreateCollection(COLLECTION_PATH); }
            store?.SetString(COLLECTION_PATH, CombineCatelogAndOptionName(catelogName, optionName), value);
        }

        private static Color? GetColorOption(SettingsStore store, string catelogName, string optionName)
        {
            if (store == null ||
                !store.CollectionExists(COLLECTION_PATH) ||
                !store.PropertyExists(COLLECTION_PATH, CombineCatelogAndOptionName(catelogName, optionName))) { return null; }

            var rgb = store.GetString(COLLECTION_PATH, CombineCatelogAndOptionName(catelogName, optionName)).Split(',').Select(x => Int32.Parse(x)).ToList();

            return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
        }

        private static void SetOption(WritableSettingsStore store, string catelogName, string optionName, Color value)
        {
            if (!store.CollectionExists(COLLECTION_PATH)) { store.CreateCollection(COLLECTION_PATH); }
            store?.SetString(COLLECTION_PATH, CombineCatelogAndOptionName(catelogName, optionName), string.Join(",", value.R, value.G, value.B));
        }


        private static string CombineCatelogAndOptionName(string catelogName, string optionName)
        {
            return $"{catelogName}_{optionName}";
        }
    }
}
