using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinTracker1.Helper
{
    public class ScreenManager
    {
        public static void CaptureActiveWindow()
        {
            // Get the handle of the active window
            IntPtr handle = NativeMethods.GetForegroundWindow();

            // Capture the window image
            CaptureWindow(handle);
        }

        public static void CaptureDesktop()
        {
            // Get the bounds of the virtual screen
            Rectangle bounds = Screen.GetBounds(Point.Empty);

            // Capture the desktop image
            CaptureScreen(bounds);
        }

        public static void CaptureWindow(IntPtr handle)
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

        private static void CaptureScreen(Rectangle bounds)
        {
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
            bitmap.Save(filePath, ImageFormat.Png);
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
