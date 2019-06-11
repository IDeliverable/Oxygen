using System;
using System.ComponentModel.Design;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;
using Commands = IDeliverable.Oxygen.Core.Commands;
using Microsoft;
using Microsoft.VisualStudio.Shell.Interop;

namespace IDeliverable.Oxygen.Extension
{
	sealed class OxygenCommands
	{
		private static readonly Guid sCommandSetId = new Guid("71af475e-976d-44bd-b07e-cd6846d102b9");
		private static readonly int sNewEntitiesFromDatabaseCommandId = 256;
		private static readonly int sRebuildAllEntitiesCommandId = 257;
		private static readonly int sEditEntityCommandId = 258;
		private static readonly int sEditSettingsCommandId = 259;
		private static readonly int sRebuildEntityCommandId = 260;
		
		public static async Task InitializeAsync(AsyncPackage package)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

			var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
			Assumes.Present(commandService);
			var dte = await package.GetServiceAsync(typeof(DTE)) as DTE2;
			var statusbar = await package.GetServiceAsync(typeof(SVsStatusbar)) as IVsStatusbar;

			var commands = new Commands(dte, statusbar);

			commandService.AddCommand(new OleMenuCommand((sender, e) => { commands.NewEntitiesFromDatabaseInvoke(); }, (sender, e) => { }, (sender, e) => { commands.NewEntitiesFromDatabaseQueryStatus(sender as OleMenuCommand); }, new CommandID(sCommandSetId, sNewEntitiesFromDatabaseCommandId)));
			commandService.AddCommand(new OleMenuCommand((sender, e) => { commands.RebuildAllEntitiesInvoke(); }, (sender, e) => { }, (sender, e) => { commands.RebuildAllEntitiesQueryStatus(sender as OleMenuCommand); }, new CommandID(sCommandSetId, sRebuildAllEntitiesCommandId)));
			commandService.AddCommand(new OleMenuCommand((sender, e) => { commands.EditEntityInvoke(); }, (sender, e) => { }, (sender, e) => { commands.EditEntityQueryStatus(sender as OleMenuCommand); }, new CommandID(sCommandSetId, sEditEntityCommandId)));
			commandService.AddCommand(new OleMenuCommand((sender, e) => { commands.EditSettingsInvoke(); }, (sender, e) => { }, (sender, e) => { commands.EditSettingsQueryStatus(sender as OleMenuCommand); }, new CommandID(sCommandSetId, sEditSettingsCommandId)));
			commandService.AddCommand(new OleMenuCommand((sender, e) => { commands.RebuildEntityInvoke(); }, (sender, e) => { }, (sender, e) => { commands.RebuildEntityQueryStatus(sender as OleMenuCommand); }, new CommandID(sCommandSetId, sRebuildEntityCommandId)));
		}
	}
}
