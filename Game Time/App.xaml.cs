﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Media;
using Hardcodet.Wpf.TaskbarNotification;
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
        private StopwatchWindow stopwatchWindow = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            CreateDefaultSettings();
            Wmi = new WmiEvent();
            Wmi.GameStarted += OnGameStarted;
            Wmi.GameStopped += OnGameStopped;
            Wmi.Start();

            taskbarIcon = (TaskbarIcon) FindResource("MyNotifyIcon");
            MainWindow window = new MainWindow();
            window.Show();
            foreach (KeyValuePair<DateTime, TimeSpan> pair in Logger.GetHoursPerDay())
            {
                Console.WriteLine($"{pair.Key.ToShortDateString()} - {pair.Value.ToString("hh'h'mm'm'ss's'")}");
            }
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
                        if (stopwatchWindow == null)
                        {
                            stopwatchWindow = new StopwatchWindow();
                            Screen screen = Screen.AllScreens[1]; //Get second screen
                            Rectangle area = screen.WorkingArea;
                            stopwatchWindow.Top = area.Top;
                            stopwatchWindow.Left = area.Left + ( area.Width / 2 );
                            stopwatchWindow.Show();
                            stopwatchWindow.Start();
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
                        if (stopwatchWindow != null)
                        {
                            stopwatchWindow.Stop();
                            stopwatchWindow = null;
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
    }
}