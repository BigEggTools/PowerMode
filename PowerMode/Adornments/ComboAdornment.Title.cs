namespace BigEgg.Tools.PowerMode.Adornments
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Media.Imaging;

    public partial class ComboAdornment
    {
        private const string COMBO_TITLE = "combo";


        private BitmapImage UpdateTitleImage()
        {
            var bitmap = new Bitmap(ADORNMENT_WIDTH, ADORNMENT_TITLE_HEIGHT);
            bitmap.MakeTransparent();

            var graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var font = new Font("Tahoma", 8);
            var size = graphics.MeasureString(COMBO_TITLE, font);
            graphics.DrawString(COMBO_TITLE, font, Brushes.White, new RectangleF(ADORNMENT_WIDTH - size.Width, 0, size.Width, ADORNMENT_TITLE_HEIGHT));

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
