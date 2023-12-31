﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);


builder.Services.AddTransient<IRouletteProgram, RouletteProgram>();
builder.Services.AddTransient<IRouletteService, RouletteService>();
builder.Services.AddTransient<IBettingService, BettingService>();
builder.Services.AddTransient<IRouletteWheelService, RouletteWheelService>();
builder.Services.AddTransient<INavigationService, NavigationService>();

builder.Services.AddTransient<IExampleTransientService, ExampleTransientService>();
builder.Services.AddScoped<IExampleScopedService, ExampleScopedService>();
builder.Services.AddSingleton<IExampleSingletonService, ExampleSingletonService>();
builder.Services.AddTransient<ServiceLifetimeReporter>();
builder.Services.AddDbContext<RouletteContext>();

builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);

using IHost host = builder.Build();
RunApplication(host.Services);
await host.RunAsync();

static void RunApplication(IServiceProvider hostProvider)
{
    using IServiceScope serviceScope = hostProvider.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    var navigationService = provider.GetRequiredService<INavigationService>();
    navigationService.NavigateHome();
}