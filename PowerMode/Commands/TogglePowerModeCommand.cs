namespace BigEgg.Tools.PowerMode.Commands
{
    using System;
    using System.ComponentModel.Design;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Settings;

    internal sealed class TogglePowerModeCommand : CommandHandler
    {
        private TogglePowerModeCommand(Package package)
            : base(package, CommandData.CommandSet, CommandData.TogglePowerModeCommandId)
        {
        }


        public static TogglePowerModeCommand Instance { get; private set; }


        public static void Initialize(Package package)
        {
            Instance = new TogglePowerModeCommand(package);
        }


        protected override void OnQueryStatus(object sender, EventArgs e)
        {
            var settings = SettingsService.GetGeneralSettings(serviceProvider);
            menuCommand.Checked = settings.IsEnablePowerMode;
        }

        protected override void OnExecute(object sender, EventArgs e)
        {
            var settings = SettingsService.GetGeneralSettings(serviceProvider);
            settings.IsEnablePowerMode = !settings.IsEnablePowerMode;
            settings.IsEnableComboMode = settings.IsEnablePowerMode;
            settings.IsEnableParticles = settings.IsEnablePowerMode;
            settings.IsEnableScreenShake = settings.IsEnablePowerMode;
            SettingsService.SaveToStorage(settings, serviceProvider);

            menuCommand.Checked = settings.IsEnablePowerMode;

            var menuCommandService = serviceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            var newCmdID = new CommandID(CommandData.CommandSet, CommandData.ToggleComboModeCommandId);
            var otherMenuCommand = menuCommandService.FindCommand(newCmdID);
            otherMenuCommand.Enabled = settings.IsEnablePowerMode;

            menuCommandService = serviceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            newCmdID = new CommandID(CommandData.CommandSet, CommandData.ToggleParticlesCommandId);
            otherMenuCommand = menuCommandService.FindCommand(newCmdID);
            otherMenuCommand.Enabled = settings.IsEnablePowerMode;

            menuCommandService = serviceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            newCmdID = new CommandID(CommandData.CommandSet, CommandData.ToggleScreenShakeCommandId);
            otherMenuCommand = menuCommandService.FindCommand(newCmdID);
            otherMenuCommand.Enabled = settings.IsEnablePowerMode;

            menuCommandService = serviceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            newCmdID = new CommandID(CommandData.CommandSet, CommandData.ToggleAudioCommandId);
            otherMenuCommand = menuCommandService.FindCommand(newCmdID);
            otherMenuCommand.Enabled = settings.IsEnablePowerMode;
        }
    }
}
