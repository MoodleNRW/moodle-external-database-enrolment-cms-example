using moodle_external_database_integration.Core.Models.Moodle;

namespace moodle_external_database_integration.Core.Models.External;

public class ExternalTransferUser
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string EMail { get; set; }
    public virtual MoodleUser MoodleUser { get; set; }
    public virtual ICollection<ExternalTransferCourseUser> UserCourses { get; set; }
    public virtual ICollection<MoodleEnrolment> MoodleEnrolments { get; set; }

    public ExternalTransferUser() { }
}