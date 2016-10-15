namespace OwapiClient
{
    public class Playtime
    {
        public Competitive Competitive { get; set; }
        public Quickplay Quickplay { get; set; }

        public static Playtime operator -(Playtime a, Playtime b)
        {
            return new Playtime
            {
                Competitive = a.Competitive-b.Competitive,
                Quickplay = a.Quickplay-b.Quickplay
            };
        }
    }
}