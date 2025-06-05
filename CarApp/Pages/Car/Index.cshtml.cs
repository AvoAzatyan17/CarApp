using Domain.Interface;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Pages.Car;

public class Index : PageModel
{
    private readonly ApplicationDbContext _context;
    
    private readonly ICarRepository _carRepository;

    public Index(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public IList<Domain.Entities.Car> Cars { get; set; }

    public async Task OnGet()
    {
        Cars = await _carRepository.GetAllAsync();
    }

    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        var deleted = await _carRepository.SoftDeleteAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return RedirectToPage("/Car/Index");
    }
}