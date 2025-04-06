using Microsoft.EntityFrameworkCore;
using FormSubmissionApi.Data;
using FormSubmissionApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<FormSubmissionContext>(options =>
    options.UseInMemoryDatabase("FormSubmissions"));

builder.Services.AddScoped<ISubmissionService, SubmissionService>();

var app = builder.Build();

app.UseStaticFiles();

app.UseDefaultFiles();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<FormSubmissionContext>();
    DbInitializer.Initialize(context);
}

app.MapFallbackToFile("index.html");

await app.RunAsync();
