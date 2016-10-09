using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Time
{
    public class GameSession
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public TimeSpan Duration
        {
            get { return End - Start; }
        }

        public GameSession(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public GameSession() { }
    }
}
