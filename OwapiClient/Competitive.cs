namespace OwapiClient
{
    public class Competitive
    {
        public double Ana { get; set; }
        public double Bastion { get; set; }
        public double Dva { get; set; }
        public int Genji { get; set; }
        public int Hanzo { get; set; }
        public double Junkrat { get; set; }
        public double Lucio { get; set; }
        public int Mccree { get; set; }
        public double Mei { get; set; }
        public double Mercy { get; set; }
        public double Pharah { get; set; }
        public double Reaper { get; set; }
        public double Reinhardt { get; set; }
        public double Roadhog { get; set; }
        public double Soldier76 { get; set; }
        public double Symmetra { get; set; }
        public double Torbjorn { get; set; }
        public int Tracer { get; set; }
        public int Widowmaker { get; set; }
        public double Winston { get; set; }
        public double Zarya { get; set; }
        public double Zenyatta { get; set; }

        public static Competitive operator -(Competitive a, Competitive b)
        {
            Competitive res = new Competitive
            {
                Ana = a.Ana = b.Ana,
                Bastion = a.Bastion - b.Bastion,
                Dva = a.Dva - b.Dva,

                Genji = a.Genji - b.Genji,
                Hanzo = a.Hanzo - b.Hanzo,
                Junkrat = a.Junkrat - b.Junkrat,
                Lucio = a.Lucio - b.Lucio,
                Mccree = a.Mccree - b.Mccree,
                Mei = a.Mei - b.Mei,
                Mercy = a.Mercy - b.Mercy,
                Pharah = a.Pharah - b.Pharah,
                Reaper = a.Reaper - b.Reaper,
                Reinhardt = a.Reinhardt - b.Reinhardt,
                Roadhog = a.Roadhog - b.Roadhog,
                Soldier76 = a.Soldier76 - b.Soldier76,
                Symmetra = a.Symmetra - b.Symmetra,
                Torbjorn = a.Torbjorn - b.Torbjorn,
                Tracer = a.Tracer - b.Tracer,
                Widowmaker = a.Widowmaker - b.Widowmaker,
                Winston = a.Winston - b.Winston,
                Zarya = a.Zarya - b.Zarya,
                Zenyatta = a.Zenyatta - b.Zenyatta
            };
            return res;
        }
    }
}