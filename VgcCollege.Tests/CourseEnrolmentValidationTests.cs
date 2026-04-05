using System.ComponentModel.DataAnnotations;
using VgcCollege.Web.Models;
using Xunit;

namespace VgcCollege.Tests;

public class CourseEnrolmentValidationTests
{
    [Fact]
    public void CourseEnrolment_Should_Be_Invalid_When_Status_Is_Missing()
    {
        var enrolment = new CourseEnrolment
        {
            StudentProfileId = 1,
            CourseId = 1,
            EnrolDate = DateTime.Today,
            Status = ""
        };

        var context = new ValidationContext(enrolment);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(enrolment, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains("Status"));
    }
}