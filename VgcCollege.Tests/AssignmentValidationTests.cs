using System.ComponentModel.DataAnnotations;
using VgcCollege.Web.Models;
using Xunit;

namespace VgcCollege.Tests;

public class AssignmentValidationTests
{
    [Fact]
    public void Assignment_Should_Be_Invalid_When_Title_Is_Missing()
    {
        var assignment = new Assignment
        {
            Title = "",
            CourseId = 1,
            MaxScore = 100,
            DueDate = DateTime.Today.AddDays(7)
        };

        var context = new ValidationContext(assignment);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(assignment, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains("Title"));
    }
}