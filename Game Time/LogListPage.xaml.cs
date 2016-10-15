using System.Collections.Generic;
using System.Windows.Controls;

namespace Overtime
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