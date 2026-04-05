using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VgcCollege.Web.Data;
using VgcCollege.Web.Models;

namespace VgcCollege.Web.Controllers;

[Authorize(Roles = "Admin")]
public class ExamResultsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ExamResultsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var examResults = await _context.ExamResults
            .Include(er => er.Exam)
            .Include(er => er.StudentProfile)
            .ToListAsync();

        return View(examResults);
    }

    public IActionResult Create()
    {
        ViewData["ExamId"] = new SelectList(_context.Exams, "Id", "Title");
        ViewData["StudentProfileId"] = new SelectList(_context.StudentProfiles, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ExamResult examResult)
    {
        if (ModelState.IsValid)
        {
            _context.ExamResults.Add(examResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["ExamId"] = new SelectList(_context.Exams, "Id", "Title", examResult.ExamId);
        ViewData["StudentProfileId"] = new SelectList(_context.StudentProfiles, "Id", "Name", examResult.StudentProfileId);
        return View(examResult);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var examResult = await _context.ExamResults
            .Include(er => er.Exam)
            .Include(er => er.StudentProfile)
            .FirstOrDefaultAsync(er => er.Id == id);

        if (examResult == null)
        {
            return NotFound();
        }

        return View(examResult);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var examResult = await _context.ExamResults.FindAsync(id);

        if (examResult == null)
        {
            return NotFound();
        }

        ViewData["ExamId"] = new SelectList(_context.Exams, "Id", "Title", examResult.ExamId);
        ViewData["StudentProfileId"] = new SelectList(_context.StudentProfiles, "Id", "Name", examResult.StudentProfileId);
        return View(examResult);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ExamResult examResult)
    {
        if (id != examResult.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(examResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["ExamId"] = new SelectList(_context.Exams, "Id", "Title", examResult.ExamId);
        ViewData["StudentProfileId"] = new SelectList(_context.StudentProfiles, "Id", "Name", examResult.StudentProfileId);
        return View(examResult);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var examResult = await _context.ExamResults
            .Include(er => er.Exam)
            .Include(er => er.StudentProfile)
            .FirstOrDefaultAsync(er => er.Id == id);

        if (examResult == null)
        {
            return NotFound();
        }

        return View(examResult);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var examResult = await _context.ExamResults.FindAsync(id);

        if (examResult != null)
        {
            _context.ExamResults.Remove(examResult);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}