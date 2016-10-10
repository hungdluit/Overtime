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

namespace Game_Time
{
    /// <summary>
    /// Interaction logic for LogListPage.xaml
    /// </summary>
    public partial class LogListPage : Page
    {
        public IEnumerable<GameSession> GameSessions { get; set; }

        public LogListPage()
        {
            InitializeComponent();
            LoadGameSessions();

            LogListBox.ItemsSource = GameSessions;
        }

        private void LoadGameSessions() => GameSessions = Logger.ReadLog();
    }
}