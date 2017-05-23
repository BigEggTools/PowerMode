namespace BigEgg.Tools.PowerMode.Commands
{
    using System;
    using System.ComponentModel.Design;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Settings;

    internal sealed class ToggleComboModeCommand : CommandHandler
    {
        private ToggleComboModeCommand(Package package)
            : base(package, CommandData.CommandSet, CommandData.ToggleComboModeCommandId)
        {
        }


        public static ToggleComboModeCommand Instance { get; private set; }


        public static void Initialize(Package package)
        {
            Instance = new ToggleComboModeCommand(package);
        }


        protected override void OnQueryStatus(object sender, EventArgs e)
        {
            var settings = SettingsService.GetGeneralSettings(serviceProvider);
            menuCommand.Checked = settings.IsEnableComboMode;
            menuCommand.Enabled = settings.IsEnablePowerMode;
        }

        protected override void OnExecute(object sender, EventArgs e)
        {
            var settings = SettingsService.GetGeneralSettings(serviceProvider);
            settings.IsEnableComboMode = !settings.IsEnableComboMode;
            SettingsService.SaveToStorage(settings, serviceProvider);

            var command = sender as MenuCommand;
            var menuCommandService = serviceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            var newCmdID = new CommandID(CommandData.CommandSet, command.CommandID.ID);
            var menuCommand = menuCommandService.FindCommand(newCmdID);
            menuCommand.Checked = settings.IsEnableComboMode;
        }
    }
}
