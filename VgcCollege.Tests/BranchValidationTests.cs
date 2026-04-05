using System.ComponentModel.DataAnnotations;
using VgcCollege.Web.Models;
using Xunit;

namespace VgcCollege.Tests;

public class BranchValidationTests
{
    [Fact]
    public void Branch_Should_Be_Invalid_When_Name_Is_Missing()
    {
        var branch = new Branch
        {
            Name = "",
            Address = "Dublin, Ireland"
        };

        var context = new ValidationContext(branch);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(branch, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains("Name"));
    }
}