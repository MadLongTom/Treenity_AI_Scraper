namespace Treenity_AI_Scraper.Models.Runtime
{
    public class OnlineTicket
    {
        public int? id { get; set; }
        public string userName { get; set; }
        public string token { get; set; }
        public List<Ticket> tickets { get; set; }
        public int? producedPoint { get; set; }
        public class Ticket
        {
            public int? id { get; set; }
            public string channel { get; set; }
            public string userName { get; set; }
            public string password { get; set; }
            public DateTime? orderTime { get; set; }
            public List<DateTime?> getRecord { get; set; }
        }
    }
}
