using moodle_external_database_integration.Core.Models.External;

namespace moodle_external_database_integration.Core.Models.Moodle;

public class MoodleCourse
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string ShortName { get; set; }
    public string IdNumber { get; set; }
    public int? MoodleId { get; set; }
    public virtual ICollection<MoodleEnrolment> MoodleEnrolments { get; set; }

    #region External Course
    public Guid? ExternalTransferCourseId { get; set; }
    public ExternalTransferCourse ExternalTransferCourse { get; set; }
    #endregion

    public MoodleCourse() { }

    public MoodleCourse(ExternalTransferCourse externalTransferCourse)
    {
        this.FullName = externalTransferCourse.FullName;
        this.ShortName = externalTransferCourse.ShortName;

        this.ExternalTransferCourse = externalTransferCourse;
    }
}