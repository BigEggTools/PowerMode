namespace BigEgg.Tools.PowerMode.Commands
{
    using System;
    using System.ComponentModel.Design;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Services;

    internal sealed class TogglePowerModeCommand : CommandHandler
    {
        private TogglePowerModeCommand()
            : base(CommandData.CommandSet, CommandData.TogglePowerModeCommandId)
        {
        }


        public static TogglePowerModeCommand Instance { get; private set; }


        public static async System.Threading.Tasks.Task InitializeAsync(AsyncPackage package)
        {
            Instance = new TogglePowerModeCommand();
            await Instance.InitalizeAsync(package);
        }


        protected override void OnQueryStatus(object sender, EventArgs e)
        {
            var settings = SettingsService.GetGeneralSettings();
            menuCommand.Checked = settings.IsEnablePowerMode;
        }

        protected override void OnExecute(object sender, EventArgs e)
        {
            var settings = SettingsService.GetGeneralSettings();
            settings.IsEnablePowerMode = !settings.IsEnablePowerMode;
            settings.IsEnableComboMode = settings.IsEnablePowerMode;
            settings.IsEnableParticles = settings.IsEnablePowerMode;
            settings.IsEnableScreenShake = settings.IsEnablePowerMode;
            SettingsService.SaveToStorage(settings);

            menuCommand.Checked = settings.IsEnablePowerMode;

            var newCmdID = new CommandID(CommandData.CommandSet, CommandData.ToggleComboModeCommandId);
            var otherMenuCommand = commandService.FindCommand(newCmdID);
            otherMenuCommand.Enabled = settings.IsEnablePowerMode;

            newCmdID = new CommandID(CommandData.CommandSet, CommandData.ToggleParticlesCommandId);
            otherMenuCommand = commandService.FindCommand(newCmdID);
            otherMenuCommand.Enabled = settings.IsEnablePowerMode;

            newCmdID = new CommandID(CommandData.CommandSet, CommandData.ToggleScreenShakeCommandId);
            otherMenuCommand = commandService.FindCommand(newCmdID);
            otherMenuCommand.Enabled = settings.IsEnablePowerMode;

            newCmdID = new CommandID(CommandData.CommandSet, CommandData.ToggleAudioCommandId);
            otherMenuCommand = commandService.FindCommand(newCmdID);
            otherMenuCommand.Enabled = settings.IsEnablePowerMode;
        }
    }
}
