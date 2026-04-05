using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VgcCollege.Web.Data;
using VgcCollege.Web.Models;

namespace VgcCollege.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AssignmentResultsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AssignmentResultsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var assignmentResults = await _context.AssignmentResults
            .Include(ar => ar.Assignment)
            .Include(ar => ar.StudentProfile)
            .ToListAsync();

        return View(assignmentResults);
    }

    public IActionResult Create()
    {
        ViewData["AssignmentId"] = new SelectList(_context.Assignments, "Id", "Title");
        ViewData["StudentProfileId"] = new SelectList(_context.StudentProfiles, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AssignmentResult assignmentResult)
    {
        if (ModelState.IsValid)
        {
            _context.AssignmentResults.Add(assignmentResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["AssignmentId"] = new SelectList(_context.Assignments, "Id", "Title", assignmentResult.AssignmentId);
        ViewData["StudentProfileId"] = new SelectList(_context.StudentProfiles, "Id", "Name", assignmentResult.StudentProfileId);
        return View(assignmentResult);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var assignmentResult = await _context.AssignmentResults
            .Include(ar => ar.Assignment)
            .Include(ar => ar.StudentProfile)
            .FirstOrDefaultAsync(ar => ar.Id == id);

        if (assignmentResult == null)
        {
            return NotFound();
        }

        return View(assignmentResult);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var assignmentResult = await _context.AssignmentResults.FindAsync(id);

        if (assignmentResult == null)
        {
            return NotFound();
        }

        ViewData["AssignmentId"] = new SelectList(_context.Assignments, "Id", "Title", assignmentResult.AssignmentId);
        ViewData["StudentProfileId"] = new SelectList(_context.StudentProfiles, "Id", "Name", assignmentResult.StudentProfileId);
        return View(assignmentResult);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AssignmentResult assignmentResult)
    {
        if (id != assignmentResult.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(assignmentResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["AssignmentId"] = new SelectList(_context.Assignments, "Id", "Title", assignmentResult.AssignmentId);
        ViewData["StudentProfileId"] = new SelectList(_context.StudentProfiles, "Id", "Name", assignmentResult.StudentProfileId);
        return View(assignmentResult);
    }
}