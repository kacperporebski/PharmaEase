using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmaEase.Data;
using Microsoft.Extensions.DependencyInjection;
using PharmaEase.Models.Seeders;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

//database stuff
builder.Services.AddDbContext<PharmaEaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'Default' not found.")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//role and permissions
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PharmaEaseContext>();

//require everyone to be logged in
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

//views
builder.Services.AddControllersWithViews();

var app = builder.Build();

//put data in database if theres nothing
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
