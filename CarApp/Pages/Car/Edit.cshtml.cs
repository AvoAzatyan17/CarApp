using CarApp.Storage;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Minio;
using Minio.DataModel.Args;
using Domain.Entities;

namespace CarApp.Pages.Car;

public class Edit : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly IMinioClient _minioClient;


    public Edit(ApplicationDbContext context, IMinioClient minioClient)
    {
        _context = context;
        _minioClient = minioClient;
    }

    [BindProperty] public Domain.Entities.Car Car { get; set; }

    public IList<Domain.Entities.Mark> Marks { get; set; }
    public IList<Domain.Entities.Image> CarImages { get; set; }

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
        CarImages = await _context.Image
            .Where(img => img.CarId == Car.Id && !img.IsDeleted)
            .ToListAsync();
        return Page();
    }

    [BindProperty] public List<IFormFile> UploadedFiles { get; set; }

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
        await UploadImage(carInDb.Id, UploadedFiles);
        return RedirectToPage("/Car/Index");
    }


    private string GetRandomName()
    {
        var names = new[] { "Alex", "Jordan", "Taylor", "Casey", "Riley", "Morgan", "Jamie", "Avery" };
        var random = new Random();
        return names[random.Next(names.Length)];
    }

    public async Task UploadImage(Guid carId,List<IFormFile> uploadedFiles)
    {
        foreach (var file in uploadedFiles)
        {
            var objectPath = $"{carId}/{file.FileName}";
            using var stream = file.OpenReadStream();
            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket("car-images")
                .WithObject(objectPath)
                .WithStreamData(stream)
                .WithObjectSize(file.Length)
                .WithContentType(file.ContentType));
            var image = new Image
            {
                CarId = carId,
                ObjectPath = objectPath,
                Created = DateTime.UtcNow,
                IsDeleted = false,
                LastModified = DateTime.UtcNow,
                CreatedBy = GetRandomName()
            };
            _context.Image.Add(image);
        }

        await _context.SaveChangesAsync();
    }
}