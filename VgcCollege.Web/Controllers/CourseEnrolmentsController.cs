using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VgcCollege.Web.Data;
using VgcCollege.Web.Models;

namespace VgcCollege.Web.Controllers;

[Authorize(Roles = "Admin")]
public class CourseEnrolmentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public CourseEnrolmentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var enrolments = await _context.CourseEnrolments
            .Include(e => e.StudentProfile)
            .Include(e => e.Course)
            .ToListAsync();

        return View(enrolments);
    }

    public IActionResult Create()
    {
        ViewData["StudentProfileId"] = new SelectList(_context.StudentProfiles, "Id", "Name");
        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseEnrolment enrolment)
    {
        if (ModelState.IsValid)
        {
            _context.CourseEnrolments.Add(enrolment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["StudentProfileId"] = new SelectList(_context.StudentProfiles, "Id", "Name", enrolment.StudentProfileId);
        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", enrolment.CourseId);
        return View(enrolment);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var enrolment = await _context.CourseEnrolments
            .Include(e => e.StudentProfile)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (enrolment == null)
        {
            return NotFound();
        }

        return View(enrolment);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var enrolment = await _context.CourseEnrolments.FindAsync(id);

        if (enrolment == null)
        {
            return NotFound();
        }

        ViewData["StudentProfileId"] = new SelectList(_context.StudentProfiles, "Id", "Name", enrolment.StudentProfileId);
        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", enrolment.CourseId);
        return View(enrolment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CourseEnrolment enrolment)
    {
        if (id != enrolment.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(enrolment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["StudentProfileId"] = new SelectList(_context.StudentProfiles, "Id", "Name", enrolment.StudentProfileId);
        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", enrolment.CourseId);
        return View(enrolment);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var enrolment = await _context.CourseEnrolments
            .Include(e => e.StudentProfile)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (enrolment == null)
        {
            return NotFound();
        }

        return View(enrolment);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var enrolment = await _context.CourseEnrolments.FindAsync(id);

        if (enrolment != null)
        {
            _context.CourseEnrolments.Remove(enrolment);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}