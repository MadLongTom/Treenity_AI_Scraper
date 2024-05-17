using System.ComponentModel.DataAnnotations.Schema;
using Treenity_AI_Scraper.Services.Cipher;
using Treenity_AI_Scraper.Models.Database;

namespace Treenity_AI_Scraper.Models.Runtime
{
    internal class Entity(EntityStore entityStore, string? uuid, TreenityCryptoProvider provider, HttpClient? client)
    {
        public long Id { get; set; }
        public EntityStore EntityStore { get; set; } = entityStore;
        [NotMapped]
        public TreenityCryptoProvider TreenityCryptoProvider { get; set; } = provider;
        [NotMapped]
        public HttpClient? client { get; set; } = client;
        public string? useruuid { get; set; } = uuid;
    }
}
