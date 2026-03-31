namespace VgcCollege.Web.Models;

public class StudentProfile
{
    public int Id { get; set; }

    public string IdentityUserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string StudentNumber { get; set; } = string.Empty;
}