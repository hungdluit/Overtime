using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;
using Overtime.Properties;
using Application = System.Windows.Application;

namespace Overtime
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon taskbarIcon;
        public WmiEvent Wmi { get; set; }
        private InGameWindow inGameWindow = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            CreateDefaultSettings();
            Wmi = new WmiEvent();
            Wmi.GameStarted += OnGameStarted;
            Wmi.GameStopped += OnGameStopped;
            Wmi.Start();

            taskbarIcon = (TaskbarIcon) FindResource("MyNotifyIcon");
            //MainWindow window = new MainWindow();
            //window.Show();
            foreach (KeyValuePair<DateTime, TimeSpan> pair in Logger.GetTotalPlayHoursByDay())
            {
                Console.WriteLine($"{pair.Key.ToShortDateString()} - {pair.Value.ToString("hh'h'mm'm'ss's'")}");
            }
            //StartWithWindows(); //Register to start on boot
        }

        protected override void OnExit(ExitEventArgs e)
        {
            taskbarIcon.Dispose();
            Wmi.Stop();
            base.OnExit(e);
        }

        public void OnGameStarted(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(
                (Action)
                    delegate
                    {
                        taskbarIcon.ShowCustomBalloon(
                            new BalloonControl(true),
                            PopupAnimation.Fade, 4000);
                        if (inGameWindow == null)
                        {
                            inGameWindow = new InGameWindow();
                            Screen screen = Screen.AllScreens[1]; //Get second screen
                            Rectangle area = screen.WorkingArea;
                            inGameWindow.Top = area.Top;
                            inGameWindow.Left = area.Left + ( area.Width / 2 );
                            inGameWindow.Show();
                            inGameWindow.Start();
                        }
                    });
        }

        public void OnGameStopped(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(
                (Action)
                    delegate
                    {
                        taskbarIcon.ShowCustomBalloon(
                            new BalloonControl(false),
                            PopupAnimation.Fade, 4000);
                        if (inGameWindow != null)
                        {
                            inGameWindow.Stop();
                            inGameWindow = null;
                        }
                    });
        }

        private void CreateDefaultSettings()
        {
            //Checks that user default settings were initialized
            if (!Settings.Default.DefaultsCreated)
            {
                string documents = Path.Combine(
                    System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Overtime");
                    //Specify our user-log folder
                Directory.CreateDirectory(documents); //create the directory, if needed
                Settings.Default.LogFileLocation = documents; //save this directory in our settings
                Settings.Default.DefaultsCreated = true;
                    //set flag, so that this stuff doesn't happen in subsequent launches
                Settings.Default.Save(); //persist changes
            }
            //Check that log file exists, create blank if not
            string logPath = Path.Combine(Settings.Default.LogFileLocation, Settings.Default.LogFileName);
            if (!File.Exists(logPath))
            {
                File.Create(logPath);
            }
        }

        public void StartWithWindows(bool enabled = true)
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (enabled && rkApp.GetValue("Overtime") == null)
            {
                rkApp.SetValue("Overtime", Assembly.GetExecutingAssembly().Location);
            }
            if (!enabled && rkApp.GetValue("Overtime") != null)
            {
                rkApp.DeleteValue("Overtime", false);
            }
        }
    }
}