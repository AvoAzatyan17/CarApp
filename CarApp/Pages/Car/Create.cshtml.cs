using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Car;

public class Create : PageModel
{
    private readonly ApplicationDbContext _context;

    public Create(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Domain.Entities.Mark> Marks { get; set; }
    
    [BindProperty]
    public Domain.Entities.Car Car { get; set; }


    public async Task<IActionResult> OnGetAsync()
    {
        Marks = await _context.Marks.ToListAsync();
        return Page();

    }

    public async Task<IActionResult> OnPostCreate()
    {
        Car.Id =  Guid.NewGuid();
        Car.IsDeleted = false;
        Car.Created = DateTime.UtcNow;
        Car.LastModified = DateTime.UtcNow;
        Car.CreatedBy = GetRandomName();
        _context.Cars.Add(Car);
        await _context.SaveChangesAsync();
        return RedirectToPage("/Car/Index");
    }
    
    private string GetRandomName()
    {
        var names = new[] { "Alex", "Jordan", "Taylor", "Casey", "Riley", "Morgan", "Jamie", "Avery" };
        var random = new Random();
        return names[random.Next(names.Length)];
    }
}