namespace moodle_external_database_integration.Core.Services;
public interface IExternalTransferService : IScopedProcessingService
{
    Task<bool> GetExternalDataAsync();
}