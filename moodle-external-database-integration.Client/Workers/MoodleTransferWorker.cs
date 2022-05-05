using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using moodle_external_database_integration.Core.Services;

namespace moodle_external_database_integration.Client.Workers;

public class MoodleTransferWorker : BackgroundService
{
    private readonly ILogger<MoodleTransferWorker> _logger;

    public MoodleTransferWorker(IServiceProvider services, ILogger<MoodleTransferWorker> logger)
    {
        _logger = logger;

        Services = services;
    }

    public IServiceProvider Services { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Moodle Transfer Worker started at: {time}", DateTimeOffset.Now);

        await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Moodle Transfer Worker is working.");

        using (var scope = Services.CreateScope())
        {
            var scopedProcessingService =
                scope.ServiceProvider
                    .GetRequiredService<IMoodleTransferService>();

            await scopedProcessingService.DoWork(stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Moodle Transfer Worker is stopping.");

        await base.StopAsync(stoppingToken);
    }
}