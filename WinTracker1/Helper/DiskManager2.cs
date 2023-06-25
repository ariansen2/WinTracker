using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinTracker1.Helper
{
    public static class DiskManager2
    {     
        public static void SaveImage(String imagetype, Bitmap image)
        {
            string _imagestoragepath = ConfigurationManager.AppSettings["LogPath"];
            string _backupimagestoragepath = ConfigurationManager.AppSettings["BackupLogPath"];
            string storagepath = string.Empty;
            string today = DateTime.Now.ToString("dd.MM.yyyy");
            string timestamp = DateTime.Now.ToString("hh-MM-ss");

            try
            {
                image.Save(_imagestoragepath + $"\\{today}\\{imagetype}\\{timestamp}.png",ImageFormat.Png);

            }
            catch (Exception e)
            {
                image.Save(_backupimagestoragepath + $"\\{today}\\{imagetype}\\{timestamp}.png", ImageFormat.Png);
            }
        }
    }
}
