namespace BigEgg.Tools.PowerMode.Utils
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using Controls = System.Windows.Controls;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    using Microsoft.VisualStudio.Text.Editor;

    public static class ImageUtils
    {
        public static void UpdateSource(this Controls.Image image, Bitmap bitmap)
        {
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

            image.Source = bitmapImage;
        }

        public static void RefreshImage(this IAdornmentLayer adornmentLayer, Controls.Image image, double left, double right)
        {
            adornmentLayer.RemoveAdornment(image);
            Canvas.SetLeft(image, left);
            Canvas.SetTop(image, right);
            adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, image, null);
        }
    }
}
