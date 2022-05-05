namespace moodle_external_database_integration.Core.Models.External;

public class ExternalTransferCourseUser
{
    public Guid ExternalTransferCourseId { get; set; }
    public virtual ExternalTransferCourse ExternalTransferCourse { get; set; }
    public Guid ExternalTransferUserId { get; set; }
    public virtual ExternalTransferUser ExternalTransferUser { get; set; }

    public ExternalTransferCourseUser() { }
}