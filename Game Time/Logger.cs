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
            if(end - start > TimeSpan.FromMinutes(1))
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
    }
}
