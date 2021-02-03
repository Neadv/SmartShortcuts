using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace SmartShortcuts.Services
{
    static class IconExtracter
    {
        public static ImageSource GetIcon(string path)
        {
            var iconBm = Icon.ExtractAssociatedIcon(path).ToBitmap();

            var rect = new Rectangle(0, 0, iconBm.Width, iconBm.Height);

            var bmData = iconBm.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            int bufferSize = (iconBm.Width * iconBm.Height) * 4;
            BitmapSource source = BitmapSource.Create(iconBm.Width, iconBm.Height,
                                                      iconBm.HorizontalResolution, iconBm.VerticalResolution,
                                                      PixelFormats.Bgra32, null, bmData.Scan0, bufferSize, bmData.Stride);

            iconBm.UnlockBits(bmData);

            return source;
        }
    }
}
