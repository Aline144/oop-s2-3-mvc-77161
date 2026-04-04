using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VgcCollege.Web.Data;
using VgcCollege.Web.Models;

namespace VgcCollege.Web.Controllers;

[Authorize(Roles = "Admin")]
public class CoursesController : Controller
{
    private readonly ApplicationDbContext _context;

    public CoursesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _context.Courses
            .Include(c => c.Branch)
            .ToListAsync();

        return View(courses);
    }

    public IActionResult Create()
    {
        ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Course course)
    {
        if (ModelState.IsValid)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name", course.BranchId);
        return View(course);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses
            .Include(c => c.Branch)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses.FindAsync(id);

        if (course == null)
        {
            return NotFound();
        }

        ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name", course.BranchId);
        return View(course);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Course course)
    {
        if (id != course.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name", course.BranchId);
        return View(course);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses
            .Include(c => c.Branch)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course != null)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}