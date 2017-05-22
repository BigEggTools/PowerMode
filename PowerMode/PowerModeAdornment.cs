namespace BigEgg.Tools.PowerMode
{
    using System;
    using System.Linq;
    using System.Threading;

    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Editor;

    using BigEgg.Tools.PowerMode.Adornments;
    using BigEgg.Tools.PowerMode.Settings;
    using System.Windows.Threading;

    internal sealed class PowerModeAdornment
    {
        private readonly IAdornmentLayer adornmentLayer;
        private readonly IWpfTextView view;
        private readonly ComboAdornment comboAdornment;

        private Timer clearHitTimer;
        private int hitCount = 0;


        /// <summary>
        /// Initializes a new instance of the <see cref="PowerModeAdornment"/> class.
        /// Creates a square image and attaches an event handler to the layout changed event that
        /// adds the square in the upper right-hand corner of the TextView via the adornment layer
        /// </summary>
        /// <param name="view">The <see cref="IWpfTextView"/> upon which the adornment will be drawn</param>
        public PowerModeAdornment(IWpfTextView view)
        {
            if (view == null) { throw new ArgumentNullException("view"); }

            this.view = view;
            this.adornmentLayer = view.GetAdornmentLayer("PowerModeAdornment");
            this.comboAdornment = new ComboAdornment();

            this.view.TextBuffer.Changed += TextBuffer_Changed;
            this.view.ViewportHeightChanged += View_ViewportSizeChanged; ;
            this.view.ViewportWidthChanged += View_ViewportSizeChanged;
        }

        private void View_ViewportSizeChanged(object sender, EventArgs e)
        {
            this.adornmentLayer.RemoveAllAdornments();

            var images = comboAdornment.OnSizeChanged(view);

            foreach (var image in images)
            {
                this.adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, image, null);
            }
        }

        private void TextBuffer_Changed(object sender, TextContentChangedEventArgs e)
        {
            GeneralSettings settings = null;
            SettingsService.GetFromStorages(ref settings, ServiceProvider.GlobalProvider);

            if (!settings.IsEnablePowerMode) { return; }

            KeyDown();

            comboAdornment.OnTextBufferChanged(adornmentLayer, view, hitCount);
        }

        private void KeyDown()
        {
            hitCount++;

            if (clearHitTimer == null)
            {
                var autoEvent = new AutoResetEvent(false);
                clearHitTimer = new Timer(info =>
                {
                    hitCount = 0;
                    view.VisualElement.Dispatcher.Invoke(
                        () => comboAdornment.OnTextBufferChanged(adornmentLayer, view, hitCount),
                        DispatcherPriority.ContextIdle);
                }, autoEvent, 10000, Timeout.Infinite);
            }
            else
            {
                clearHitTimer.Change(10000, Timeout.Infinite);
            }
        }
    }
}
