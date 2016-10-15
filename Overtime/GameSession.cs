using System;

namespace Overtime
{
    public class GameSession
    {
        public DateTime Start { get; set; }

        public string StartTimeString { get { return Start.ToShortTimeString(); } }

        public string StartDateString { get { return Start.ToShortDateString(); } }
        public DateTime End { get; set; }
        public string EndTimeString { get { return End.ToShortTimeString(); } }

        public TimeSpan Duration
        {
            get { return End - Start; }
        }

        public string DurationString { get { return Duration.ToString("hh' hours 'mm' minutes 'ss' seconds '"); } }

        public GameSession(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public GameSession() { }
    }
}
