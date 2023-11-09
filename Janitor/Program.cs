using Janitor.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.Configure<IConfiguration>(configuration);
        services.AddHostedService<DataUpdaterService>();
    })
    .Build();


await host.RunAsync();
