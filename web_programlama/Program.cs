using web_programlama.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL i�in ApplicationDbContext yap�land�rmas�
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity servislerini ekle
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddRoles<IdentityRole>() // Rol deste�i ekle
.AddEntityFrameworkStores<ApplicationDbContext>();

// Swagger dok�mantasyonu i�in gerekli servisleri ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MVC Controller ve View yap�land�rmas�
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Hata y�netimi ve HSTS yap�land�rmas�
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Geli�tirme ortam�nda Swagger UI etkinle�tir
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "web_programlama API v1");
    });
}

// HTTPS y�nlendirme, statik dosyalar ve y�nlendirme
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Identity i�in kimlik do�rulama middleware'i ekle
app.UseAuthentication();
app.UseAuthorization();

// Varsay�lan route yap�land�rmas�
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// API Controller'lar� i�in mapleme
app.MapControllers();

app.Run();
