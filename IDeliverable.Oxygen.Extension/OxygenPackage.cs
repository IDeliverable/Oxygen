using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace IDeliverable.Oxygen.Extension
{
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[Guid(PackageGuidString)]
	[ProvideMenuResource("Menus.ctmenu", 1)]
	[ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExistsAndFullyLoaded_string, PackageAutoLoadFlags.BackgroundLoad)]
	public sealed class OxygenPackage : AsyncPackage
	{
		public const string PackageGuidString = "05f4fe03-a3b5-471d-b27c-c37e0bbf2076";

		protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
		{
			await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
		    await OxygenCommands.InitializeAsync(this);
		}
	}
}
