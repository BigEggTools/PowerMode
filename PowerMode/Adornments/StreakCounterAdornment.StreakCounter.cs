namespace BigEgg.Tools.PowerMode.Adornments
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Media.Animation;

    using BigEgg.Tools.PowerMode.Services;

    public partial class StreakCounterAdornment
    {
        private Tuple<Bitmap, SizeF> GetStreakCounterImage(int streakCount)
        {
            var font = new Font("Tahoma", ComboService.GetPowerLevelFontSize(streakCount));
            var color = ComboService.GetPowerLevelColor(streakCount);
            var penWidth = ComboService.GetPowerLevelPenWidth(streakCount);

            var bitmap = new Bitmap(ADORNMENT_WIDTH, ADORNMENT_STREAK_COUNTER_HEIGHT);
            bitmap.MakeTransparent();

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                var size = graphics.MeasureString(streakCount.ToString(), font);
                graphics.DrawString(streakCount.ToString(), font, new SolidBrush(color), new RectangleF(ADORNMENT_WIDTH - size.Width, 0, size.Width, ADORNMENT_STREAK_COUNTER_HEIGHT));

                var pen = new Pen(color, penWidth);
                graphics.DrawLine(pen, ADORNMENT_WIDTH - size.Width, size.Height - 5 + penWidth / 2, ADORNMENT_WIDTH, size.Height - 5 + penWidth / 2);

                graphics.Flush();

                return new Tuple<Bitmap, SizeF>(bitmap, size);
            }
        }


        private DoubleAnimation GetStreakCounterImageSizeAnimation(int streakCount)
        {
            if (!ComboService.AnimationOnStreakCouunter(streakCount))
            {
                return null;
            }

            return new DoubleAnimation()
            {
                EasingFunction = new BackEase { Amplitude = 2, EasingMode = EasingMode.EaseOut },
                From = 0.85,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(100)
            };
        }
    }
}
