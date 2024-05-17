// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Treenity_AI_Scraper.Services.Cipher;
using Treenity_AI_Scraper.Services;
using Microsoft.EntityFrameworkCore;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContext<ProgramDbContext>(ServiceLifetime.Transient);
builder.Services.AddTransient<AnswerService>();
//builder.Services.AddSingleton<OnlineStoreService>(); //get online tickets from delegate service, impl in OnStartTicketProcessHostedService.StartAsync
builder.Services.AddSingleton<TicketProcessorService>();
builder.Services.AddScoped<TreenityCryptoProvider>();
builder.Services.AddTransient<TreenityCookieService>();
builder.Services.AddTransient<WorkService>();
builder.Services.AddHostedService<OnStartTicketProcessHostedService>();
//builder.Services.AddHostedService<SignalRHostedService>(); //real time ticket processing, need a host server using SignalR
IHost host = builder.Build();
//migrate database
using (IServiceScope scope = host.Services.CreateScope())
{
    ProgramDbContext context = scope.ServiceProvider.GetRequiredService<ProgramDbContext>();
    context.Database.Migrate();
    //manual add answer example:
    //AnswerService answerService = scope.ServiceProvider.GetRequiredService<AnswerService>();
    //answerService.SetAnswer(925810868, [626263035]);
}
await host.RunAsync();
