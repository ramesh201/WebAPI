using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Added Database Context
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
builder.Services.AddEntityFrameworkMySql()
                .AddDbContext<DBSchoolContext>((serviceProvider, options) =>
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion)
                       .UseInternalServiceProvider(serviceProvider)
                       );

//Add Cors to resolve Origin-Allow error
builder.Services.AddCors(options =>
{

    options.AddPolicy(

    name: "AllowOrigin",

    builder => {

        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

    });

});

var app = builder.Build();

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
//use Cors here
app.UseCors("AllowOrigin");
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

