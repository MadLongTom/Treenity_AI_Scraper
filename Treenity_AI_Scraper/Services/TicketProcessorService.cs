using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;
using Treenity_AI_Scraper.Models.Database;
using Channel = System.Threading.Channels.Channel;

namespace Treenity_AI_Scraper.Services
{
    internal class TicketProcessorService(ProgramDbContext db, IServiceProvider serviceProvider, ILogger<TicketProcessorService> logger, IHostApplicationLifetime lifetime)
    {
        Channel<TicketStore> channel = Channel.CreateUnbounded<TicketStore>();
        private SemaphoreSlim semaphoreSlim = new(3);
        List<string> workingAccounts = [];
        public async Task ProduceTicket(TicketStore ticketStore)
        {
            await channel.Writer.WriteAsync(ticketStore);
            await Task.Factory.StartNew(TryRunWork);
        }

        private async Task TryRunWork()
        {
            logger.LogInformation($"RunWork called with slim={semaphoreSlim.CurrentCount},queue={channel.Reader.Count}");
            //if (semaphoreSlim.CurrentCount == 3 && channel.Reader.Count == 0 && !Connected) lifetime.StopApplication();
            if (semaphoreSlim.CurrentCount > 0 && channel.Reader.Count > 0)
            {
                bool continueFlag = true;
                var ticketStore = await channel.Reader.ReadAsync();
                try
                {
                    await semaphoreSlim.WaitAsync();
                    logger.LogInformation("Run work for " + ticketStore.entityStore.username);
                    WorkService service = serviceProvider.GetRequiredService<WorkService>();
                    var task = Task.Run(async () =>
                    {
                        if (workingAccounts.Contains(ticketStore.entityStore.username))
                        {
                            await channel.Writer.WriteAsync(ticketStore);
                            continueFlag = false;
                            return;
                        }
                        workingAccounts.Add(ticketStore.entityStore.username);
                        await service.Run(ticketStore);
                    });
                    await task.WaitAsync(CancellationToken.None);
                    workingAccounts.Remove(ticketStore.entityStore.username);
                }
                catch (Exception e)
                {
                    logger.LogError(e, "Error while run worker");
                }
                finally
                {
                    semaphoreSlim.Release();
                    logger.LogInformation("Complete work for " + ticketStore.entityStore.username);
                }
                if (continueFlag) await Task.Factory.StartNew(TryRunWork);
            }
        }
    }
}
