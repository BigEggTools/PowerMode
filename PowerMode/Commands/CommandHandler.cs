namespace BigEgg.Tools.PowerMode.Commands
{
    using System;
    using System.ComponentModel.Design;

    using Microsoft.VisualStudio.Shell;

    internal abstract class CommandHandler : IDisposable
    {
        protected readonly Guid commandSet;
        protected readonly int commandid;

        protected IMenuCommandService commandService;
        protected OleMenuCommand menuCommand;


        protected CommandHandler(Guid group, int id)
        {
            this.commandSet = group;
            this.commandid = id;
        }


        protected async System.Threading.Tasks.Task InitalizeAsync(AsyncPackage package)
        {
            commandService = (IMenuCommandService)await package.GetServiceAsync(typeof(IMenuCommandService));
            var menuCommandID = new CommandID(commandSet, commandid);
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
