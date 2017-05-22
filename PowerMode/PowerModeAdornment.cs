namespace BigEgg.Tools.PowerMode
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Editor;

    using BigEgg.Tools.PowerMode.Adornments;
    using BigEgg.Tools.PowerMode.Settings;

    internal sealed class PowerModeAdornment
    {
        private readonly IAdornmentLayer adornmentLayer;
        private readonly IWpfTextView view;
        private readonly ComboAdornment comboAdornment;

        private DateTime lastKeyPressTime = DateTime.Now;
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
            if (e.Changes?.Count > 0) { e.Changes.Sum(x => x.Delta); }

            comboAdornment.OnTextBufferChanged(adornmentLayer, view, hitCount);
        }

        private void KeyDown()
        {
            var now = DateTime.Now;

            if (lastKeyPressTime.AddSeconds(10) < now)
            {
                hitCount = 1;
            }
            else
            {
                hitCount++;
            }
            lastKeyPressTime = now;
        }
    }
}
