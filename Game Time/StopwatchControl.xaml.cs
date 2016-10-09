using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game_Time
{
    /// <summary>
    /// Interaction logic for StopwatchControl.xaml
    /// </summary>
    public partial class StopwatchControl : UserControl, INotifyPropertyChanged
    {
        private Stopwatch stopwatch;
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        private Timer timer; //used to fire propertychanged on TimeString every second

        public TimeSpan ElapsedTime
        {
            get
            {
                if (stopwatch == null) return TimeSpan.Zero; //if stopwatch is null, play time is 0
                return TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds); //just get those milliseconds
            }
        }

        public string TimeString
        {
            get { return ElapsedTime.ToString("hh':'mm':'ss"); } //just format and return
        }

        public StopwatchControl()
        {
            InitializeComponent();
        }

        public void Start()
        {
            EndTime = DateTime.Now; //just to blank out any previous values
            StartTime = DateTime.Now; //capture start time
            stopwatch = Stopwatch.StartNew(); //start stopwatch
            timer =
                new Timer((s) => OnPropertyChanged(this, new PropertyChangedEventArgs("TimeString")),
                    null, 1000, 1000); //set and start a timer to fire TimeString's PropertyChanged event every 1000ms
        }

        public void Stop()
        {
            EndTime = DateTime.Now; //capture end time
            stopwatch.Stop(); //stop stopwatch
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(sender, e);
            }
        }
    }
}