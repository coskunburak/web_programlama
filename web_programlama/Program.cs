using web_programlama.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL için ApplicationDbContext yapýlandýrmasý
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
.AddRoles<IdentityRole>() // Rol desteði ekle
.AddEntityFrameworkStores<ApplicationDbContext>();

// Swagger dokümantasyonu için gerekli servisleri ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MVC Controller ve View yapýlandýrmasý
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Hata yönetimi ve HSTS yapýlandýrmasý
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Geliþtirme ortamýnda Swagger UI etkinleþtir
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "web_programlama API v1");
    });
}

// HTTPS yönlendirme, statik dosyalar ve yönlendirme
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Identity için kimlik doðrulama middleware'i ekle
app.UseAuthentication();
app.UseAuthorization();

// Varsayýlan route yapýlandýrmasý
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// API Controller'larý için mapleme
app.MapControllers();

app.Run();
