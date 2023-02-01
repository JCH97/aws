using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.SQS;
using Amazon.SQS.Model;
using MediatR;
using Microsoft.Extensions.Options;
using Weather.API.Consumer.Contracts;

namespace Weather.API.Consumer;

public class QueueConsumerService : BackgroundService
{
    private readonly IMediator _mediator;
    private readonly IAmazonSQS _amazonSqs;
    private readonly IOptions<WeatherQueue> _options;
    private readonly ILogger<QueueConsumerService> _logger;

    public QueueConsumerService(IAmazonSQS amazonSqs, IOptions<WeatherQueue> options, IMediator mediator,
                                ILogger<QueueConsumerService> logger)
    {
        _amazonSqs = amazonSqs;
        _options = options;
        _mediator = mediator;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queueUrl = await _amazonSqs.GetQueueUrlAsync(_options.Value.Name, stoppingToken);

        var receiveMessageRequest = new ReceiveMessageRequest
        {
            QueueUrl = queueUrl.QueueUrl,
            AttributeNames = new List<string> { "All" },
            MessageAttributeNames = new List<string>
                { "All" }, // retrieve all attributes, it could be util in order to facilitate events in consumers
            MaxNumberOfMessages = 1
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            var receiveMessageResponse =
                await _amazonSqs.ReceiveMessageAsync(receiveMessageRequest, stoppingToken);


            foreach (var msg in receiveMessageResponse.Messages)
            {
                var messageType = msg.MessageAttributes["MessageType"].StringValue!;

                var type = Type.GetType($"Weather.API.Consumer.MediatR.{messageType}Message");

                if (type is null)
                {
                    // Don't delete message at this point due to dead letter queue
                    _logger.LogError("Message type not found: {MessageType}", messageType);
                    continue;
                }

                var body = (ISqsMessage)JsonSerializer.Deserialize(msg.Body, type)!;

                try
                {
                    await _mediator.Send(body, stoppingToken);
                }
                catch (Exception e)
                {
                    // Don't delete message at this point due to dead letter queue
                    _logger.LogError("Error while processing message: {MessageType}", messageType);
                    continue;
                }

                // Everything was ok, so delete message
                await _amazonSqs.DeleteMessageAsync(queueUrl.QueueUrl, msg.ReceiptHandle, stoppingToken);
            }


            await Task.Delay(1000, stoppingToken);
        }
    }
}