namespace BigEgg.Tools.PowerMode.Adornments
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    using Microsoft.VisualStudio.Text.Editor;

    using BigEgg.Tools.PowerMode.Services;
    using BigEgg.Tools.PowerMode.Utils;

    public partial class StreakCounterAdornment : IAdornment
    {
        private const int ADORNMENT_WIDTH = 100;
        private const int ADORNMENT_TITLE_HEIGHT = 15;
        private const int ADORNMENT_STREAK_COUNTER_HEIGHT = 65;
        private const int ADORNMENT_EXCLAMATION_HEIGHT = 30;
        private const int TopMargin = 30;
        private const int RightMargin = 30;
        private Image titleImage;
        private Image streakCounterImage;
        private Image exclamationImage;
        private int newMaxComboStreak = 0;
        private bool newMaxComboStreakReached = false;


        public void Cleanup(IAdornmentLayer adornmentLayer, IWpfTextView view)
        {
            if (titleImage != null)
            {
                adornmentLayer.RemoveAdornment(titleImage);
                titleImage = null;
            }
            if (streakCounterImage != null)
            {
                adornmentLayer.RemoveAdornment(streakCounterImage);
                streakCounterImage = null;
            }
            if (exclamationImage != null)
            {
                adornmentLayer.RemoveAdornment(exclamationImage);
                exclamationImage = null;
            }
            newMaxComboStreakReached = false;
        }

        public void OnSizeChanged(IAdornmentLayer adornmentLayer, IWpfTextView view, int streakCount)
        {
            if (titleImage == null)
            {
                titleImage = new Image();
                titleImage.UpdateSource(GetTitleImage());
            }
            adornmentLayer.RefreshImage(titleImage, view.ViewportRight - RightMargin - ADORNMENT_WIDTH, view.ViewportTop + TopMargin);

            if (streakCounterImage == null) { streakCounterImage = new Image(); }
            streakCounterImage.UpdateSource(GetStreakCounterImage(streakCount).Item1);
            adornmentLayer.RefreshImage(streakCounterImage, view.ViewportRight - RightMargin - ADORNMENT_WIDTH, view.ViewportTop + TopMargin + ADORNMENT_TITLE_HEIGHT);

            if (exclamationImage != null) { adornmentLayer.RemoveAdornment(exclamationImage); }
        }

        public void OnTextBufferChanged(IAdornmentLayer adornmentLayer, IWpfTextView view, int streakCount)
        {
            if (titleImage == null)
            {
                titleImage = new Image();
                titleImage.UpdateSource(GetTitleImage());
                adornmentLayer.RefreshImage(titleImage, view.ViewportRight - RightMargin - ADORNMENT_WIDTH, view.ViewportTop + TopMargin);
            }


            var comboNumberImageTuple = GetStreakCounterImage(streakCount);
            if (streakCounterImage == null) { streakCounterImage = new Image(); }
            streakCounterImage.UpdateSource(comboNumberImageTuple.Item1);
            adornmentLayer.RefreshImage(streakCounterImage, view.ViewportRight - RightMargin - ADORNMENT_WIDTH, view.ViewportTop + TopMargin + ADORNMENT_TITLE_HEIGHT);

            ScaleTransform trans = new ScaleTransform();
            streakCounterImage.RenderTransformOrigin = new Point((ADORNMENT_WIDTH - comboNumberImageTuple.Item2.Width / 2) / ADORNMENT_WIDTH, (comboNumberImageTuple.Item2.Height / 2) / comboNumberImageTuple.Item2.Height);
            streakCounterImage.RenderTransform = trans;

            trans.BeginAnimation(ScaleTransform.ScaleXProperty, GetStreakCounterImageSizeAnimation(streakCount));
            trans.BeginAnimation(ScaleTransform.ScaleYProperty, GetStreakCounterImageSizeAnimation(streakCount));


            if (ComboService.ShowExclamation(streakCount))
            {
                ShowExclamation(adornmentLayer, view, GetExclamationImage(streakCount), comboNumberImageTuple.Item2.Height);
            }

            var achievevments = AchievementsService.GetAchievements();
            if (streakCount > 0 && achievevments.MaxComboStreak < streakCount && !newMaxComboStreakReached)
            {
                ShowExclamation(adornmentLayer, view, GetNewMaxExclamationImage(streakCount), comboNumberImageTuple.Item2.Height);
                newMaxComboStreakReached = true;
            }

            if (streakCount == 0 && newMaxComboStreak > 0)
            {
                achievevments.MaxComboStreak = newMaxComboStreak;
                AchievementsService.SaveToStorage(achievevments);
                newMaxComboStreakReached = false;
            }
            newMaxComboStreak = streakCount;
        }

        private void ShowExclamation(IAdornmentLayer adornmentLayer, IWpfTextView view, System.Drawing.Bitmap exclamationBitmap, float streakCounterHeight)
        {
            if (exclamationImage == null) { exclamationImage = new Image(); }
            exclamationImage.UpdateSource(exclamationBitmap);

            exclamationImage.BeginAnimation(Canvas.TopProperty, null);
            exclamationImage.BeginAnimation(UIElement.OpacityProperty, null);
            exclamationImage.Visibility = Visibility.Visible;

            double exclamationImageTop = view.ViewportTop + TopMargin + ADORNMENT_TITLE_HEIGHT + streakCounterHeight + 5;
            adornmentLayer.RefreshImage(exclamationImage, view.ViewportRight - RightMargin - ADORNMENT_WIDTH, exclamationImageTop);

            exclamationImage.BeginAnimation(Canvas.TopProperty, GetExclamationTopAnimation(exclamationImageTop));
            var opacityAnimation = GetExclamationOpacityAnimation();
            opacityAnimation.Completed += (sender, e) => exclamationImage.Visibility = Visibility.Hidden;
            exclamationImage.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
        }
    }
}
