using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinTracker1.Helper
{
    public class DiskManager
    {

        public static string ImageStoragePath()
        { 
            string _imagestoragepath = ConfigurationManager.AppSettings["LogPath"];
            string _backupimagestoragepath = ConfigurationManager.AppSettings["BackupLogPath"];
            string storagepath = string.Empty;
            try
            {
                if (Directory.Exists(_imagestoragepath + "\\" + DateTime.Now.ToString("dd.MM.yyyy")))
                {
                    storagepath = _imagestoragepath + "\\" + DateTime.Now.ToString("dd.MM.yyyy");
                    Directory.CreateDirectory(storagepath + "\\Desktop");
                    Directory.CreateDirectory(storagepath + "\\Window");
                }
            }
            catch (Exception ex)
            {
                if (Directory.Exists(_backupimagestoragepath + "\\" + DateTime.Now.ToString("dd.MM.yyyy")))
                {
                    storagepath = _backupimagestoragepath + "\\" + DateTime.Now.ToString("dd.MM.yyyy");


                }
            }
            return storagepath;
        }

        public static string CapturedImageDesktopPathandFilename()
        {
            string _imagestoragepath = ConfigurationManager.AppSettings["LogPath"];
            string _backupimagestoragepath = ConfigurationManager.AppSettings["BackupLogPath"];
            string storagepath = string.Empty;
            try
            {
                if (Directory.Exists(_imagestoragepath + "\\" + DateTime.Now.ToString("dd.MM.yyyy")))
                {
                    storagepath = _imagestoragepath + "\\" + DateTime.Now.ToString("dd.MM.yyyy") + "\\Desktop\\" + DateTime.Now.ToString("HH_mm_ss") + ".png";
                }
            }
            catch (Exception ex)
            {
                if (Directory.Exists(_backupimagestoragepath + "\\" + DateTime.Now.ToString("dd.MM.yyyy")))
                {
                    storagepath = _backupimagestoragepath + "\\" + DateTime.Now.ToString("dd.MM.yyyy") + "\\Desktop\\" + DateTime.Now.ToString("HH_mm_ss") + ".png";
                }
            }
            return storagepath;
        }
        public static string CapturedImageWindowPathandFilename()
        {
            string _imagestoragepath = ConfigurationManager.AppSettings["LogPath"];
            string _backupimagestoragepath = ConfigurationManager.AppSettings["BackupLogPath"];
            string storagepath = string.Empty;
            try
            {
                if (Directory.Exists(_imagestoragepath + "\\" + DateTime.Now.ToString("dd.MM.yyyy")))
                {
                    storagepath = _imagestoragepath + "\\" + DateTime.Now.ToString("dd.MM.yyyy") + "\\Window\\" + DateTime.Now.ToString("HH_mm_ss") + ".png";
                }
            }
            catch (Exception ex)
            {
                if (Directory.Exists(_backupimagestoragepath + "\\" + DateTime.Now.ToString("dd.MM.yyyy")))
                {
                    storagepath = _backupimagestoragepath + "\\" + DateTime.Now.ToString("dd.MM.yyyy") + "\\Window\\" + DateTime.Now.ToString("HH_mm_ss") + ".png";
                }
            }
            return storagepath;
        }
    }
}
