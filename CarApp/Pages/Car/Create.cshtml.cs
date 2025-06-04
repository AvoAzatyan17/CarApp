using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarApp.Pages.Car;

public class Create : PageModel
{
    private readonly ApplicationDbContext _context;
    
    [BindProperty]
    public Domain.Entities.Car Car { get; set; }

    public Create(ApplicationDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostCreate()
    {
        Car.Id = Guid.NewGuid();
        Car.MakeId = Guid.NewGuid();
        Car.CreatedBy = Guid.NewGuid().ToString();
        Car.LastModified = DateTime.UtcNow;
        Car.Created = DateTime.UtcNow;
        Car.IsDeleted = false;
        _context.Cars.Add(Car);
        await _context.SaveChangesAsync();
        return RedirectToPage("/Car/Index");
    }
}