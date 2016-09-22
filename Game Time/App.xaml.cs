using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
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
            Wmi = new WmiEvent();
            Wmi.GameStarted += OnGameStarted;
            Wmi.GameStopped += OnGameStopped;
            Wmi.Start();

            taskbarIcon = (TaskbarIcon) FindResource("MyNotifyIcon");
            //Game_Time.MainWindow window = new MainWindow();
            //window.Show();
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
    }
}