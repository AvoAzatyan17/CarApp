using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Car;

public class Edit : PageModel
{
    private readonly ApplicationDbContext _context;

    public Edit(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty] public Domain.Entities.Car Car { get; set; }

    public IList<Domain.Entities.Mark> Marks { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        Car = await _context.Cars
            .Include(c => c.Mark)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (Car == null)
        {
            return NotFound();
        }

        Marks = await _context.Marks.ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var carInDb = await _context.Cars.FirstOrDefaultAsync(c => c.Id == Car.Id);
        if (carInDb == null)
        {
            return NotFound();
        }
        carInDb.Name = Car.Name;
        carInDb.MarkId = Car.MarkId;
        _context.Cars.Update(carInDb);
        await _context.SaveChangesAsync();
        return RedirectToPage("/Car/Index");
    }
}