using System.ComponentModel.DataAnnotations;
using VgcCollege.Web.Models;
using Xunit;

namespace VgcCollege.Tests;

public class ExamValidationTests
{
    [Fact]
    public void Exam_Should_Be_Invalid_When_Title_Is_Missing()
    {
        var exam = new Exam
        {
            Title = "",
            CourseId = 1,
            Date = DateTime.Today,
            MaxScore = 100,
            ResultsReleased = false
        };

        var context = new ValidationContext(exam);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(exam, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains("Title"));
    }
}