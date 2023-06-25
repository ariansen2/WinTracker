using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WinTracker1.Helper.ScreenManager;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace WinTracker1.Helper
{
    public class ScreenManager2
    {
        //public static void CaptureActiveWindow()
        //{
        //    // Get the handle of the active window
        //    IntPtr handle = NativeMethods.GetForegroundWindow();

        //    // Capture the window image
        //    CaptureWindow(handle);
        //}

        public static Bitmap CaptureWindow()
        {
            // Get the handle of the active window
            IntPtr handle = NativeMethods.GetForegroundWindow();
            // Get the bounds of the window
            NativeMethods.RECT rect;
            NativeMethods.GetWindowRect(handle, out rect);

            // Create a bitmap with the same size as the window
            Bitmap bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Copy the window image to the graphics object
                IntPtr hdc = graphics.GetHdc();
                NativeMethods.PrintWindow(handle, hdc, 0);
                graphics.ReleaseHdc(hdc);
            }
            //string filePath = String.Format((string)ConfigurationManager.AppSettings["LogPath"],
            //    DateTime.Now.Date.ToString("dd.MM.yyyy"), "WindowCapture_" + DateTime.Now.ToString("hh_mm_ss"));
            var filePath = DiskManager.CapturedImageWindowPathandFilename();
            return bitmap;
        }
        private void CaptureWindow(IntPtr handle)
        {
            // Get the bounds of the window
            NativeMethods.RECT rect;
            NativeMethods.GetWindowRect(handle, out rect);

            // Create a bitmap with the same size as the window
            Bitmap bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Copy the window image to the graphics object
                IntPtr hdc = graphics.GetHdc();
                NativeMethods.PrintWindow(handle, hdc, 0);
                graphics.ReleaseHdc(hdc);
            }
            //string filePath = String.Format((string)ConfigurationManager.AppSettings["LogPath"],
            //    DateTime.Now.Date.ToString("dd.MM.yyyy"), "WindowCapture_" + DateTime.Now.ToString("hh_mm_ss"));
            var filePath = DiskManager.CapturedImageWindowPathandFilename();
            // Save the bitmap to a file
            // string filePath = ConfigurationManager.AppSettings["ActiveWindowImagePath"];
            bitmap.Save(filePath, ImageFormat.Png);
        }

        //public static void CaptureScreen()
        //{
        //    // Get the bounds of the virtual screen
        //    Rectangle bounds = Screen.GetBounds(Point.Empty);
        //    // Create a bitmap with the size of the screen
        //    Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);

        //    // Create a graphics object from the bitmap
        //    using (Graphics graphics = Graphics.FromImage(bitmap))
        //    {
        //        // Copy the screen image to the graphics object
        //        graphics.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
        //    }

        //    // Save the bitmap to a file
        //    //string filePath = String.Format((string)ConfigurationManager.AppSettings["LogPath"],
        //    //    DateTime.Now.Date.ToString("dd.MM.yyyy"), "ScreenCapture_" + DateTime.Now.ToString("hh_mm_ss"));
        //    string filePath = DiskManager.CapturedImageDesktopPathandFilename();
        //    bitmap.Save(filePath, ImageFormat.Png);
        //}

        public static Bitmap CaptureAllScreens()
        {
            Rectangle bounds = Rectangle.Empty;
            foreach (Screen screen in Screen.AllScreens)
            {
                bounds = Rectangle.Union(bounds, screen.Bounds);
            }

            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics graphics = Graphics.FromImage(screenshot))
            {
                graphics.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
                AddDateTimeStamp(graphics);
            }

            // Save the captured image to a file
            //string savePath = ReadSavePathFromConfigFile();
            //string fileName = "all_screens_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
            //string fullPath = Path.Combine(savePath, fileName);
            //screenshot.Save(fullPath, ImageFormat.Png);

            return screenshot;
        }

        private static void AddDateTimeStamp(Graphics graphics)
        {
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Font font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold, GraphicsUnit.Pixel);
            Brush brush = Brushes.White;
            int x = (int)graphics.VisibleClipBounds.Width - 160;
            int y = 10;
            Point position = new Point(x, y);
            graphics.DrawString(timeStamp, font, brush, position);
        }

        public static Bitmap CaptureScreen()
        {
            // Get the bounds of the virtual screen
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            // Create a bitmap with the size of the screen
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Copy the screen image to the graphics object
                graphics.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
            }

            // Save the bitmap to a file
            //string filePath = String.Format((string)ConfigurationManager.AppSettings["LogPath"],
            //    DateTime.Now.Date.ToString("dd.MM.yyyy"), "ScreenCapture_" + DateTime.Now.ToString("hh_mm_ss"));
            string filePath = DiskManager.CapturedImageDesktopPathandFilename();
            return bitmap;
        }
        internal static class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern IntPtr GetForegroundWindow();

            [DllImport("user32.dll")]
            public static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

            [DllImport("user32.dll")]
            public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, uint nFlags);

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int Left;
                public int Top;
                public int Right;
                public int Bottom;

                public int Width => Right - Left;
                public int Height => Bottom - Top;
            }
        }
    }
}
