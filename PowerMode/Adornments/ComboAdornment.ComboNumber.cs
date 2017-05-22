namespace BigEgg.Tools.PowerMode.Adornments
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Media.Imaging;

    using BigEgg.Tools.PowerMode.Services;

    public partial class ComboAdornment
    {
        private BitmapImage UpdateComboNumberImage(int comboHit)
        {
            var font = new Font("Tahoma", ComboService.GetPowerLevelFontSize(comboHit));
            var color = ComboService.GetPowerLevelColor(comboHit);
            var penWidth = ComboService.GetPowerLevelPenWidth(comboHit);

            var bitmap = new Bitmap(ADORNMENT_WIDTH, ADORNMENT_COMBO_NUMBER_HEIGHT);
            bitmap.MakeTransparent();

            var graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var size = graphics.MeasureString(comboHit.ToString(), font);
            graphics.DrawString(comboHit.ToString(), font, new SolidBrush(color), new RectangleF(ADORNMENT_WIDTH - size.Width, 0, size.Width, ADORNMENT_COMBO_NUMBER_HEIGHT));

            var pen = new Pen(color, penWidth);
            graphics.DrawLine(pen, ADORNMENT_WIDTH - size.Width, size.Height - 5 + penWidth / 2, size.Width, size.Height - 5 + penWidth / 2);

            graphics.Flush();

            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }
    }
}
