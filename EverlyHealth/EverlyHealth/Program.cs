using EverlyHealth.Business;
using EverlyHealth.Repository;
using EverlyHealth.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DI classes.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMemberLogic, MemberLogic>();
builder.Services.AddScoped<IScrapper, Scrapper>();
builder.Services.AddScoped<ITinyUrl, TinyUrl>();
builder.Services.AddSingleton<IMemberRepository, MemberRepository>();

// Add services to the container.
builder.Services.AddHttpClient();
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
    pattern: "{controller=Member}/{action=Index}/{id?}");

app.Run();
