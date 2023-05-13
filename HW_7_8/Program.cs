using HW_7_8.Data.Database;
using HW_7_8.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ExpenseRepository>();
builder.Services.AddScoped<CategoryRepository>();

builder.Services.AddMvc(mvcOptions => {
    mvcOptions.EnableEndpointRouting = false;
});

IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Services.AddDbContext<HomeAccountingDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseRouting();

app.UseMvc(routes => {
    routes.MapRoute(name: "default", template: "{controller-Expenses}/{action-Index}/");
});

using (var scope = app.Services.CreateScope())
{
    HomeAccountingDbContext context = scope.ServiceProvider.GetRequiredService<HomeAccountingDbContext>();
    DbObjects.Initial(context);
}

app.UseStaticFiles();

app.Run();