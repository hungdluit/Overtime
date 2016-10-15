using System;
using System.ComponentModel;
using System.Timers;
using OwapiClient;

namespace Overtime
{
    public class RankTracker:INotifyPropertyChanged
    {
        public string BattleTag { get; set; }
        public int Rank { get; set; }
        private Timer timer;

        public bool Running => timer.Enabled;

        public RankTracker(string battletag)
        {
            BattleTag = battletag;
            timer = new Timer(1000);
            timer.Elapsed += (sender, args) => { CheckAndLog(); };
        }

        public void CheckAndLog()
        {
            int rank = new BlizzScraper(BattleTag, "us").GetRank();
            Rank = rank; //Just set the property, don't waste "comp"s
            int? lastRank = Logger.ReadLastRank();
            if (!lastRank.HasValue || rank != lastRank.Value)
            {
                //Log it
                Logger.LogRank(DateTime.Now, rank);
                //Trigger property event, for UI
                OnPropertyChanged(this, new PropertyChangedEventArgs("Rank"));
            }
        }

        public void Start()
        {
            CheckAndLog();
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
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
