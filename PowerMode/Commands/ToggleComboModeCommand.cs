namespace BigEgg.Tools.PowerMode.Commands
{
    using System;
    using System.ComponentModel.Design;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Services;

    internal sealed class ToggleComboModeCommand : CommandHandler
    {
        private ToggleComboModeCommand()
            : base(CommandData.CommandSet, CommandData.ToggleComboModeCommandId)
        {
        }


        public static ToggleComboModeCommand Instance { get; private set; }


        public static async System.Threading.Tasks.Task InitializeAsync(AsyncPackage package)
        {
            Instance = new ToggleComboModeCommand();
            await Instance.InitalizeAsync(package);
        }


        protected override void OnQueryStatus(object sender, EventArgs e)
        {
            var settings = SettingsService.GetGeneralSettings();
            menuCommand.Checked = settings.IsEnableComboMode;
            menuCommand.Enabled = settings.IsEnablePowerMode;
        }

        protected override void OnExecute(object sender, EventArgs e)
        {
            var settings = SettingsService.GetGeneralSettings();
            settings.IsEnableComboMode = !settings.IsEnableComboMode;
            SettingsService.SaveToStorage(settings);

            var command = sender as MenuCommand;
            var newCmdID = new CommandID(CommandData.CommandSet, command.CommandID.ID);
            var menuCommand = commandService.FindCommand(newCmdID);
            menuCommand.Checked = settings.IsEnableComboMode;
        }
    }
}
