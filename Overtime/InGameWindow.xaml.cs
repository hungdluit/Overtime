using System.Windows;

namespace Overtime
{
    /// <summary>
    /// Interaction logic for InGameWindow.xaml
    /// </summary>
    public partial class InGameWindow : Window
    {
        public InGameWindow()
        {
            InitializeComponent();
        }

        public void Start()
        {
            Stopwatch.Start();
        }

        public void Stop()
        {
            Stopwatch.Stop();
            Logger.LogGameSession(Stopwatch.StartTime, Stopwatch.EndTime);
            Close();
        }
    }
}