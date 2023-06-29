using AutoMapper;
using HW_7_8.DAL.Repositories;
using HW_7_8.DAL.Database;
using HW_7_8.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using HW_7_8.BLL.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

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

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "HW_7_8", Version = "v2" });
});

var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "HW_7_8");
});
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