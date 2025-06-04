using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Mark;

public class Details : PageModel
{
    private readonly ApplicationDbContext _context;

    public Details(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty] public Domain.Entities.Mark? Mark { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Mark = await _context.Marks.FirstOrDefaultAsync(m => m.Id == id);
        if (Mark == null)
            return NotFound();

        return Page();
    }
}