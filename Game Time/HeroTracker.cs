using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OwapiClient;

namespace Overtime
{
    public class HeroTracker
    {
        public string BattleTag { get; set; }
        public string TrackedHero { get; set; }
        public double TimeTracked { get; set; }

        public Playtime StartingPlaytime { get; private set; }
        public Playtime PreviousTickPlaytime { get; set; }

        public DateTime TickTimestamp { get; set; }
        public DateTime PreviousTickTimestamp { get; set; }

        public HeroTracker(string battletag)
        {
            BattleTag = battletag;
            StartingPlaytime = GetPlaytime();
            PreviousTickPlaytime = StartingPlaytime;
            TickTimestamp = DateTime.Now;
            PreviousTickTimestamp = TickTimestamp;
        }

        private Playtime GetPlaytime() => new OClient().GetHeroPlaytimes(BattleTag);

        public Playtime GetPlaytimeDelta()
        {
            Playtime nextTick = GetPlaytime();
            PreviousTickTimestamp = TickTimestamp;
            TickTimestamp = DateTime.Now;
            Playtime delta = nextTick - PreviousTickPlaytime;
            PreviousTickPlaytime = nextTick;
            return delta;
        }
        //todo: Make a temporary log for each tick update include both timestamp and playtime- when Overwatch is closed, the app will sum these up into contiguous stretches and write to the official logfile
        //todo: Get this set to start, and run at an arbitrary tick rate resolution
    }
}
