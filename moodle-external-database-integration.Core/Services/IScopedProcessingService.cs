namespace moodle_external_database_integration.Core.Services;
public interface IScopedProcessingService
{
    Task DoWork(CancellationToken stoppingToken);
}
