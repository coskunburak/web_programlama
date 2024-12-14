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

// Kestrel yap�land�rmas� (HTTP ve HTTPS deste�i)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // HTTP
    options.ListenAnyIP(5001, listenOptions => listenOptions.UseHttps()); // HTTPS
});

// Rolleri ve admin hesab�n� olu�turmak i�in servis ekle
builder.Services.AddHostedService<RoleSeeder>();

var app = builder.Build();

// Hata y�netimi ve HSTS yap�land�rmas�
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "web_programlama API v1");
    });
}

// Middleware s�ras�
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Varsay�lan route yap�land�rmas�
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
