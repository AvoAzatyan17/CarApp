using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Mark;

public class Edit : PageModel
{
    private readonly ApplicationDbContext _context;

    public Edit(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public Domain.Entities.Mark Mark { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        Mark = await _context.Marks.FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);
        if (Mark == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var markInDb = await _context.Marks.FirstOrDefaultAsync(m => m.Id == Mark.Id);
        if (markInDb == null)
        {
            return NotFound();
        }
        markInDb.Name = Mark.Name;
        await _context.SaveChangesAsync();
        return RedirectToPage("/Mark/Index");
    }
   
}