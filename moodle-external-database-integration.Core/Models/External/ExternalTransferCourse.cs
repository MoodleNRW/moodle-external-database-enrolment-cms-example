using moodle_external_database_integration.Core.Models.Moodle;

namespace moodle_external_database_integration.Core.Models.External;

public class ExternalTransferCourse
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string ShortName { get; set; }
    public virtual MoodleCourse MoodleCourse { get; set; }
    public virtual ICollection<ExternalTransferCourseUser> CourseUsers { get; set; }
    public virtual ICollection<MoodleEnrolment> MoodleEnrolments { get; set; }

    public ExternalTransferCourse() { }
}