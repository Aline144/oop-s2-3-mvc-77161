using System.ComponentModel.DataAnnotations;
using VgcCollege.Web.Models;
using Xunit;

namespace VgcCollege.Tests;

public class CourseValidationTests
{
    [Fact]
    public void Course_Should_Be_Invalid_When_Name_Is_Missing()
    {
        var course = new Course
        {
            Name = "",
            BranchId = 1,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddMonths(3)
        };

        var context = new ValidationContext(course);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(course, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains("Name"));
    }
}