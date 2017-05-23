namespace BigEgg.Tools.PowerMode.Adornments
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    using Microsoft.VisualStudio.Text.Editor;

    using BigEgg.Tools.PowerMode.Services;

    public partial class StreakCounterAdornment
    {
        private const int ADORNMENT_WIDTH = 100;
        private const int ADORNMENT_TITLE_HEIGHT = 15;
        private const int ADORNMENT_STREAK_COUNTER_HEIGHT = 65;
        private const int ADORNMENT_EXCLAMATION_HEIGHT = 30;
        private const double TopMargin = 30;
        private const double RightMargin = 30;
        private Image titleImage;
        private Image streakCounterImage;
        private Image exclamationImage;


        public StreakCounterAdornment()
        {
            this.titleImage = new Image()
            {
                Source = UpdateTitleImage()
            };
            this.streakCounterImage = new Image()
            {
                Source = UpdateStreakCounterImage(0).Item1
            };
            this.exclamationImage = new Image()
            {
                Source = UpdateExclamationImage(0)
            };
        }

        public List<Image> OnSizeChanged(IWpfTextView view)
        {
            Canvas.SetLeft(this.titleImage, view.ViewportRight - RightMargin - ADORNMENT_WIDTH);
            Canvas.SetTop(this.titleImage, view.ViewportTop + TopMargin);

            Canvas.SetLeft(this.streakCounterImage, view.ViewportRight - RightMargin - ADORNMENT_WIDTH);
            Canvas.SetTop(this.streakCounterImage, view.ViewportTop + TopMargin + ADORNMENT_TITLE_HEIGHT);

            return new List<Image>()
            {
                titleImage,
                streakCounterImage
            };
        }

        public void OnTextBufferChanged(IAdornmentLayer adornmentLayer, IWpfTextView view, int streakCount)
        {
            #region Title
            adornmentLayer.RemoveAdornment(titleImage);
            Canvas.SetLeft(this.titleImage, view.ViewportRight - RightMargin - ADORNMENT_WIDTH);
            Canvas.SetTop(this.titleImage, view.ViewportTop + TopMargin);
            adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, titleImage, null);
            #endregion

            #region StreakCounter
            adornmentLayer.RemoveAdornment(streakCounterImage);

            var comboNumberImageTuple = UpdateStreakCounterImage(streakCount);
            streakCounterImage.Source = comboNumberImageTuple.Item1;
            Canvas.SetLeft(streakCounterImage, view.ViewportRight - RightMargin - ADORNMENT_WIDTH);
            Canvas.SetTop(streakCounterImage, view.ViewportTop + TopMargin + ADORNMENT_TITLE_HEIGHT);
            adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, streakCounterImage, null);

            ScaleTransform trans = new ScaleTransform();
            streakCounterImage.RenderTransformOrigin = new Point((ADORNMENT_WIDTH - comboNumberImageTuple.Item2.Width / 2) / ADORNMENT_WIDTH, (comboNumberImageTuple.Item2.Height / 2) / comboNumberImageTuple.Item2.Height);
            streakCounterImage.RenderTransform = trans;

            trans.BeginAnimation(ScaleTransform.ScaleXProperty, GetStreakCounterImageSizeAnimation(streakCount));
            trans.BeginAnimation(ScaleTransform.ScaleYProperty, GetStreakCounterImageSizeAnimation(streakCount));
            #endregion

            #region Exclamation
            if (ComboService.ShowExclamation(streakCount))
            {
                adornmentLayer.RemoveAdornment(exclamationImage);
                exclamationImage.BeginAnimation(Canvas.TopProperty, null);
                exclamationImage.BeginAnimation(UIElement.OpacityProperty, null);
                exclamationImage.Visibility = Visibility.Visible;

                exclamationImage.Source = UpdateExclamationImage(streakCount);
                Canvas.SetLeft(exclamationImage, view.ViewportRight - RightMargin - ADORNMENT_WIDTH);
                double exclamationImageTop = view.ViewportTop + TopMargin + ADORNMENT_TITLE_HEIGHT + comboNumberImageTuple.Item2.Height + 5;
                Canvas.SetTop(exclamationImage, exclamationImageTop);
                adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, exclamationImage, null);

                exclamationImage.BeginAnimation(Canvas.TopProperty, GetExclamationTopAnimation(exclamationImageTop));
                var opacityAnimation = GetExclamationOpacityAnimation();
                opacityAnimation.Completed += (sender, e) => exclamationImage.Visibility = Visibility.Hidden;
                exclamationImage.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            }
            #endregion
        }
    }
}
