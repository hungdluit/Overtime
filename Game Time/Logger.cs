using System;
using System.Collections.Generic;
using System.IO;
using Game_Time.Properties;

namespace Game_Time
{
    public static class Logger
    {
        public static string LogPath
        {
            get { return Path.Combine(Settings.Default.LogFileLocation, Settings.Default.LogFileName); }
        }

        public static void LogGameSession(DateTime start, DateTime end)
        {
            //Only append log if the game was running for longer than 1 minute
            if (end - start > TimeSpan.FromMinutes(1))
                File.AppendAllText(LogPath, $"{start},{end}\r\n");
        }

        public static IEnumerable<GameSession> ReadLog()
        {
            foreach (string line in File.ReadAllLines(LogPath))
            {
                string[] dateStrings = line.Replace("\r\n", "").Split(',');
                yield return new GameSession(DateTime.Parse(dateStrings[0]), DateTime.Parse(dateStrings[1]));
            }
        }

        public static Dictionary<DateTime, TimeSpan> GetHoursPerDay()
        {
            Dictionary<DateTime, TimeSpan> results = new Dictionary<DateTime, TimeSpan>();
            foreach (GameSession session in ReadLog())
            {
                //Span ends on same day, timespan can just be added
                if (session.Start.Date == session.End.Date)
                {
                    if (results.ContainsKey(session.Start.Date))
                        results[session.Start.Date] += session.Duration;
                    else
                        results.Add(session.Start.Date, session.Duration);
                }

                //Span ends on different day (late-night gaming!) so more processing is needed
                //If someone leaves their computer running Overwatch for several days, this will still work (despite useless metrics)
                else
                {
                    //Cache starting datetime
                    DateTime current = session.Start;
                    //iterate arbitray number of days, including EndDate
                    while (current <= session.End)
                    {
                        DateTime next = current.Date.AddDays(1); //Set to 12AM, the next day
                        //Set to end time, if 'next' is past the end time on that day
                        if (next > session.End)
                            next = session.End;

                        TimeSpan firstDuration = next - current; //Get the difference

                        if (results.ContainsKey(current.Date))
                            results[current.Date] += firstDuration;
                        else
                            results.Add(current.Date, firstDuration);
                        current = next; //Continue loop
                    }
                }
            }
            return results;
        }
    }
}