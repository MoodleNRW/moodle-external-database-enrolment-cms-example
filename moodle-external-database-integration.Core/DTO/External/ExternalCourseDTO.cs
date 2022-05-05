namespace moodle_external_database_integration.Core.DTO.External;

public class ExternalCourseDTO
{
    public string FullName { get; set; }
    public string ShortName { get; set; }
    public List<ExternalUserDTO> Users { get; set; }
    public ExternalCourseDTO() { }
}