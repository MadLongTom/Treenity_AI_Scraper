namespace Treenity_AI_Scraper.Models.Database
{
    public record Channel(long Id,List<long>? courseIds)
    {
        public virtual long Id { get; set; } = Id;
        public virtual List<long> coursdIds { get; set; } = courseIds ?? [];
    }
}
