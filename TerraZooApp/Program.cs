using Microsoft.EntityFrameworkCore;
using TerraZooApp.Data;
using TerraZooApp.Repositories;
using TerraZooApp.Services;
using Microsoft.AspNetCore.Identity;
using TerraZooApp.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession(); 
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TerraZooAppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<ITerraZooScheduleRepository, TerraZooScheduleRepository>();
builder.Services.AddScoped<ITerraZooScheduleService, TerraZooScheduleService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
builder.Services.AddScoped<ITicketTypeService, TicketTypeService>();
builder.Services.AddScoped<ITourRepository, TourRepository>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<ITourGuideScheduleRepository, TourGuideScheduleRepository>();
builder.Services.AddScoped<ITourGuideScheduleService, TourGuideScheduleService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();


// Add Identity services
builder.Services.AddIdentity<Users, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
})
.AddEntityFrameworkStores<TerraZooAppDbContext>()
.AddDefaultTokenProviders();

// Add AuthService
builder.Services.AddScoped<IUsersAuthService, UsersAuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UsersAuth}/{action=Login}/{id?}")
    .WithStaticAssets();

var roles = new[] { "admin", "tour guide", "visitor" };

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.Run();
