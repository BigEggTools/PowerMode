namespace BigEgg.Tools.PowerMode.Options.Converters
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Data;
    using Color2 = System.Windows.Media.Color;

    using BigEgg.Tools.PowerMode.Utils;

    public class ColorDrawingToMediaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is Color c) { return c.ToMediaColor(); }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is Color2 c) { return c.ToDrawingColor(); }
            return value;
        }
    }
}
