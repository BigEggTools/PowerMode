namespace BigEgg.Tools.PowerMode.Adornments
{
    using System;
    using Task = System.Threading.Tasks.Task;

    using Microsoft.VisualStudio.Text.Editor;

    using BigEgg.Tools.PowerMode.Services;
    using BigEgg.Tools.PowerMode.Utils;

    public class ScreenShakeAdornment : IAdornment
    {
        private readonly static int SHAKE_TIMEOUT_MILLISECONDS = 75;
        private readonly static int SHAKE_THROTTLED_MILLISECONDS = 50;
        private DateTime lastShakeTime = DateTime.Now;


        public void Cleanup(IAdornmentLayer adornmentLayer, IWpfTextView view)
        {
            lastShakeTime = DateTime.Now;
        }

        public void OnSizeChanged(IAdornmentLayer adornmentLayer, IWpfTextView view, int streakCount)
        {
            lastShakeTime = DateTime.Now;
        }

        public void OnTextBufferChanged(IAdornmentLayer adornmentLayer, IWpfTextView view, int streakCount)
        {
            if (lastShakeTime.AddMilliseconds(SHAKE_THROTTLED_MILLISECONDS) > DateTime.Now) { return; }

            Shake(view).ConfigureAwait(false);
        }

        public async Task Shake(IWpfTextView view)
        {
            var settings = SettingsService.GetScreenShakeSettings();

            int leftAmount = GetShakeIntensity(settings.MinIntensity, settings.MaxIntensity),
                topAmount = GetShakeIntensity(settings.MinIntensity, settings.MaxIntensity);

            lastShakeTime = DateTime.Now;
            view.ViewScroller.ScrollViewportHorizontallyByPixels(leftAmount);
            view.ViewScroller.ScrollViewportVerticallyByPixels(topAmount);
            //  TODO: Should have a better way to shake the screen

            await Task.Delay(SHAKE_TIMEOUT_MILLISECONDS);
            view.ViewScroller.ScrollViewportHorizontallyByPixels(-leftAmount);
            view.ViewScroller.ScrollViewportVerticallyByPixels(-topAmount);
        }


        private int GetShakeIntensity(int min, int max)
        {
            var direction = RandomUtils.NextSignal();
            return RandomUtils.Random.Next(min, max) * direction;
        }
    }
}
