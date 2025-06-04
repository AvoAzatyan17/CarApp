using Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Car;

public class Index : PageModel
{
    private readonly ApplicationDbContext _context;
    
    public Index(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Domain.Entities.Car> Cars { get; set; }

    public async Task OnGetAsync()
    {
        Cars = await _context.Cars.ToListAsync();
    }
}