
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Treenity_AI_Scraper.Models.Database;

namespace Treenity_AI_Scraper.Services
{
    internal class SignalRHostedService(TicketProcessorService eventbus,ProgramDbContext db, ILogger<SignalRHostedService> logger) : IHostedService, IHostedLifecycleService
    {
        HubConnection connection { get; set; }
        public bool Connected => connection.State == HubConnectionState.Connected;
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://your_websocket_service/TreenityHub")
                .WithAutomaticReconnect()
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                })
                .Build();

            connection.Closed += async (error) =>
            {
                if(error != null)
                {
                    logger.LogError(error,"Error while establishing connection with order service");
                }
                else
                {
                    logger.LogInformation("Connection with order service closed intentionally");
                }
            };

            connection.On("AddOrder", async (string username, string password,string channel) =>
            {
                logger.LogInformation($"Received AddOrder:{username}:{password}:{channel}");
                EntityStore? entityStore = db.Entities.FirstOrDefault(e => e.username == username);
                if (entityStore == null)
                {
                    entityStore = new(username, password);
                    db.Entities.Add(entityStore);
                }
                else
                {
                    if (entityStore.password != password)
                    {
                        entityStore.password = password;
                        entityStore.cookie = null;
                        entityStore.CookieExpired = null;
                    }
                }
                TicketStore ticketStore = new() { entityStore = entityStore, channel = channel, finished = false, orderTime = DateTime.Now };
                db.Tickets.Add(ticketStore);
                db.AppRuntimeConfig.First().lastGetTime = DateTime.Now;
                await db.SaveChangesAsync();
                await eventbus.ProduceTicket(ticketStore);
            });
            connection.On("Handshake", (string message) => logger.LogInformation(message));
            try
            {
                await connection.StartAsync();
            }catch(Exception e)
            {
                logger.LogError(e, "Error while establishing connection with order service");
            }

        }

        public async Task StartedAsync(CancellationToken cancellationToken)
        {
        }

        public async Task StartingAsync(CancellationToken cancellationToken)
        {
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
        }

        public async Task StoppedAsync(CancellationToken cancellationToken)
        {
        }

        public async Task StoppingAsync(CancellationToken cancellationToken)
        {
        }
    }
}
