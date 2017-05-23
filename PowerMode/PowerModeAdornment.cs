namespace BigEgg.Tools.PowerMode
{
    using System;
    using System.Threading;
    using System.Windows.Threading;

    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Editor;

    using BigEgg.Tools.PowerMode.Adornments;
    using BigEgg.Tools.PowerMode.Settings;

    internal sealed class PowerModeAdornment
    {
        private readonly IAdornmentLayer adornmentLayer;
        private readonly IWpfTextView view;
        private readonly StreakCounterAdornment streakCounterAdornment;

        private Timer clearHitTimer;
        private int hitCount = 0;


        public PowerModeAdornment(IWpfTextView view)
        {
            if (view == null) { throw new ArgumentNullException("view"); }

            this.view = view;
            this.adornmentLayer = view.GetAdornmentLayer("PowerModeAdornment");
            this.streakCounterAdornment = new StreakCounterAdornment();

            this.view.TextBuffer.Changed += TextBuffer_Changed;
            this.view.ViewportHeightChanged += View_ViewportSizeChanged; ;
            this.view.ViewportWidthChanged += View_ViewportSizeChanged;
        }

        private void View_ViewportSizeChanged(object sender, EventArgs e)
        {
            this.adornmentLayer.RemoveAllAdornments();

            var generalSettings = SettingsService.GetGeneralSettings(ServiceProvider.GlobalProvider);
            if (!generalSettings.IsEnablePowerMode) { return; }

            if (generalSettings.IsEnableComboMode)
            {
                var comboModeSettings = SettingsService.GetComboModeSettings(ServiceProvider.GlobalProvider);
                if (comboModeSettings.IsShowStreakCounter)
                {
                    streakCounterAdornment.OnSizeChanged(view)
                                  .ForEach(image =>
                                        adornmentLayer.AddAdornment(
                                            AdornmentPositioningBehavior.ViewportRelative,
                                            null,
                                            null,
                                            image,
                                            null));
                }
            }

        }

        private void TextBuffer_Changed(object sender, TextContentChangedEventArgs e)
        {
            var generalSettings = SettingsService.GetGeneralSettings(ServiceProvider.GlobalProvider);
            if (!generalSettings.IsEnablePowerMode) { return; }

            if (generalSettings.IsEnableComboMode)
            {
                KeyDown();

                var comboModeSettings = SettingsService.GetComboModeSettings(ServiceProvider.GlobalProvider);
                if (comboModeSettings.IsShowStreakCounter)
                {
                    streakCounterAdornment.OnTextBufferChanged(adornmentLayer, view, hitCount);
                }
            }
        }

        private void KeyDown()
        {
            var comboModeSettings = SettingsService.GetComboModeSettings(ServiceProvider.GlobalProvider);

            hitCount++;

            var timeout = comboModeSettings.StreakTimeout * 1000;
            if (clearHitTimer == null)
            {
                var autoEvent = new AutoResetEvent(false);
                clearHitTimer = new Timer(info =>
                {
                    hitCount = 0;
                    view.VisualElement.Dispatcher.Invoke(
                        () => streakCounterAdornment.OnTextBufferChanged(adornmentLayer, view, hitCount),
                        DispatcherPriority.ContextIdle);
                }, autoEvent, timeout, Timeout.Infinite);
            }
            else
            {
                clearHitTimer.Change(timeout, Timeout.Infinite);
            }
        }
    }
}
