This is a readme:

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddXmlSerializerFormatters();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});
// app.MapControllers();
app.Run();