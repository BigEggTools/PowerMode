using BigEgg.Tools.PowerMode.Settings;
using BigEgg.Tools.PowerMode.Utils;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Image = System.Windows.Controls.Image;

namespace BigEgg.Tools.PowerMode.Adornments
{
    public class ParticlesAdornment : IAdornment
    {
        private readonly static double PARTICLES_START_ALPHA = 0.9;
        private static double MAX_SIDE_VELOCITY = 2;
        private static double MAX_UP_VELOCITY = 10;
        private readonly double iterations;
        private readonly TimeSpan timeSpan;
        private readonly List<Image> particlesList;
        private ParticlesSettings settings;


        public ParticlesAdornment()
        {
            iterations = PARTICLES_START_ALPHA / Constants.ALPHA_REMOVE_AMOUNT;
            timeSpan = TimeSpan.FromMilliseconds(Constants.FRAME_DELAY_MILLISECOND * iterations);

            particlesList = new List<Image>();
        }


        public void Cleanup(IAdornmentLayer adornmentLayer, IWpfTextView view)
        {
            particlesList.ForEach(image => { adornmentLayer.RemoveAdornment(image); });
            particlesList.Clear();
        }

        public void OnSizeChanged(IAdornmentLayer adornmentLayer, IWpfTextView view, int streakCount)
        {
            particlesList.ForEach(image => { adornmentLayer.RemoveAdornment(image); });
            particlesList.Clear();
        }

        public void OnTextBufferChanged(IAdornmentLayer adornmentLayer, IWpfTextView view, int streakCount)
        {
            settings = SettingsService.GetParticlesSettings(ServiceProvider.GlobalProvider);

            var spawnedSize = RandomUtils.Random.Next(settings.MinSpawnedParticles, settings.MaxSpawnedParticles);
            if (spawnedSize + particlesList.Count > settings.MaxParticlesCount) { spawnedSize = settings.MaxParticlesCount - particlesList.Count; }

            for (int i = 0; i < spawnedSize; i++)
            {
                NewParticlesImage(adornmentLayer, view);
            }
        }


        private void NewParticlesImage(IAdornmentLayer adornmentLayer, IWpfTextView view)
        {
            var particles = new Image();
            particles.UpdateSource(GetParticlesImage());
            adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, particles, null);
            particlesList.Add(particles);

            var top = view.Caret.Top;
            var left = view.Caret.Left;
            var upVelocity = -RandomUtils.Random.NextDouble() * MAX_UP_VELOCITY;
            var leftVelocity = RandomUtils.Random.NextDouble() * MAX_SIDE_VELOCITY * RandomUtils.NextSignal();

            particles.BeginAnimation(Canvas.TopProperty, GetParticlesTopAnimation(top, upVelocity));
            particles.BeginAnimation(Canvas.LeftProperty, GetParticlesLeftAnimation(left, left - (iterations * leftVelocity)));
            var opacityAnimation = GetParticlesOpacityAnimation();
            opacityAnimation.Completed += (sender, e) =>
            {
                particles.Visibility = Visibility.Hidden;
                adornmentLayer.RemoveAdornment(particles);
                particlesList.Remove(particles);
            };
            particles.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
        }

        private Bitmap GetParticlesImage()
        {
            var color = RandomUtils.NextColor();
            var size = RandomUtils.Random.Next(settings.MinParticlesSize, settings.MaxParticlesSize) * 2;

            var bitmap = new Bitmap(size, size);
            bitmap.MakeTransparent();

            using (var graphics = Graphics.FromImage(bitmap))
            using (var brush = new SolidBrush(color))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphics.FillEllipse(brush, 0, 0, size - 1, size - 1);

                graphics.Flush();
                return bitmap;
            }
        }


        private DoubleAnimation GetParticlesTopAnimation(double from, double by)
        {
            return new DoubleAnimation()
            {
                EasingFunction = new BackEase(),
                From = from,
                By = by,
                Duration = timeSpan
            };
        }
        private DoubleAnimation GetParticlesLeftAnimation(double from, double to)
        {
            return new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = timeSpan
            };
        }

        private DoubleAnimation GetParticlesOpacityAnimation()
        {
            return new DoubleAnimation()
            {
                From = PARTICLES_START_ALPHA,
                To = 0,
                Duration = timeSpan
            };
        }
    }
}
