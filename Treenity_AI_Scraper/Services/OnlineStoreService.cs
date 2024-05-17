
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using Treenity_AI_Scraper.Models.Runtime;

namespace Treenity_AI_Scraper.Services
{
    internal class OnlineStoreService(ProgramDbContext dbContext,ILogger<OnlineStoreService> logger)
    {
        public async Task<List<OnlineTicket.Ticket>> GetOnlineTickets()
        {
            using HttpClient client = new();
            var users = await client.GetFromJsonAsync<List<OnlineTicket>>(@"http://your_delegate_service/api/Users");
            users.ForEach(u => logger.LogInformation(u.userName + ":producedPoint=" +  u.producedPoint));
            var tokens = users.Select(u => u.token).ToList();
            List<OnlineTicket.Ticket> tickets = [];
            foreach (var token in tokens) 
            {
                var result = await client.GetFromJsonAsync<List<OnlineTicket.Ticket>>(@"http://your_delegate_service/api/Tickets" + "?Token=" + token + (dbContext.AppRuntimeConfig.Any() ? ("&lastDateTime=" + dbContext.AppRuntimeConfig.First().lastGetTime) : ""));
                tickets.AddRange(result);
                //log
                logger.LogInformation("Get " + result.Count + " tickets from " + token);
            }
            if (dbContext.AppRuntimeConfig.Any())
            {
                dbContext.AppRuntimeConfig.First().lastGetTime = DateTime.Now;
            }
            else
            {
                dbContext.AppRuntimeConfig.Add(new() { lastGetTime = DateTime.Now });
            }
            await dbContext.SaveChangesAsync();
            return tickets;
        }
    }
}
