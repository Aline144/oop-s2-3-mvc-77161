using System.ComponentModel.DataAnnotations;
using VgcCollege.Web.Models;
using Xunit;

namespace VgcCollege.Tests;

public class FacultyProfileValidationTests
{
    [Fact]
    public void FacultyProfile_Should_Be_Invalid_When_Name_Is_Missing()
    {
        var faculty = new FacultyProfile
        {
            Name = "",
            Email = "faculty@vgc.ie",
            Phone = "123456789",
            IdentityUserId = "user-1"
        };

        var context = new ValidationContext(faculty);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(faculty, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains("Name"));
    }
}