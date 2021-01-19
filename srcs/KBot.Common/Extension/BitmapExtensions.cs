using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace KBot.Common.Extension
{
    public static class BitmapExtensions
    {
        public static BitmapImage ToBitmapSource(this Bitmap bitmap)
        {
            var image = new BitmapImage();

            var ms = new MemoryStream();
     
            bitmap.Save(ms, ImageFormat.Png);
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            image.Freeze();
            
            return image;
        }
    }
}