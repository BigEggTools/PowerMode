namespace BigEgg.Tools.PowerMode
{
    using System;

    using Microsoft.VisualStudio.Text.Editor;
    using BigEgg.Tools.PowerMode.Adornments;

    internal sealed class PowerModeAdornment
    {
        private readonly IAdornmentLayer adornmentLayer;
        private readonly IWpfTextView view;
        private readonly ComboAdornment comboAdornment;

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

            this.view.ViewportHeightChanged += this.OnSizeChanged;
            this.view.ViewportWidthChanged += this.OnSizeChanged;
        }

        /// <summary>
        /// Event handler for viewport height or width changed events. Adds adornment at the top right corner of the viewport.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void OnSizeChanged(object sender, EventArgs e)
        {
            this.adornmentLayer.RemoveAllAdornments();

            var images = comboAdornment.OnSizeChanged(view);

            foreach (var image in images)
            {
                this.adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, image, null);
            }
        }
    }
}
