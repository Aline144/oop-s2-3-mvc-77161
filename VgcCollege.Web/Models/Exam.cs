using System.ComponentModel.DataAnnotations;

namespace VgcCollege.Web.Models;

public class Exam
{
    public int Id { get; set; }

    [Required]
    public int CourseId { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [Range(0, 1000)]
    public double MaxScore { get; set; } = 100;

    public bool ResultsReleased { get; set; } = false;

    public Course? Course { get; set; }

    public List<ExamResult> ExamResults { get; set; } = new();
}