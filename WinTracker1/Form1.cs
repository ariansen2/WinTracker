using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Serilog;
using WinTracker1.Data;
using WinTracker1.Helper;
using WorkTracker.Core.Helper;

namespace WinTracker1
{
    public partial class Form1 : Form
    {
        //private string windowstitle ="";
        //private Stopwatch sw = new Stopwatch();

        private DispatcherTimer timer;
        private string previousWindowTitle;
        private Stopwatch stopwatch;
        private string currentWindowTitle;
        private int timeInWindowSetting;

        private Bitmap desktop;
        private Bitmap allscreens;
        private Bitmap window;


        public Form1()
        {
            Log.Information("Programmet starter kl. " + DateTime.Now.ToString("F"));
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Ticker;

            stopwatch = new Stopwatch();
            stopwatch.Start();

            previousWindowTitle = ActiveWindow.ActiveWindowTitle();

            //First open window
            window = ScreenManager2.CaptureWindow();
            desktop = ScreenManager2.CaptureScreen();
            allscreens = ScreenManager2.CaptureAllScreens();
            DiskManager2.SaveImage("Desktop", desktop);
            DiskManager2.SaveImage("Window", window);
            DiskManager2.SaveImage("AllScreens", allscreens);
            timer.Start();
        }

        private void Timer_Ticker(object sender, EventArgs e)
        {
            lblWatch.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

            Console.WriteLine("Start 1 - previous er " + previousWindowTitle);
            Console.WriteLine("Start 1 - current er " + currentWindowTitle);

            currentWindowTitle = ActiveWindow.ActiveWindowTitle();

            Console.WriteLine("Start 2 - previous er " + previousWindowTitle);
            Console.WriteLine("Start 2 - current er " + currentWindowTitle);

            timeInWindowSetting = GetTimeInWindowSetting();

            if (previousWindowTitle != currentWindowTitle && stopwatch.Elapsed.Seconds > timeInWindowSetting)
            {
                // User has been in a window for more than the specified time and switched to another window
                // - needs to be logged

                DataHelper dh = new DataHelper();
                DateTime startStamp = DateTime.Now;
                DateTime stopStamp = DateTime.Now;
                TimeSpan elapsedTime = stopwatch.Elapsed;

                //save all captured images
                DiskManager2.SaveImage("Desktop", desktop);
                DiskManager2.SaveImage("Window", window);
                DiskManager2.SaveImage("AllScreens", allscreens);
                // Save the window usage
                dh.SaveScreenUsed(previousWindowTitle, startStamp, stopStamp, elapsedTime);

                stopwatch.Reset();
                stopwatch.Start();
                Console.WriteLine("Slutten 1 - previous er " +previousWindowTitle  );
                Console.WriteLine("Slutten 1 - current er " + currentWindowTitle);



                Console.WriteLine("Slutten 2 - previous er " + previousWindowTitle);
                Console.WriteLine("Slutten 2 - current er " + currentWindowTitle);
            }
            window = ScreenManager2.CaptureWindow();
            desktop = ScreenManager2.CaptureScreen();
            allscreens = ScreenManager2.CaptureAllScreens();

            Log.Debug("Current window is " + currentWindowTitle);
            Console.WriteLine("Current window is " + currentWindowTitle);

            if (stopwatch.Elapsed.Seconds > 30)
            {
                Log.Debug("Current window is being logged");
                Console.WriteLine("Current window is being logged");
            }
            previousWindowTitle = currentWindowTitle;
        }

        private int GetTimeInWindowSetting()
        {
            int timeInWindow;
            if (!int.TryParse(ConfigurationManager.AppSettings["TimeInWindow"], out timeInWindow))
            {
                // Failed to parse the setting, handle the error or provide a default value
                timeInWindow = 30; // Default to 30 seconds if the setting is not configured correctly
            }

            return timeInWindow;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            //sw.Stop();
            
            Log.Information("Programmet er avsluttet.");
            Application.Exit();
        }
    }
}
