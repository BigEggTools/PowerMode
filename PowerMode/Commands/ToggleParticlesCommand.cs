namespace BigEgg.Tools.PowerMode.Commands
{
    using System;
    using System.ComponentModel.Design;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Settings;

    internal sealed class ToggleParticlesCommand : CommandHandler
    {
        private ToggleParticlesCommand(Package package)
            : base(package, CommandData.CommandSet, CommandData.ToggleParticlesCommandId)
        {
        }


        public static ToggleParticlesCommand Instance { get; private set; }


        public static void Initialize(Package package)
        {
            Instance = new ToggleParticlesCommand(package);
        }


        protected override void OnQueryStatus(object sender, EventArgs e)
        {
            var settings = new GeneralSettings();
            SettingsService.GetFromStorages(ref settings, serviceProvider);
            menuCommand.Checked = settings.IsEnableParticles;
            menuCommand.Enabled = settings.IsEnablePowerMode;
        }

        protected override void OnExecute(object sender, EventArgs e)
        {
            var settings = new GeneralSettings();
            SettingsService.GetFromStorages(ref settings, serviceProvider);
            settings.IsEnableParticles = !settings.IsEnableParticles;
            SettingsService.SaveToStorage(settings, serviceProvider);

            var command = sender as MenuCommand;
            var menuCommandService = serviceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            var newCmdID = new CommandID(CommandData.CommandSet, command.CommandID.ID);
            var menuCommand = menuCommandService.FindCommand(newCmdID);
            menuCommand.Checked = settings.IsEnableParticles;
        }
    }
}
