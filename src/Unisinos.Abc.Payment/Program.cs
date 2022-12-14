using Unisinos.Abc.Payment;
using Unisinos.Abc.Payment.Infrastructure.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        IConfiguration configurationService = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

        services.AddMasstransitConfiguration(configurationService);
        services.AddPersistenceConfiguration(configurationService);

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
