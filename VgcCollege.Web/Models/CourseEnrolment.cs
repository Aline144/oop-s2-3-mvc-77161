using System.ComponentModel.DataAnnotations;

namespace VgcCollege.Web.Models;

public class CourseEnrolment
{
    public int Id { get; set; }

    [Required]
    public int StudentProfileId { get; set; }

    [Required]
    public int CourseId { get; set; }

    [DataType(DataType.Date)]
    public DateTime EnrolDate { get; set; } = DateTime.Now;

    [Required]
    [StringLength(30)]
    public string Status { get; set; } = "Active";

    public StudentProfile? StudentProfile { get; set; }

    public Course? Course { get; set; }

    public List<AttendanceRecord> AttendanceRecords { get; set; } = new();
}