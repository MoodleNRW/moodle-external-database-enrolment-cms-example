// See https://aka.ms/new-console-template for more information

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using moodle_external_database_integration.Client.Services;
using moodle_external_database_integration.Client.Workers;
using moodle_external_database_integration.Core.Options;
using moodle_external_database_integration.Core.Services;
using moodle_external_database_integration.Data;

static void ConfigureAppConfiguration(HostBuilderContext hostBuilderContext, IConfigurationBuilder configurationBuilder)
{
    configurationBuilder.Sources.Clear();

    IHostEnvironment env = hostBuilderContext.HostingEnvironment;

    configurationBuilder
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

    IConfigurationRoot configurationRoot = configurationBuilder.Build();

    ConnectionStringsOptions connectionStringsOptions = new();
    MigrationsAssemblyOptions migrationsAssemblyOptions = new();

    configurationRoot.GetSection(nameof(ConnectionStringsOptions))
                     .Bind(connectionStringsOptions);
    configurationRoot.GetSection(nameof(MigrationsAssemblyOptions))
                     .Bind(migrationsAssemblyOptions);
}

static void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection serviceCollection)
{
    var configuration = hostBuilderContext.Configuration;
    var connectionStringsOptions = configuration.GetSection(nameof(ConnectionStringsOptions)).Get<ConnectionStringsOptions>();
    var migrationsAssemblyOptions = configuration.GetSection(nameof(MigrationsAssemblyOptions)).Get<MigrationsAssemblyOptions>();

    serviceCollection.AddDbContext<MoodleExternalDatabaseIntegrationDbContext>(options =>
        options
        .UseNpgsql(connectionStringsOptions.DefaultConnection, x => x.MigrationsAssembly(migrationsAssemblyOptions.DefaultMigrationsAssembly))
    );

    serviceCollection.AddHostedService<ExternalTransferWorker>();
    serviceCollection.AddHostedService<MoodleTransferWorker>();
    serviceCollection.AddScoped<IExternalTransferService, ExternalTransferService>();
    serviceCollection.AddScoped<IMoodleTransferService, MoodleTransferService>();
}


using IHost host = Host.CreateDefaultBuilder(args).ConfigureAppConfiguration(ConfigureAppConfiguration).ConfigureServices(ConfigureServices).Build();

await host.RunAsync();