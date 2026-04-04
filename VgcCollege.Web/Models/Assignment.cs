using System.ComponentModel.DataAnnotations;

namespace VgcCollege.Web.Models;

public class Assignment
{
    public int Id { get; set; }

    [Required]
    public int CourseId { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Range(0, 1000)]
    public double MaxScore { get; set; } = 100;

    [Required]
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; }

    public Course? Course { get; set; }

    public List<AssignmentResult> AssignmentResults { get; set; } = new();
}