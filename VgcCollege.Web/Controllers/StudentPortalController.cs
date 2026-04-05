using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VgcCollege.Web.Data;

namespace VgcCollege.Web.Controllers;

[Authorize(Roles = "Student")]
public class StudentPortalController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public StudentPortalController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> MyProfile()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(s => s.IdentityUserId == user.Id);

        if (studentProfile == null)
        {
            return NotFound();
        }

        return View(studentProfile);
    }

    public async Task<IActionResult> MyExamResults()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        var studentProfile = await _context.StudentProfiles
            .FirstOrDefaultAsync(s => s.IdentityUserId == user.Id);

        if (studentProfile == null)
        {
            return NotFound();
        }

        var examResults = await _context.ExamResults
            .Include(er => er.Exam)
            .ThenInclude(e => e!.Course)
            .Where(er => er.StudentProfileId == studentProfile.Id && er.Exam != null && er.Exam.ResultsReleased)
            .ToListAsync();

        return View(examResults);
    }
}