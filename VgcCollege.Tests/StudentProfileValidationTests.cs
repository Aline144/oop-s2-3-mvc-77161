using System.ComponentModel.DataAnnotations;
using VgcCollege.Web.Models;
using Xunit;

namespace VgcCollege.Tests;

public class StudentProfileValidationTests
{
    [Fact]
    public void StudentProfile_Should_Be_Invalid_When_Name_Is_Missing()
    {
        var student = new StudentProfile
        {
            Name = "",
            Email = "student1@vgc.ie",
            Phone = "123456789",
            Address = "Dublin, Ireland",
            StudentNumber = "ST001",
            IdentityUserId = "user-1"
        };

        var context = new ValidationContext(student);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(student, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains("Name"));
    }
}