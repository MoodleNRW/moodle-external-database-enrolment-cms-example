namespace moodle_external_database_integration.Core.Services;
public interface IMoodleTransferService : IScopedProcessingService
{
    Task<bool> MapExternalEnrolmentsAsync();
    Task<bool> MapExternalCoursesAsync();
}