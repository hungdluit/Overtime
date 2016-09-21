using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;

namespace Game_Time
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon taskbarIcon;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            taskbarIcon = (TaskbarIcon) FindResource("MyNotifyIcon");
            //Game_Time.MainWindow window = new MainWindow();
            //window.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            taskbarIcon.Dispose();
            base.OnExit(e);
        }
    }
}
