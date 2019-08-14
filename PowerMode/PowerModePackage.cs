namespace BigEgg.Tools.PowerMode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Threading;

    using Microsoft.VisualStudio.Shell;

    using BigEgg.Tools.PowerMode.Commands;
    using BigEgg.Tools.PowerMode.Options;

    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "0.6.2", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(PowerModePackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideOptionPage(typeof(GeneralOptionPage), "Power Mode", "General", 0, 0, true)]
    [ProvideOptionPage(typeof(ComboModeOptionPage), "Power Mode", "Combo Mode", 0, 0, true)]
    [ProvideOptionPage(typeof(ScreenShakeOptionPage), "Power Mode", "Screen Shake", 0, 0, true)]
    [ProvideOptionPage(typeof(ParticlesOptionPage), "Power Mode", "Particles", 0, 0, true)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad("ADFC4E64-0397-11D1-9F4E-00A0C911004F")]
    public sealed class PowerModePackage : AsyncPackage
    {
        /// <summary>
        /// PowerModePackage GUID string.
        /// </summary>
        public const string PackageGuidString = "4f67b5b3-fd7c-44ab-9bb6-e03b9bab9294";

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerModePackage"/> class.
        /// </summary>
        public PowerModePackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override async System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            progress.Report(new ServiceProgressData("Starting Power Mode", "Initializing...", 0, 6));
            await base.InitializeAsync(cancellationToken, progress);

            cancellationToken.ThrowIfCancellationRequested();
            progress.Report(new ServiceProgressData("Starting Power Mode", "Initializing...", 1, 6));
            TogglePowerModeCommand.Initialize(this);

            cancellationToken.ThrowIfCancellationRequested();
            progress.Report(new ServiceProgressData("Starting Power Mode", "Initializing...", 2, 6));
            ToggleComboModeCommand.Initialize(this);

            cancellationToken.ThrowIfCancellationRequested();
            progress.Report(new ServiceProgressData("Starting Power Mode", "Initializing...", 3, 6));
            ToggleParticlesCommand.Initialize(this);

            cancellationToken.ThrowIfCancellationRequested();
            progress.Report(new ServiceProgressData("Starting Power Mode", "Initializing...", 4, 6));
            ToggleScreenShakeCommand.Initialize(this);

            cancellationToken.ThrowIfCancellationRequested();
            progress.Report(new ServiceProgressData("Starting Power Mode", "Initializing...", 5, 6));
            ToggleAudioCommand.Initialize(this);

            cancellationToken.ThrowIfCancellationRequested();
            progress.Report(new ServiceProgressData("Starting Power Mode", "Initializing...", 6, 6));
        }
        #endregion
    }
}
