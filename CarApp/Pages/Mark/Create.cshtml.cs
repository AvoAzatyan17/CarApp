using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarApp.Pages.Mark;

public class Create : PageModel
{
    private readonly ApplicationDbContext _context;
    
    [BindProperty]
    public Domain.Entities.Mark Mark { get; set; }

    public Create(ApplicationDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostCreate()
    {
        Mark.Id = Guid.NewGuid();
        Mark.CreatedBy = Guid.NewGuid().ToString();
        Mark.LastModified = DateTime.UtcNow;
        Mark.IsDeleted = false;
        _context.Marks.Add(Mark);
        await _context.SaveChangesAsync();
        return RedirectToPage("/Mark/Index");
    }
}