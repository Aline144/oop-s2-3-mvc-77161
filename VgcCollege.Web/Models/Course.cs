using System.ComponentModel.DataAnnotations;

namespace VgcCollege.Web.Models;

public class Course
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int BranchId { get; set; }

    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    public Branch? Branch { get; set; }

    public List<CourseEnrolment> CourseEnrolments { get; set; } = new();

    public List<Assignment> Assignments { get; set; } = new();

    public List<Exam> Exams { get; set; } = new();
}