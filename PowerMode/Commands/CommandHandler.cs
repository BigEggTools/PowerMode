namespace BigEgg.Tools.PowerMode.Commands
{
    using System;
    using System.ComponentModel.Design;

    using Microsoft.VisualStudio.Shell;

    internal abstract class CommandHandler : IDisposable
    {
        protected readonly IServiceProvider serviceProvider;
        protected readonly OleMenuCommandService commandService;
        protected readonly OleMenuCommand menuCommand;


        protected CommandHandler(Package package, Guid group, int id)
        {
            if (package == null) { throw new ArgumentNullException("package"); }

            serviceProvider = package;

            commandService = serviceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            var menuCommandID = new CommandID(group, id);
            menuCommand = new OleMenuCommand(Invoke, menuCommandID);

            menuCommand.BeforeQueryStatus += MenuCommand_BeforeQueryStatus;

            commandService.AddCommand(menuCommand);
        }

        protected virtual void OnExecute(object sender, EventArgs e) { }

        protected virtual void OnQueryStatus(object sender, EventArgs e) { }


        private void Invoke(object sender, EventArgs e)
        {
            this.OnExecute(sender, e);
        }

        private void MenuCommand_BeforeQueryStatus(object sender, EventArgs e)
        {
            this.OnQueryStatus(sender, e);
        }

        public void Dispose()
        {
            menuCommand.BeforeQueryStatus -= this.MenuCommand_BeforeQueryStatus;
        }
    }
}
