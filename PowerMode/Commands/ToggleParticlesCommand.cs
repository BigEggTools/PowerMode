namespace BigEgg.Tools.PowerMode.Commands
{
    using System;
    using System.ComponentModel.Design;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Services;

    internal sealed class ToggleParticlesCommand : CommandHandler
    {
        private ToggleParticlesCommand()
            : base( CommandData.CommandSet, CommandData.ToggleParticlesCommandId)
        {
        }


        public static ToggleParticlesCommand Instance { get; private set; }


        public static async System.Threading.Tasks.Task InitializeAsync(AsyncPackage package)
        {
            Instance = new ToggleParticlesCommand();
            await Instance.InitalizeAsync(package);
        }


        protected override void OnQueryStatus(object sender, EventArgs e)
        {
            var settings = SettingsService.GetGeneralSettings();
            menuCommand.Checked = settings.IsEnableParticles;
            menuCommand.Enabled = settings.IsEnablePowerMode;
        }

        protected override void OnExecute(object sender, EventArgs e)
        {
            var settings = SettingsService.GetGeneralSettings();
            settings.IsEnableParticles = !settings.IsEnableParticles;
            SettingsService.SaveToStorage(settings);

            var command = sender as MenuCommand;
            var newCmdID = new CommandID(CommandData.CommandSet, command.CommandID.ID);
            var menuCommand = commandService.FindCommand(newCmdID);
            menuCommand.Checked = settings.IsEnableParticles;
        }
    }
}
