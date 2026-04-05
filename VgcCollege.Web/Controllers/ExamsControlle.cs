using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VgcCollege.Web.Data;
using VgcCollege.Web.Models;

namespace VgcCollege.Web.Controllers;

[Authorize(Roles = "Admin")]
public class ExamsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ExamsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var exams = await _context.Exams
            .Include(e => e.Course)
            .ToListAsync();

        return View(exams);
    }

    public IActionResult Create()
    {
        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Exam exam)
    {
        if (ModelState.IsValid)
        {
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", exam.CourseId);
        return View(exam);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var exam = await _context.Exams
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (exam == null)
        {
            return NotFound();
        }

        return View(exam);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var exam = await _context.Exams.FindAsync(id);

        if (exam == null)
        {
            return NotFound();
        }

        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", exam.CourseId);
        return View(exam);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Exam exam)
    {
        if (id != exam.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(exam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", exam.CourseId);
        return View(exam);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var exam = await _context.Exams
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (exam == null)
        {
            return NotFound();
        }

        return View(exam);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var exam = await _context.Exams.FindAsync(id);

        if (exam != null)
        {
            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}