using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WorkTracker
{
    static class LastInputHelper
    {
        /// <remarks>http://www.pinvoke.net/default.aspx/user32.GetLastInputInfo</remarks>
        [DllImport("User32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hwnd, StringBuilder ss, int count);
        struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        public static TimeSpan GetIdleTime()
        {
            var plii = new LASTINPUTINFO { cbSize = (uint)Marshal.SizeOf(typeof(LASTINPUTINFO)) };

            if (!GetLastInputInfo(ref plii)) return TimeSpan.Zero;

            long up = (uint)Environment.TickCount;
            long last = plii.dwTime;
            var idle = up - last;

            // if idle is negative, it means uptime rolled over and dwTime did not and we need to adjust
            if (idle < 0) idle += uint.MaxValue;

            return TimeSpan.FromMilliseconds(idle);
        }
    }
}
