using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VgcCollege.Web.Data;
using VgcCollege.Web.Models;

namespace VgcCollege.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AttendanceRecordsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AttendanceRecordsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var attendanceRecords = await _context.AttendanceRecords
            .Include(a => a.CourseEnrolment)
            .ThenInclude(e => e.StudentProfile)
            .Include(a => a.CourseEnrolment)
            .ThenInclude(e => e.Course)
            .ToListAsync();

        return View(attendanceRecords);
    }

    public IActionResult Create()
    {
        var enrolments = _context.CourseEnrolments
            .Include(e => e.StudentProfile)
            .Include(e => e.Course)
            .ToList();

        ViewData["CourseEnrolmentId"] = new SelectList(
            enrolments.Select(e => new
            {
                e.Id,
                Display = e.StudentProfile!.Name + " - " + e.Course!.Name
            }),
            "Id",
            "Display"
        );

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AttendanceRecord attendanceRecord)
    {
        if (ModelState.IsValid)
        {
            _context.AttendanceRecords.Add(attendanceRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        var enrolments = _context.CourseEnrolments
            .Include(e => e.StudentProfile)
            .Include(e => e.Course)
            .ToList();

        ViewData["CourseEnrolmentId"] = new SelectList(
            enrolments.Select(e => new
            {
                e.Id,
                Display = e.StudentProfile!.Name + " - " + e.Course!.Name
            }),
            "Id",
            "Display",
            attendanceRecord.CourseEnrolmentId
        );

        return View(attendanceRecord);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var attendanceRecord = await _context.AttendanceRecords
            .Include(a => a.CourseEnrolment)
            .ThenInclude(e => e.StudentProfile)
            .Include(a => a.CourseEnrolment)
            .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (attendanceRecord == null)
        {
            return NotFound();
        }

        return View(attendanceRecord);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var attendanceRecord = await _context.AttendanceRecords.FindAsync(id);

        if (attendanceRecord == null)
        {
            return NotFound();
        }

        var enrolments = _context.CourseEnrolments
            .Include(e => e.StudentProfile)
            .Include(e => e.Course)
            .ToList();

        ViewData["CourseEnrolmentId"] = new SelectList(
            enrolments.Select(e => new
            {
                e.Id,
                Display = e.StudentProfile!.Name + " - " + e.Course!.Name
            }),
            "Id",
            "Display",
            attendanceRecord.CourseEnrolmentId
        );

        return View(attendanceRecord);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AttendanceRecord attendanceRecord)
    {
        if (id != attendanceRecord.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(attendanceRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        var enrolments = _context.CourseEnrolments
            .Include(e => e.StudentProfile)
            .Include(e => e.Course)
            .ToList();

        ViewData["CourseEnrolmentId"] = new SelectList(
            enrolments.Select(e => new
            {
                e.Id,
                Display = e.StudentProfile!.Name + " - " + e.Course!.Name
            }),
            "Id",
            "Display",
            attendanceRecord.CourseEnrolmentId
        );

        return View(attendanceRecord);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var attendanceRecord = await _context.AttendanceRecords
            .Include(a => a.CourseEnrolment)
            .ThenInclude(e => e.StudentProfile)
            .Include(a => a.CourseEnrolment)
            .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (attendanceRecord == null)
        {
            return NotFound();
        }

        return View(attendanceRecord);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var attendanceRecord = await _context.AttendanceRecords.FindAsync(id);

        if (attendanceRecord != null)
        {
            _context.AttendanceRecords.Remove(attendanceRecord);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}