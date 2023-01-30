using LearningAWS.Domain.Env;
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
builder.Services.RegisterEnvVars(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.Services.InitializeDbAsync();

app.Run();