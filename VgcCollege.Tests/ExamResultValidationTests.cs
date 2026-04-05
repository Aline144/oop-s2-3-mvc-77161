using System.ComponentModel.DataAnnotations;
using VgcCollege.Web.Models;
using Xunit;

namespace VgcCollege.Tests;

public class ExamResultValidationTests
{
    [Fact]
    public void ExamResult_Should_Be_Valid_With_Proper_Data()
    {
        var result = new ExamResult
        {
            ExamId = 1,
            StudentProfileId = 1,
            Score = 78,
            Grade = "B"
        };

        var context = new ValidationContext(result);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(result, context, results, true);

        Assert.True(isValid);
    }
}   