using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Car;

public class Details : PageModel
{
    private readonly ApplicationDbContext _context;

    public Details(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public Domain.Entities.Car? Car { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        Car = await _context.Cars
            .Include(c => c.Mark)
            .FirstOrDefaultAsync(c => c.Id == id);
        return Page();
    }
}