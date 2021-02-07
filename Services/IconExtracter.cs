using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;

namespace SmartShortcuts.Services
{
    static class IconExtracter
    {
        private static Dictionary<string, ImageSource> icons = new Dictionary<string, ImageSource>();

        public static ImageSource GetIcon(string path)
        {
            if (path == null)
                return null;

            if (icons.ContainsKey(path))
                return icons[path];
            if (File.Exists(path))
                if (path.Contains(".png") || path.Contains(".jpg") || path.Contains(".jpeg") || path.Contains(".bmp"))
                    return FromImage(path);
                else
                    return ExtractIcon(path);
            return null;
        }

        private static ImageSource FromImage(string path)
        {
            Uri uri = new Uri(path);
            BitmapImage image = new BitmapImage(uri);
            return image;
        }

        private static ImageSource ExtractIcon(string path)
        {
            var iconBm = Icon.ExtractAssociatedIcon(path).ToBitmap();

            var rect = new Rectangle(0, 0, iconBm.Width, iconBm.Height);

            var bmData = iconBm.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            int bufferSize = (iconBm.Width * iconBm.Height) * 4;
            BitmapSource source = BitmapSource.Create(iconBm.Width, iconBm.Height,
                                                      iconBm.HorizontalResolution, iconBm.VerticalResolution,
                                                      PixelFormats.Bgra32, null, bmData.Scan0, bufferSize, bmData.Stride);

            iconBm.UnlockBits(bmData);

            icons.Add(path, source);

            return source;
        }
    }
}
