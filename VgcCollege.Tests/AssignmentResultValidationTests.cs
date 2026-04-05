using System.ComponentModel.DataAnnotations;
using VgcCollege.Web.Models;
using Xunit;

namespace VgcCollege.Tests;

public class AssignmentResultValidationTests
{
    [Fact]
    public void AssignmentResult_Should_Be_Valid_With_Proper_Data()
    {
        var result = new AssignmentResult
        {
            AssignmentId = 1,
            StudentProfileId = 1,
            Score = 80,
            Feedback = "Good work"
        };

        var context = new ValidationContext(result);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(result, context, results, true);

        Assert.True(isValid);
    }
}