using HW_7_8.Data.Database;
using HW_7_8.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ExpenseRepository>();
builder.Services.AddScoped<CategoryRepository>();

builder.Services.AddMvc(mvcOptions => {
    mvcOptions.EnableEndpointRouting = false;
});

IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Services.AddDbContext<HomeAccountingDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<HomeAccountingDbContext>();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/auth/login";
});

var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();
app.UseAuthentication();

app.UseMvc(routes => {
    routes.MapRoute(name: "default", template: "{controller-Expenses}/{action-Index}/");
});

using (var scope = app.Services.CreateScope())
{
    HomeAccountingDbContext context = scope.ServiceProvider.GetRequiredService<HomeAccountingDbContext>();
    DbObjects.Initial(context);
}

app.Run();