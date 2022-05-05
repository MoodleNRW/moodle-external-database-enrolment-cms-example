using moodle_external_database_integration.Core.Models.External;

namespace moodle_external_database_integration.Core.Models.Moodle;

public class MoodleEnrolment
{
    public Guid Id { get; set; }

    #region Moodle User
    public string UserIdNumber { get; set; }

    // This relates to the internally used MoodleUser Table
    public Guid MoodleUserId { get; set; }
    public MoodleUser MoodleUser { get; set; }
    #endregion

    #region Moodle Course
    public string CourseIdNumber { get; set; }

    // This relates to the internally used MoodleCourse Table
    public Guid MoodleCourseId { get; set; }
    public MoodleCourse MoodleCourse { get; set; }
    #endregion

    #region Moodle Role
    public int? RoleId { get; set; }
    #endregion

    #region External User
    public Guid? ExternalTransferUserId { get; set; }
    public virtual ExternalTransferUser ExternalTransferUser { get; set; }
    #endregion

    #region External Course
    public Guid? ExternalTransferCourseId { get; set; }
    public virtual ExternalTransferCourse ExternalTransferCourse { get; set; }
    #endregion

    public MoodleEnrolment() { }

    public MoodleEnrolment(MoodleCourse moodleCourse, MoodleUser moodleUser)
    {
        MoodleCourseId = moodleCourse.Id;
        MoodleCourse = moodleCourse;

        MoodleUserId = moodleUser.Id;
        MoodleUser = moodleUser;

        if (moodleCourse.ExternalTransferCourse == null)
        {
            ExternalTransferCourseId = moodleCourse.ExternalTransferCourseId;
        }
        else
        {
            ExternalTransferCourseId = moodleCourse.ExternalTransferCourse.Id;
            ExternalTransferCourse = moodleCourse.ExternalTransferCourse;
        }

        if (moodleUser.ExternalTransferUser == null)
        {
            ExternalTransferUserId = moodleUser.ExternalTransferUserId;
        }
        else
        {
            ExternalTransferUserId = moodleUser.ExternalTransferUser.Id;
            ExternalTransferUser = moodleUser.ExternalTransferUser;
        }
    }
}