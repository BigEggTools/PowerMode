namespace BigEgg.Tools.PowerMode.Commands
{
    using System;
    using System.ComponentModel.Design;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Settings;

    internal sealed class ToggleAudioCommand : CommandHandler
    {
        private ToggleAudioCommand(Package package)
            : base(package, CommandData.CommandSet, CommandData.ToggleAudioCommandId)
        {
        }


        public static ToggleAudioCommand Instance { get; private set; }


        public static void Initialize(Package package)
        {
            Instance = new ToggleAudioCommand(package);
        }


        protected override void OnQueryStatus(object sender, EventArgs e)
        {
            var settings = new GeneralSettings();
            SettingsService.GetFromStorages(ref settings, serviceProvider);
            menuCommand.Checked = settings.IsEnableAudio;
            menuCommand.Enabled = false;
        }

        protected override void OnExecute(object sender, EventArgs e)
        {
            var settings = new GeneralSettings();
            SettingsService.GetFromStorages(ref settings, serviceProvider);
            settings.IsEnableAudio = !settings.IsEnableAudio;
            SettingsService.SaveToStorage(settings, serviceProvider);

            var command = sender as MenuCommand;
            var menuCommandService = serviceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            var newCmdID = new CommandID(CommandData.CommandSet, command.CommandID.ID);
            var menuCommand = menuCommandService.FindCommand(newCmdID);
            menuCommand.Checked = settings.IsEnableAudio;
        }
    }
}
