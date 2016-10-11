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

namespace Overtime
{
    /// <summary>
    /// Interaction logic for BalloonControl.xaml
    /// </summary>
    public partial class BalloonControl : UserControl
    {
        public bool Starting { get; set; }

        public BalloonControl()
        {
            InitializeComponent();
        }

        public BalloonControl(bool started)
        {
            InitializeComponent();
            Starting = started;
            if (Starting)
            {
                MessageText.Text = "Game Launched";
            }
            else
            {
                MessageText.Text = "Game Stopped";
            }
        }
    }
}