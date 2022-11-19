using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SecurityProject.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<SecurityContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SecurityConection"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.LoginPath = "/Auth/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });


var app = builder.Build();


//Carga inicial de datos
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<SecurityContext>();
    ctx.Database.Migrate();
    if (!ctx.Roles.Any())
    {
        ctx.Roles.Add(new SecurityProject.DB.Models.Rol()
        {
            RolDescription = "Administrador"
        });
        ctx.Roles.Add(new SecurityProject.DB.Models.Rol()
        {
            RolDescription = "Usuario"
        });

    }

    if (!ctx.Users.Any())
    {
        ctx.Users.Add(new SecurityProject.DB.Models.User()
        {
            UserName = "admin",
            UserEmail = "admin@gmail.com",
            UserLastName = "admin",
            UserPassword = "admin",
            RolID = 1
        });
    }

    ctx.SaveChanges();
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
