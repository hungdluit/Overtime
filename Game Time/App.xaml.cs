using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Game_Time.Properties;
using Hardcodet.Wpf.TaskbarNotification;

namespace Game_Time
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon taskbarIcon;
        public WmiEvent Wmi { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            CreateDefaultSettings();
            Wmi = new WmiEvent();
            Wmi.GameStarted += OnGameStarted;
            Wmi.GameStopped += OnGameStopped;
            Wmi.Start();

            taskbarIcon = (TaskbarIcon) FindResource("MyNotifyIcon");
            Game_Time.MainWindow window = new MainWindow();
            window.Show();
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
               });
        }

        private void CreateDefaultSettings()
        {
            //Checks that user default settings were initialized
            if (!Settings.Default.DefaultsCreated)
            {
                string documents = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Game Time"); //Specify our user-log folder
                Directory.CreateDirectory(documents); //create the directory, if needed
                Settings.Default.LogFileLocation = documents; //save this directory in our settings
                Settings.Default.DefaultsCreated = true; //set flag, so that this stuff doesn't happen in subsequent launches
                Settings.Default.Save(); //persist changes
            }
            //Check that log file exists, create blank if not
            string logPath = Path.Combine(Settings.Default.LogFileLocation, "GameLog.csv");
            if (!File.Exists(logPath))
            {
                File.Create(logPath);
            }
        }
    }
}