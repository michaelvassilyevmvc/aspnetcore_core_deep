var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddXmlSerializerFormatters();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        "default",
        "{controller=Home}/{action=Index}/{id?}"
    );
});
// app.MapControllers();
app.Run();