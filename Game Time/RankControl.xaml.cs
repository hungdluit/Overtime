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
using Overtime.Properties;

namespace Overtime
{
    /// <summary>
    /// Interaction logic for RankControl.xaml
    /// </summary>
    public partial class RankControl : UserControl
    {
        public RankTracker RankTracker { get; set; }
        public RankControl()
        {
            InitializeComponent();
            RankTracker = new RankTracker(Settings.Default.BattleTag);
            RankTracker.Start();
            RankText.DataContext = RankTracker;
        }

    }
}
