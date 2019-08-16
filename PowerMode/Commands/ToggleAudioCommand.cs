namespace BigEgg.Tools.PowerMode.Commands
{
    using System;
    using System.ComponentModel.Design;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Services;

    internal sealed class ToggleAudioCommand : CommandHandler
    {
        private ToggleAudioCommand()
            : base(CommandData.CommandSet, CommandData.ToggleAudioCommandId)
        {
        }


        public static ToggleAudioCommand Instance { get; private set; }


        public static async System.Threading.Tasks.Task InitializeAsync(AsyncPackage package)
        {
            Instance = new ToggleAudioCommand();
            await Instance.InitalizeAsync(package);
        }


        protected override void OnQueryStatus(object sender, EventArgs e)
        {
            var settings = SettingsService.GetGeneralSettings();
            menuCommand.Checked = settings.IsEnableAudio;
            menuCommand.Enabled = false;
        }

        protected override void OnExecute(object sender, EventArgs e)
        {
            var settings = SettingsService.GetGeneralSettings();
            settings.IsEnableAudio = !settings.IsEnableAudio;
            SettingsService.SaveToStorage(settings);

            var command = sender as MenuCommand;
            var newCmdID = new CommandID(CommandData.CommandSet, command.CommandID.ID);
            var menuCommand = commandService.FindCommand(newCmdID);
            menuCommand.Checked = settings.IsEnableAudio;
        }
    }
}
