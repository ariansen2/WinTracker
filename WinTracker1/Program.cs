using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;

namespace WinTracker1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string LogPath = ConfigurationManager.AppSettings["LogPath"].ToString();
            InitializeLogging(LogPath);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(LogPath + "\\" + ConfigurationManager.AppSettings["LogFileName"].ToString())
                .CreateLogger();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void InitializeLogging(string LogPath)
        {
            string today = DateTime.Now.ToString("dd.MM.yyyy");
            try
            {
                //Er loggemappen opprettet
                if (!Directory.Exists(LogPath))
                {
                    //Nei - opprett loggemappe
                    Directory.CreateDirectory(LogPath);
                    
                    //opprett loggemappe for i dag
                    Directory.CreateDirectory(LogPath + "\\" + today);
                    Directory.CreateDirectory(LogPath + "\\" + today + "\\Desktop");
                    Directory.CreateDirectory(LogPath + "\\" + today + "\\Window");
                    Directory.CreateDirectory(LogPath + "\\" + today + "\\AllScreens");
                }
                else
                { // Hovedloggemappe er opprettet - må sjekke om mappe for i dag er opprettet
                    if (!Directory.Exists(LogPath + "\\" + today))
                    { //mappe for i dag er ikke opprettet
                        Directory.CreateDirectory(LogPath + "//" + today);
                        Directory.CreateDirectory(LogPath + "\\" + today + "\\Desktop");
                        Directory.CreateDirectory(LogPath + "\\" + today + "\\Window");
                        Directory.CreateDirectory(LogPath + "\\" + today + "\\AllScreens");
                    }
                }
            } //oppretter backup logging
            catch (Exception ex)
            {
              
                LogPath = ConfigurationManager.AppSettings["BackupLogPath"].ToString();
                if (!Directory.Exists(LogPath))
                {
                    //Nei - opprett loggemappe
                    Directory.CreateDirectory(LogPath);

                    //opprett loggemappe for i dag
                    Directory.CreateDirectory(LogPath + "\\" + today);
                    Directory.CreateDirectory(LogPath + "\\" + today + "\\Desktop");
                    Directory.CreateDirectory(LogPath + "\\" + today + "\\Window");
                }
                else
                {
                    // Hovedloggemappe er opprettet - må sjekke om mappe for i dag er opprettet
                    if (!Directory.Exists(LogPath + "\\" + today))
                    {
                        //mappe for i dag er ikke opprettet
                        Directory.CreateDirectory(LogPath + "//" + today);
                        Directory.CreateDirectory(LogPath + "\\" + today + "\\Desktop");
                        Directory.CreateDirectory(LogPath + "\\" + today + "\\Window");
                    }

                    //Console.WriteLine(ex.Message);
                   
                }
            }
        }
    }
}
