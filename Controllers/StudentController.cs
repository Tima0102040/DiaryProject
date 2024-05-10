using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers;

public class StudentController: Controller
{
    private readonly DiaryProjectContext _context;

    public StudentController(DiaryProjectContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index(string sortOrder)
    {
        ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        var students = from s in _context.Student
            select s;
        switch (sortOrder)
        {
            case "name_desc":
                students = students.OrderByDescending(s => s.StudentFullName);
                break;
            default:
                students = students.OrderBy(s => s.StudentId);
                break;
        }
        return View(await students.ToListAsync());
    }
    
    public async Task<IActionResult> Create()
    {
        ViewBag.Classes = await _context.Class.ToListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create( Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Student
            .FirstOrDefaultAsync(m => m.StudentId == id);
        
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id,
        Student student)
    {
        if (id != student.StudentId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExist(student.StudentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        return View(student);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Student.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }
    
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Student
            .FirstOrDefaultAsync(m => m.StudentId == id);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.Student.FindAsync(id);
        _context.Student.Remove(student);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    private bool TeacherExist(int id)
    {
        return _context.Teacher.Any(e => e.TeacherId == id);
    }

    public IActionResult ExportToCSV()
    {
        var builder = new StringBuilder();
        var students = from s in _context.Student
            select s;

        foreach (var student in students)
        {
            builder.AppendLine($"{student.StudentId}, {student.StudentFullName}");
        }

        return File(Encoding.UTF32.GetBytes(builder.ToString()), "text/csv", "users.csv");
    }
}