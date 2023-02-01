using Amazon.Runtime;
using Amazon.SQS;
using MediatR;
using Weather.API.Consumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<WeatherQueue>(builder.Configuration.GetSection(WeatherQueue.Key));
builder.Services.Configure<SqsEnvs>(builder.Configuration.GetSection(SqsEnvs.Key));

var credentials = new BasicAWSCredentials("", "");
builder.Services.AddSingleton<IAmazonSQS>(_ => new AmazonSQSClient(credentials, Amazon.RegionEndpoint.USEast1));

builder.Services.AddHostedService<QueueConsumerService>();

builder.Services.AddMediatR(typeof(Program));

var app = builder.Build();

app.Run();