namespace BigEgg.Tools.PowerMode.Commands
{
    using System;
    using System.ComponentModel.Design;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Settings;

    internal sealed class ToggleScreenShakeCommand : CommandHandler
    {
        private ToggleScreenShakeCommand(Package package)
            : base(package, CommandData.CommandSet, CommandData.ToggleScreenShakeCommandId)
        {
        }


        public static ToggleScreenShakeCommand Instance { get; private set; }


        public static void Initialize(Package package)
        {
            Instance = new ToggleScreenShakeCommand(package);
        }


        protected override void OnQueryStatus(object sender, EventArgs e)
        {
            var settings = SettingsService.GetGeneralSettings(serviceProvider);
            menuCommand.Checked = settings.IsEnableScreenShake;
            menuCommand.Enabled = settings.IsEnablePowerMode;
        }

        protected override void OnExecute(object sender, EventArgs e)
        {
            var settings = SettingsService.GetGeneralSettings(serviceProvider);
            settings.IsEnableScreenShake = !settings.IsEnableScreenShake;
            SettingsService.SaveToStorage(settings, serviceProvider);

            var command = sender as MenuCommand;
            var menuCommandService = serviceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            var newCmdID = new CommandID(CommandData.CommandSet, command.CommandID.ID);
            var menuCommand = menuCommandService.FindCommand(newCmdID);
            menuCommand.Checked = settings.IsEnableScreenShake;
        }
    }
}
