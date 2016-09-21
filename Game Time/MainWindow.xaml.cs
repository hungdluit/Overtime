using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Management;

namespace Game_Time
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WmiEvent Wmi { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.Closed += OnClosed;
            Wmi = new WmiEvent();
            //Wmi.Start();
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            Wmi.Stop();
        }
    }
}
