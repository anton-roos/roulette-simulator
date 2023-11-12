using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IRouletteProgram, RouletteProgram>();
builder.Services.AddSingleton<IRouletteService, RouletteService>();
builder.Services.AddSingleton<IBettingService, BettingService>();
builder.Services.AddTransient<IRouletteWheelService, RouletteWheelService>();

builder.Services.AddTransient<IExampleTransientService, ExampleTransientService>();
builder.Services.AddScoped<IExampleScopedService, ExampleScopedService>();
builder.Services.AddSingleton<IExampleSingletonService, ExampleSingletonService>();
builder.Services.AddTransient<ServiceLifetimeReporter>();
builder.Services.AddDbContext<RouletteContext>(
        options => options.UseSqlite("Data Source=Roulette.db"));

using IHost host = builder.Build();

ExemplifyServiceLifetime(host.Services);

await host.RunAsync();

static void ExemplifyServiceLifetime(IServiceProvider hostProvider)
{
    using IServiceScope serviceScope = hostProvider.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    IRouletteProgram rouletteProgram = provider.GetRequiredService<IRouletteProgram>();
    rouletteProgram.Run();
}