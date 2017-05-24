namespace BigEgg.Tools.PowerMode.Adornments
{
    using System.Threading;
    using System.Windows.Threading;

    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Text.Editor;

    using BigEgg.Tools.PowerMode.Settings;
    using BigEgg.Tools.PowerMode.Utils;

    public class ScreenShakeAdornment : IAdornment
    {
        private readonly static int SHAKE_TIMEOUT_MILLISECONDS = 75;
        private readonly static int SHAKE_THROTTLED_MILLISECONDS = 100;
        private Timer clearScreenShakeTimer;
        private ThrottledAction<IWpfTextView> throttledShake;


        public ScreenShakeAdornment()
        {
            throttledShake = new ThrottledAction<IWpfTextView>(Shake, SHAKE_THROTTLED_MILLISECONDS);
        }


        public void Cleanup(IAdornmentLayer adornmentLayer, IWpfTextView view)
        {
            throttledShake.Cancel();
        }

        public void OnSizeChanged(IAdornmentLayer adornmentLayer, IWpfTextView view, int streakCount)
        {
            throttledShake.Cancel();
        }

        public void OnTextBufferChanged(IAdornmentLayer adornmentLayer, IWpfTextView view, int streakCount)
        {
            throttledShake.InvokeAccumulated(view);
        }

        public void Shake(IWpfTextView view)
        {
            var settings = SettingsService.GetScreenShakeSettings(ServiceProvider.GlobalProvider);

            int leftAmount = GetShakeIntensity(settings.MinIntensity, settings.MaxIntensity),
                topAmount = GetShakeIntensity(settings.MinIntensity, settings.MaxIntensity);

            view.ViewportLeft += leftAmount;
            view.ViewScroller.ScrollViewportVerticallyByPixels(topAmount);

            if (clearScreenShakeTimer == null)
            {
                clearScreenShakeTimer = new Timer(info =>
                {
                    view.VisualElement.Dispatcher.Invoke(
                        () =>
                        {
                            view.ViewportLeft -= leftAmount;
                            view.ViewScroller.ScrollViewportVerticallyByPixels(-topAmount);
                        },
                        DispatcherPriority.ContextIdle);
                }, new AutoResetEvent(false), SHAKE_TIMEOUT_MILLISECONDS, Timeout.Infinite);
            }
        }


        private int GetShakeIntensity(int min, int max)
        {
            var direction = RandomUtils.NextSignal();
            return RandomUtils.Random.Next(min, max) * direction;
        }
    }
}
