using Unisinos.Abc.Orchestrator;
using Unisinos.Abc.Orchestrator.Infrastructure.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {

        IConfiguration configurationService = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

        services.AddMasstransitConfirurations(configurationService);
        services.AddHostedService<Worker>();

    })
    .Build();

await host.RunAsync();
