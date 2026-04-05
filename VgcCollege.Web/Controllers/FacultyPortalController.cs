using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VgcCollege.Web.Data;

namespace VgcCollege.Web.Controllers;

[Authorize(Roles = "Faculty")]
public class FacultyPortalController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public FacultyPortalController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
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

        var facultyProfile = await _context.FacultyProfiles
            .FirstOrDefaultAsync(f => f.IdentityUserId == user.Id);

        if (facultyProfile == null)
        {
            return NotFound();
        }

        return View(facultyProfile);
    }

    public async Task<IActionResult> StudentList()
    {
        var students = await _context.StudentProfiles
            .OrderBy(s => s.Name)
            .ToListAsync();

        return View(students);
    }
}