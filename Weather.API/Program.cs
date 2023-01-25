using LearningAWS.Infra;
using LearningAWS.Infra.Database;
using LearningAWS.Infra.Installers;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

// Add services to the container.
builder.Services.Install(builder.Configuration, typeof(IMarkerApi));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
await databaseInitializer.InitializeAsync();

app.Run();