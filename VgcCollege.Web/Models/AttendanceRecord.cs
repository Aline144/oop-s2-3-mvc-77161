using System.ComponentModel.DataAnnotations;

namespace VgcCollege.Web.Models;

public class AttendanceRecord
{
    public int Id { get; set; }

    [Required]
    public int CourseEnrolmentId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.Now;

    public bool Present { get; set; } = true;

    public CourseEnrolment? CourseEnrolment { get; set; }
}