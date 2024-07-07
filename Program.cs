using Microsoft.EntityFrameworkCore;
using RazorLoginRegister.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapRazorPages();

app.MapGet("/", async context =>
{
    var userId = context.Session.GetInt32("UserId");
    if (userId == null)
    {
        context.Response.Redirect("/Login");
    }
    else
    {
        context.Response.Redirect("/MoneyFlow");
    }
});

app.Run();
