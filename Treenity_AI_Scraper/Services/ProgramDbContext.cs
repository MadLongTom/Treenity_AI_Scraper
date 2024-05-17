using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Treenity_AI_Scraper.Models.Database;

namespace Treenity_AI_Scraper.Services
{
    internal class ProgramDbContext : DbContext
    {
        public DbSet<AppRuntimeConfig> AppRuntimeConfig { get; set; }
        public DbSet<EntityStore> Entities { get; set; }
        public DbSet<TicketStore> Tickets { get; set; }
        public DbSet<Answer> AnswerStores { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public ProgramDbContext(DbContextOptions options) : base(options)
        {
        }

        public ProgramDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(new SqlConnectionStringBuilder()
            {
                ConnectionString = "Server=.;Database=ScraperDB;Trusted_Connection=True;",
                TrustServerCertificate = true,
                MultipleActiveResultSets = true
            }.ToString())
                .EnableThreadSafetyChecks()
                .UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);

            /* optionsBuilder.UseSqlite(new SqliteConnectionStringBuilder()
             {
                 DataSource = "ScraperDB"
             }.ToString())*/

            //.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>().Property(c => c.questionId).ValueGeneratedNever();
            modelBuilder.Entity<Channel>().Property(c => c.Id).ValueGeneratedNever();
            modelBuilder.Entity<Channel>().HasData([
                new(1,[4000001593]),
                new(2,[4000001588,4000001589]),
                new(3,[4000001598,4000001629])
                ]);
            base.OnModelCreating(modelBuilder);
        }
    }
}
