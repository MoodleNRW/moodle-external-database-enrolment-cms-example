using moodle_external_database_integration.Core.Models.External;

namespace moodle_external_database_integration.Core.Models.Moodle;

public class MoodleUser
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string EMail { get; set; }
    public string IdNumber { get; set; }
    public int? MoodleId { get; set; }
    public virtual ICollection<MoodleEnrolment> MoodleEnrolments { get; set; }

    #region External User
    public Guid? ExternalTransferUserId { get; set; }
    public ExternalTransferUser ExternalTransferUser { get; set; }
    #endregion

    public MoodleUser() { }

    public MoodleUser(ExternalTransferUser externalTransferUser)
    {
        this.UserName = externalTransferUser.UserName;
        this.EMail = externalTransferUser.EMail;
        this.Password = String.Empty;

        this.ExternalTransferUser = externalTransferUser;
    }
}