using EverlyHealth.Business;
using EverlyHealth.Repository;
using EverlyHealth.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMemberLogic, MemberLogic>();
builder.Services.AddScoped<IScrapper, Scrapper>();
builder.Services.AddSingleton<IMemberRepository, MemberRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
