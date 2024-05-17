using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Treenity_AI_Scraper.Services.Cipher;
using Treenity_AI_Scraper.Models.Database;

namespace Treenity_AI_Scraper.Services
{
    internal class OnStartTicketProcessHostedService(TicketProcessorService eventbus, OnlineStoreService OnlineStoreService, ProgramDbContext db, TreenityCryptoProvider provider, IHostApplicationLifetime lifetime, ILogger<OnStartTicketProcessHostedService> logger, IServiceProvider serviceProvider) : IHostedService, IHostedLifecycleService
    {
        FileSystemWatcher fsw = new(Environment.CurrentDirectory);
        bool isWriting = false;
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //AnswerService.MigrateJson2Db("answer.json");
            //AnswerService.SaveToJson("answer.json");
            await Task.Factory.StartNew(RunMain);
            //add event handler to fsw and start it
            fsw.Changed += async (sender, e) =>
            {
                if (e.Name == "addList.txt")
                {
                    if (isWriting)
                    {
                        isWriting = false;
                        return;
                    }
                    //use readonly method to open the txt tile
                    var tickets = await File.ReadAllLinesAsync("addList.txt");
                    if (tickets.Length == 0) return;
                    foreach (var tic in tickets)
                    {
                        var kv = tic.Split(' ');
                        EntityStore? entityStore = db.Entities.FirstOrDefault(e => e.username == kv[0]);
                        if (entityStore == null)
                        {
                            entityStore = new(kv[0], kv[1]);
                            db.Entities.Add(entityStore);
                        }
                        else
                        {
                            if (entityStore.password != kv[1])
                            {
                                entityStore.password = kv[1];
                                entityStore.cookie = null;
                                entityStore.CookieExpired = null;
                            }
                        }
                        TicketStore ticketStore = new() { entityStore = entityStore, channel = kv[2], finished = false, orderTime = DateTime.Now };
                        db.Tickets.Add(ticketStore);
                        await db.SaveChangesAsync();
                    }
                    isWriting = true;
                    await File.WriteAllTextAsync("addList.txt", string.Empty);
                    isWriting = false;
                    foreach (var item in await db.Tickets.Where(t => t.finished == false).Include(t => t.entityStore).ToListAsync())
                    {
                        await eventbus.ProduceTicket(item);
                    }
                }
            };
            fsw.EnableRaisingEvents = true;
        }

        public async Task StartedAsync(CancellationToken cancellationToken)
        {

        }

        public async Task StartingAsync(CancellationToken cancellationToken)
        {

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            return;
        }

        public async Task StoppedAsync(CancellationToken cancellationToken)
        {

        }

        public async Task StoppingAsync(CancellationToken cancellationToken)
        {

        }

        private async Task RunMain()
        {
            /*var onlineTickets = await OnlineStoreService.GetOnlineTickets();
            foreach (var tic in onlineTickets)
            {
                EntityStore? entityStore = db.Entities.FirstOrDefault(e => e.username == tic.userName);
                if (entityStore == null)
                {
                    entityStore = new(tic.userName, tic.password);
                    db.Entities.Add(entityStore);
                }
                else
                {
                    if (entityStore.password != tic.password)
                    {
                        entityStore.password = tic.password;
                        entityStore.cookie = null;
                        entityStore.CookieExpired = null;
                    }
                }
                TicketStore ticketStore = new() { entityStore = entityStore, channel = tic.channel, finished = false, orderTime = tic.orderTime!.Value };
                db.Tickets.Add(ticketStore);
                await db.SaveChangesAsync();
            }*/
            var tickets = await File.ReadAllLinesAsync("addList.txt");
            foreach (var tic in tickets)
            {
                var kv = tic.Split(' ');
                EntityStore? entityStore = db.Entities.FirstOrDefault(e => e.username == kv[0]);
                if (entityStore == null)
                {
                    entityStore = new(kv[0], kv[1]);
                    db.Entities.Add(entityStore);
                }
                else
                {
                    if (entityStore.password != kv[1])
                    {
                        entityStore.password = kv[1];
                        entityStore.cookie = null;
                        entityStore.CookieExpired = null;
                    }
                }
                TicketStore ticketStore = new() { entityStore = entityStore, channel = kv[2], finished = false, orderTime = DateTime.Now };
                db.Tickets.Add(ticketStore);
                await db.SaveChangesAsync();
            }

            await File.WriteAllTextAsync("addList.txt", string.Empty);

            /* Dictionary<EntityStore, Queue<TicketStore>> orderedQueue = [];
             foreach (var item in await db.Tickets.Where(t => t.finished == false).Include(t => t.entityStore).ToListAsync())
             {
                 if (!orderedQueue.ContainsKey(item.entityStore)) orderedQueue.Add(item.entityStore, new());
                 orderedQueue[item.entityStore].Enqueue(item);
             }
             await Parallel.ForEachAsync(orderedQueue, new ParallelOptions()
             {
                 MaxDegreeOfParallelism = 3
             },
             async (ticketsPerEntity, _) =>
             {
                 while (ticketsPerEntity.Value.Count > 0)
                 {
                     var ticket = ticketsPerEntity.Value.Dequeue();
                     logger.LogInformation(ticket.entityStore.username + ":" + ticket.channel);
                     WorkService service = serviceProvider.GetRequiredService<WorkService>();
                     await service.Run(db,ticket);
                     await db.SaveChangesAsync();
                 }
             }); */

            foreach (var item in await db.Tickets.Where(t => t.finished == false).Include(t => t.entityStore).ToListAsync())
            {
                await eventbus.ProduceTicket(item);
            }

            //if (File.Exists("ScraperDB.bak")) File.Delete("ScraperDB.bak");
            //File.Copy("ScraperDB", "ScraperDB.bak");

            //logger.LogInformation("EXIT");
            //lifetime.StopApplication();


        }
    }
}
