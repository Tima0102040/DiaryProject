using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers;

public class TeacherController: Controller
{
    private readonly DiaryProjectContext _context;

    public TeacherController(DiaryProjectContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index(string sortOrder)
    {
        ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        var students = from s in _context.Teacher
            select s;
        switch (sortOrder)
        {
            case "name_desc":
                students = students.OrderByDescending(s => s.TeacherFullName);
                break;
            default:
                students = students.OrderBy(s => s.TeacherId);
                break;
        }
        return View(await students.AsNoTracking().ToListAsync());
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Teacher teacher)
    {
        if (ModelState.IsValid)
        {
            _context.Add(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(teacher);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var teacher = await _context.Teacher
            .FirstOrDefaultAsync(m => m.TeacherId == id);
        
        if (teacher == null)
        {
            return NotFound();
        }

        return View(teacher);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id,
         Teacher teacher)
    {
        if (id != teacher.TeacherId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(teacher);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExist(teacher.TeacherId))
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

        return View(teacher);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var teacher = await _context.Teacher.FindAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }

        return View(teacher);
    }
    
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var teacher = await _context.Teacher
            .FirstOrDefaultAsync(m => m.TeacherId == id);
        if (teacher == null)
        {
            return NotFound();
        }

        return View(teacher);
    }

    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var teacher = await _context.Teacher.FindAsync(id);
        _context.Teacher.Remove(teacher);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    private bool TeacherExist(int id)
    {
        return _context.Teacher.Any(e => e.TeacherId == id);
    }
}