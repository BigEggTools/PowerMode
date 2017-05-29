namespace BigEgg.Tools.PowerMode.Options.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    using BigEgg.Tools.PowerMode.Utils;

    [TemplatePart(Name = PART_SpectrumDisplay, Type = typeof(Rectangle))]
    public class ColorSpectrumSlider : Slider
    {
        private const string PART_SpectrumDisplay = "PART_SpectrumDisplay";

        private Rectangle spectrumDisplay;


        static ColorSpectrumSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorSpectrumSlider), new FrameworkPropertyMetadata(typeof(ColorSpectrumSlider)));
        }


        #region Dependency Properties
        #region Selected Color
        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color?), typeof(ColorSpectrumSlider), new PropertyMetadata(Colors.Transparent));

        public Color? SelectedColor
        {
            get { return (Color?)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        #endregion
        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            spectrumDisplay = GetTemplateChild(PART_SpectrumDisplay) as Rectangle;
            CreateSpectrum();
            OnValueChanged(Double.NaN, Value);
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            SelectedColor = ColorExtensions.ColorFromHSV(360 - newValue, 1, 1).ToMediaColor();
        }


        private void CreateSpectrum()
        {
            var pickerBrush = new LinearGradientBrush()
            {
                StartPoint = new Point(0.5f, 0),
                EndPoint = new Point(0.5f, 1),
                ColorInterpolationMode = ColorInterpolationMode.SRgbLinearInterpolation
            };

            List<Color> colorsList = GenerateHsvSpectrum();
            double stopIncrement = (double)1 / colorsList.Count;
            for (int i = 0; i < colorsList.Count; i++)
            {
                pickerBrush.GradientStops.Add(new GradientStop(colorsList[i], i * stopIncrement));
            }
            pickerBrush.GradientStops.Last().Offset = 1.0;

            spectrumDisplay.Fill = pickerBrush;
        }

        private static List<Color> GenerateHsvSpectrum()
        {
            List<Color> colorsList = new List<Color>(8);

            for (int i = 0; i < 29; i++)
            {
                colorsList.Add(ColorExtensions.ColorFromHSV(i * 12, 1, 1).ToMediaColor());
            }
            colorsList.Add(ColorExtensions.ColorFromHSV(0, 1, 1).ToMediaColor());

            return colorsList;
        }
    }
}
