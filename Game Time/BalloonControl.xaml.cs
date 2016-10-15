using System.Windows.Controls;

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