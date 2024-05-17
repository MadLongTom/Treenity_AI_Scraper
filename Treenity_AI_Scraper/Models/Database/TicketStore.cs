namespace Treenity_AI_Scraper.Models.Database
{
    public class TicketStore
    {
        public virtual long Id { get; set; }
        public virtual EntityStore entityStore { get; set; }
        public virtual string channel { get; set; }
        public virtual bool finished { get; set; }
        public virtual DateTime orderTime { get; set; }
        public virtual DateTime? startTime { get; set; }
        public virtual DateTime finishTime { get; set; }
    }
}
