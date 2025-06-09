using CarApp.Options;
using Domain.Interface;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Minio;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => { b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName); });
});

builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetService<ApplicationDbContext>());
builder.Services.AddScoped<ICarRepository, CarRepository>();

var minioOptions = new MinioOptions();
builder.Configuration.GetSection(nameof(MinioOptions)).Bind(minioOptions);

builder.Services.AddMinio(configureClient => configureClient
    .WithEndpoint(minioOptions.Endpoint)
    .WithSSL(secure: false)
    .WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey)
    .Build());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();