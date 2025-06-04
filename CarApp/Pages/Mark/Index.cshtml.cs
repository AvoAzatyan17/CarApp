using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Mark;

public class Index : PageModel
{
    private readonly ApplicationDbContext _context;
    
    public Index(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Domain.Entities.Mark> Marks { get; set; }

    public async Task OnGetAsync()
    {
        Marks = await _context.Marks.ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    {
        var mark = await _context.Marks.FindAsync(id);
        if (mark == null)
        {
            return NotFound();
        }
        mark.IsDeleted = true;
        _context.Marks.Update(mark);
        await _context.SaveChangesAsync();
        return RedirectToPage();
    }
    
}