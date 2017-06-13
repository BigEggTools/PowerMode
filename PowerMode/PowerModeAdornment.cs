namespace BigEgg.Tools.PowerMode
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows.Threading;

    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Editor;

    using BigEgg.Tools.PowerMode.Adornments;
    using BigEgg.Tools.PowerMode.Services;
    using BigEgg.Tools.PowerMode.Settings;

    internal sealed class PowerModeAdornment
    {
        private readonly IAdornmentLayer adornmentLayer;
        private readonly IWpfTextView view;
        private readonly IAdornment streakCounterAdornment;
        private readonly IAdornment screenShakeAdornment;
        private readonly IAdornment particlesAdornment;

        private readonly GeneralSettings generalSettings;
        private readonly ComboModeSettings comboModeSettings;

        private readonly static int TEXT_CHANGE_THROTTLED_MILLISECONDS = 50;
        private DateTime lastTextChangeTime = DateTime.Now;
        private Timer clearStreakCountTimer;
        private int streakCount = 0;


        public PowerModeAdornment(IWpfTextView view)
        {
            if (view == null) { throw new ArgumentNullException("view"); }

            this.view = view;
            adornmentLayer = view.GetAdornmentLayer("PowerModeAdornment");

            streakCounterAdornment = new StreakCounterAdornment();
            screenShakeAdornment = new ScreenShakeAdornment();
            particlesAdornment = new ParticlesAdornment();

            generalSettings = SettingsService.GetGeneralSettings();
            comboModeSettings = SettingsService.GetComboModeSettings();

            this.view.TextBuffer.Changed += TextBuffer_Changed;
            this.view.ViewportHeightChanged += View_ViewportSizeChanged;
            this.view.ViewportWidthChanged += View_ViewportSizeChanged;

            PropertyChangedEventManager.AddHandler(generalSettings, GeneralSettingModelPropertyChanged, "");
            PropertyChangedEventManager.AddHandler(comboModeSettings, ComboModeSettingsModelPropertyChanged, "");

        }

        private void View_ViewportSizeChanged(object sender, EventArgs e)
        {
            RefreshSettings();

            if (!generalSettings.IsEnablePowerMode) { return; }

            if (generalSettings.IsEnableComboMode && comboModeSettings.IsShowStreakCounter)
            {
                streakCounterAdornment.OnSizeChanged(adornmentLayer, view, streakCount);
            }
            screenShakeAdornment.Cleanup(adornmentLayer, view);
            particlesAdornment.Cleanup(adornmentLayer, view);
        }

        private void TextBuffer_Changed(object sender, TextContentChangedEventArgs e)
        {
            if (lastTextChangeTime.AddMilliseconds(TEXT_CHANGE_THROTTLED_MILLISECONDS) > DateTime.Now) { return; }
            lastTextChangeTime = DateTime.Now;
            //  TODO: Should have a better way to reduce effect of batch action, such as: CTRL+Z

            RefreshSettings();

            if (!generalSettings.IsEnablePowerMode) { return; }

            if (generalSettings.IsEnableComboMode)
            {
                KeyDown();

                if (comboModeSettings.IsShowStreakCounter)
                {
                    streakCounterAdornment.OnTextBufferChanged(adornmentLayer, view, streakCount);
                }
                if (ComboService.CanScreenShake(streakCount) && generalSettings.IsEnableScreenShake)
                {
                    screenShakeAdornment.OnTextBufferChanged(adornmentLayer, view, streakCount);
                }
                if (ComboService.CanShowParticles(streakCount) && generalSettings.IsEnableParticles)
                {
                    particlesAdornment.OnTextBufferChanged(adornmentLayer, view, streakCount);
                }
            }
            else
            {
                if (generalSettings.IsEnableParticles)
                {
                    particlesAdornment.OnTextBufferChanged(adornmentLayer, view, streakCount);
                }
                if (generalSettings.IsEnableScreenShake)
                {
                    screenShakeAdornment.OnTextBufferChanged(adornmentLayer, view, streakCount);
                }
            }
        }

        private void KeyDown()
        {
            RefreshSettings();

            streakCount++;

            var timeout = comboModeSettings.StreakTimeout * 1000;
            if (clearStreakCountTimer == null)
            {
                clearStreakCountTimer = new Timer(info =>
                {
                    streakCount = 0;
                    view.VisualElement.Dispatcher.Invoke(
                        () =>
                        {
                            RefreshSettings();
                            if (generalSettings.IsEnablePowerMode && generalSettings.IsEnableComboMode && comboModeSettings.IsShowStreakCounter)
                            {
                                streakCounterAdornment.OnTextBufferChanged(adornmentLayer, view, streakCount);
                            }
                        },
                        DispatcherPriority.ContextIdle);
                }, new AutoResetEvent(false), timeout, Timeout.Infinite);
            }
            else
            {
                clearStreakCountTimer.Change(timeout, Timeout.Infinite);
            }
        }

        private void RefreshSettings()
        {
            generalSettings.CloneFrom(SettingsService.GetGeneralSettings());
            comboModeSettings.CloneFrom(SettingsService.GetComboModeSettings());
        }

        private void GeneralSettingModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GeneralSettings.IsEnablePowerMode) && !generalSettings.IsEnablePowerMode)
            {
                streakCounterAdornment.Cleanup(adornmentLayer, view);
                screenShakeAdornment.Cleanup(adornmentLayer, view);
                particlesAdornment.Cleanup(adornmentLayer, view);
            }

            if (e.PropertyName == nameof(GeneralSettings.IsEnableComboMode))
            {
                if (!generalSettings.IsEnableComboMode)
                {
                    streakCounterAdornment.Cleanup(adornmentLayer, view);
                }
                else
                {
                    if (comboModeSettings.IsShowStreakCounter)
                    {
                        streakCounterAdornment.OnSizeChanged(adornmentLayer, view, streakCount);
                    }
                }
            }

            if ((e.PropertyName == nameof(GeneralSettings.IsEnableScreenShake) && !generalSettings.IsEnableScreenShake))
            {
                screenShakeAdornment.Cleanup(adornmentLayer, view);
            }

            if ((e.PropertyName == nameof(GeneralSettings.IsEnableParticles) && !generalSettings.IsEnableParticles))
            {
                particlesAdornment.Cleanup(adornmentLayer, view);
            }
        }

        private void ComboModeSettingsModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ComboModeSettings.IsShowStreakCounter) && !comboModeSettings.IsShowStreakCounter)
            {
                streakCounterAdornment.Cleanup(adornmentLayer, view);
            }
            else
            {
                if (generalSettings.IsEnablePowerMode && generalSettings.IsEnableComboMode)
                {
                    streakCounterAdornment.OnSizeChanged(adornmentLayer, view, streakCount);
                }
            }
        }
    }
}
