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

        private readonly GeneralSettings generalSettings;
        private readonly ComboModeSettings comboModeSettings;

        private Timer clearStreakCountTimer;
        private int streakCount = 0;


        public PowerModeAdornment(IWpfTextView view)
        {
            if (view == null) { throw new ArgumentNullException("view"); }

            this.view = view;
            adornmentLayer = view.GetAdornmentLayer("PowerModeAdornment");

            streakCounterAdornment = new StreakCounterAdornment();
            screenShakeAdornment = new ScreenShakeAdornment();

            generalSettings = new GeneralSettings();
            comboModeSettings = new ComboModeSettings();

            this.view.TextBuffer.Changed += TextBuffer_Changed;
            this.view.ViewportHeightChanged += View_ViewportSizeChanged;
            this.view.ViewportWidthChanged += View_ViewportSizeChanged;

            PropertyChangedEventManager.RemoveHandler(generalSettings, GeneralSettingModelPropertyChanged, "");
            PropertyChangedEventManager.RemoveHandler(comboModeSettings, ComboModeSettingsModelPropertyChanged, "");

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
        }

        private void TextBuffer_Changed(object sender, TextContentChangedEventArgs e)
        {
            RefreshSettings();

            if (!generalSettings.IsEnablePowerMode) { return; }

            if (generalSettings.IsEnableComboMode)
            {
                KeyDown();

                if (comboModeSettings.IsShowStreakCounter)
                {
                    streakCounterAdornment.OnTextBufferChanged(adornmentLayer, view, streakCount);
                }
                if (ComboService.CanShowExclamation(streakCount))
                {
                    screenShakeAdornment.OnTextBufferChanged(adornmentLayer, view, streakCount);
                }
            }
            else
            {
                screenShakeAdornment.OnTextBufferChanged(adornmentLayer, view, streakCount);
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
            generalSettings.CloneFrom(SettingsService.GetGeneralSettings(ServiceProvider.GlobalProvider));
            comboModeSettings.CloneFrom(SettingsService.GetComboModeSettings(ServiceProvider.GlobalProvider));
        }

        private void GeneralSettingModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ((e.PropertyName == nameof(GeneralSettings.IsEnablePowerMode) ||
                 e.PropertyName == nameof(GeneralSettings.IsEnableComboMode)) &&
                !generalSettings.IsEnablePowerMode)
            {
                streakCounterAdornment.Cleanup(adornmentLayer, view);
            }
            if ((e.PropertyName == nameof(GeneralSettings.IsEnablePowerMode) ||
                 e.PropertyName == nameof(GeneralSettings.IsEnableScreenShake)) &&
                !generalSettings.IsEnablePowerMode)
            {
                screenShakeAdornment.Cleanup(adornmentLayer, view);
            }
        }

        private void ComboModeSettingsModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ComboModeSettings.IsShowStreakCounter) &&
                !comboModeSettings.IsShowStreakCounter)
            {
                streakCounterAdornment.Cleanup(adornmentLayer, view);
            }
        }
    }
}
