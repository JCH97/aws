using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.S3;
using S3.Customers.Api.Validation;
using FluentValidation.AspNetCore;
using Microsoft.Net.Http.Headers;
using S3.Customers.Api.Repositories;
using S3.Customers.Api.Services;
using S3.Customers.Api.Validation;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

var config = builder.Configuration;
config.AddEnvironmentVariables("CustomersApi_");

builder.Services.AddControllers().AddFluentValidation(x =>
{
    x.RegisterValidatorsFromAssemblyContaining<Program>();
    x.DisableDataAnnotationsValidation = true;
});
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAmazonS3>(s => new AmazonS3Client(
    new BasicAWSCredentials("", ""),
    RegionEndpoint.USEast1
));

builder.Services.AddSingleton<IImageService, ImageService>();

builder.Services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddSingleton<IGitHubService, GitHubService>();

builder.Services.AddHttpClient("GitHub", httpClient =>
{
    httpClient.BaseAddress = new Uri(config.GetValue<string>("GitHub:ApiBaseUrl")!);
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/vnd.github.v3+json");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.UserAgent, $"Course-{Environment.MachineName}");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ValidationExceptionMiddleware>();
app.MapControllers();

app.Run();