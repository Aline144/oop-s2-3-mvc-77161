using System.ComponentModel.DataAnnotations;

namespace VgcCollege.Web.Models;

public class ExamResult
{
    public int Id { get; set; }

    [Required]
    public int ExamId { get; set; }

    [Required]
    public int StudentProfileId { get; set; }

    [Range(0, 1000)]
    public double Score { get; set; } = 0;

    [StringLength(10)]
    public string Grade { get; set; } = string.Empty;

    public Exam? Exam { get; set; }

    public StudentProfile? StudentProfile { get; set; }
}