namespace Treenity_AI_Scraper.Models.Database
{
    public record EntityStore(string username, string password)
    {
        public virtual string password { get; set; } = password;
        public virtual string? cookie { get; set; }
        public virtual DateTime? CookieExpired { get; set; }
        public virtual long Id { get; set; }
    }
}
