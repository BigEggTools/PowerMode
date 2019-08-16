namespace BigEgg.Tools.PowerMode.Adornments
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;
    using Image = System.Windows.Controls.Image;

    using Microsoft.VisualStudio.Text.Editor;

    using BigEgg.Tools.PowerMode.Services;
    using BigEgg.Tools.PowerMode.Settings;
    using BigEgg.Tools.PowerMode.Utils;

    public class ParticlesAdornment : IAdornment
    {
        private readonly static double PARTICLES_START_ALPHA = 1.0;
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
            settings = SettingsService.GetParticlesSettings();

            var spawnedSize = RandomUtils.Random.Next(settings.MinSpawnedParticles, settings.MaxSpawnedParticles);
            if (spawnedSize + particlesList.Count > settings.MaxParticlesCount) { spawnedSize = settings.MaxParticlesCount - particlesList.Count; }

            for (int i = 0; i < spawnedSize; i++)
            {
                NewParticleImage(adornmentLayer, view);
            }
        }


        private void NewParticleImage(IAdornmentLayer adornmentLayer, IWpfTextView view)
        {
            try
            {
                var particles = new Image();
                particles.UpdateSource(GetParticleImage());
                adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, particles, null);
                particlesList.Add(particles);

                try
                {
                    var top = view.Caret.Top;
                    var left = view.Caret.Left;
                    particles.BeginAnimation(Canvas.TopProperty, GetParticleTopAnimation(top));
                    particles.BeginAnimation(Canvas.LeftProperty, GetParticleLeftAnimation(left));
                    var opacityAnimation = GetParticleOpacityAnimation();
                    opacityAnimation.Completed += (sender, e) =>
                    {
                        particles.Visibility = Visibility.Hidden;
                        adornmentLayer.RemoveAdornment(particles);
                        particlesList.Remove(particles);
                    };
                    particles.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
                }
                catch
                {
                    adornmentLayer.RemoveAdornment(particles);
                    particlesList.Remove(particles);
                }
            }
            catch
            {
            }
        }

        private Bitmap GetParticleImage()
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


        private DoubleAnimation GetParticleTopAnimation(double top)
        {
            return new DoubleAnimation()
            {
                EasingFunction = new BackEase { Amplitude = RandomUtils.Random.NextDouble() * 3 + 0.5, EasingMode = EasingMode.EaseIn },
                From = top,
                To = top + RandomUtils.Random.Next(1, 30),
                Duration = timeSpan
            };
        }
        private DoubleAnimation GetParticleLeftAnimation(double left)
        {
            var leftDelta = RandomUtils.Random.NextDouble() * 40 * RandomUtils.NextSignal();

            return new DoubleAnimation()
            {
                From = left,
                To = left - leftDelta,
                Duration = timeSpan
            };
        }

        private DoubleAnimation GetParticleOpacityAnimation()
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
