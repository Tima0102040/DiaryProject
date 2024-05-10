using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.ViewModels;

namespace Project.Controllers;

public class ClassController: Controller
{
    private readonly DiaryProjectContext _context;

    public ClassController(DiaryProjectContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        return View(await _context.Class.ToListAsync());
    }
    
    public async Task<IActionResult> Create()
    {
        ViewBag.Teachers = await _context.Teacher.ToListAsync();
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Class classes)
    {
        if (ModelState.IsValid)
        {
            _context.Add(classes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(classes);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var admin = await _context.Admin
            .FirstOrDefaultAsync(m => m.AdminId == id);
        
        if (admin == null)
        {
            return NotFound();
        }

        return View(admin);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id,
         Admin admin)
    {
        if (id != admin.AdminId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(admin);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExist(admin.AdminId))
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

        return View(admin);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var admin = await _context.Admin.FindAsync(id);
        if (admin == null)
        {
            return NotFound();
        }

        return View(admin);
    }
    
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var admin = await _context.Admin
            .FirstOrDefaultAsync(m => m.AdminId == id);
        if (admin == null)
        {
            return NotFound();
        }

        return View(admin);
    }

    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var admin = await _context.Admin.FindAsync(id);
        _context.Admin.Remove(admin);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    private bool AdminExist(int id)
    {
        return _context.Admin.Any(e => e.AdminId == id);
    }
}