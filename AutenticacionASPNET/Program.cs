using AutenticacionASPNET.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(options =>
{
    // Política para menores de edad
    options.AddPolicy("menoresEdad", policy =>
        policy.RequireAssertion(context =>
        {
            var user = context.User;
            if (user.Identity?.IsAuthenticated == true)
            {
                var birthDateClaim = user.FindFirst("FechaNacimiento");
                if (birthDateClaim != null && DateTime.TryParse(birthDateClaim.Value, out DateTime birthDate))
                {
                    var edad = DateTime.Today.Year - birthDate.Year;
                    if (birthDate.Date > DateTime.Today.AddYears(-edad)) edad--;
                    return edad < 18;
                }
            }
            return false;
        }));

    // Política solo para administradores
    options.AddPolicy("SoloAdmin", policy => policy.RequireRole("Admin"));

    // Política para admin o usuario
    options.AddPolicy("AdminOUsuario", policy =>
        policy.RequireRole("Admin", "Usuario"));
});


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    SeedData.Initialize(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
