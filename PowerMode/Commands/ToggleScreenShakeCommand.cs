namespace BigEgg.Tools.PowerMode.Commands
{
    using System;
    using System.ComponentModel.Design;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Services;

    internal sealed class ToggleScreenShakeCommand : CommandHandler
    {
        private ToggleScreenShakeCommand()
            : base(CommandData.CommandSet, CommandData.ToggleScreenShakeCommandId)
        {
        }


        public static ToggleScreenShakeCommand Instance { get; private set; }


        public static async System.Threading.Tasks.Task InitializeAsync(AsyncPackage package)
        {
            Instance = new ToggleScreenShakeCommand();
            await Instance.InitalizeAsync(package);
        }


        protected override void OnQueryStatus(object sender, EventArgs e)
        {
            var settings = SettingsService.GetGeneralSettings();
            menuCommand.Checked = settings.IsEnableScreenShake;
            menuCommand.Enabled = settings.IsEnablePowerMode;
        }

        protected override void OnExecute(object sender, EventArgs e)
        {
            var settings = SettingsService.GetGeneralSettings();
            settings.IsEnableScreenShake = !settings.IsEnableScreenShake;
            SettingsService.SaveToStorage(settings);

            var command = sender as MenuCommand;
            var newCmdID = new CommandID(CommandData.CommandSet, command.CommandID.ID);
            var menuCommand = commandService.FindCommand(newCmdID);
            menuCommand.Checked = settings.IsEnableScreenShake;
        }
    }
}
