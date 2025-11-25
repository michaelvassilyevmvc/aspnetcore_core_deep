using DepartsCrud.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddSingleton<IDepartmentsRepository, DepartmentsRepository>();

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers["Cache-Control"] = "public, max-age=600";
        ctx.Context.Response.Headers["Expires"] = DateTime.UtcNow.AddMinutes(10)
            .ToString("R");
    }
});
app.UseRouting();
app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            "default",
            "{controller=Home}/{action=Index}/{id?}"
        );
        endpoints.MapRazorPages();
    }
);

app.Run();