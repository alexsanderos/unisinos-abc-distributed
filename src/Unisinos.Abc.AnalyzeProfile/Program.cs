using Unisinos.Abc.AnalyzeProfile;
using Unisinos.Abc.AnalyzeProfile.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        IConfiguration configurationService = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

        services.AddMasstransitConfiguration(configurationService);

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
