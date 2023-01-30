using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using LearningAWS.Domain.Interfaces;

namespace LearningAWS.Infra.Events;

public interface ISqsPublisher
{
    Task<SendMessageResponse> Publish<T>(T message, string queueName) where T : IMessage;
}

public class SqsPublisher : ISqsPublisher
{
    private readonly IAmazonSQS _amazonSqs;
    private string? _queueUrl;

    public SqsPublisher(IAmazonSQS amazonSqs)
    {
        _amazonSqs = amazonSqs;
    }

    public async Task<SendMessageResponse> Publish<T>(T message, string queueName) where T : IMessage
    {
        var queueUrl = await GetQueueUrl(queueName);

        var sendMessageRequest = new SendMessageRequest
        {
            QueueUrl = queueUrl,
            MessageBody = JsonSerializer.Serialize(message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    "MessageType", new MessageAttributeValue
                    {
                        DataType = "String",
                        StringValue = typeof(T).Name
                    }
                },
                {
                    "Event", new MessageAttributeValue
                    {
                        DataType = "Int",
                        StringValue =
                            message
                               .GetType()
                               .GetProperty(nameof(IMessage.EventType))?
                               .GetValue(message)?
                               .ToString()
                         ?? String.Empty
                    }
                }
            }
        };

        return await _amazonSqs.SendMessageAsync(sendMessageRequest);
    }

    private async Task<string> GetQueueUrl(string queueName)
    {
        if (string.IsNullOrEmpty(_queueUrl))
            _queueUrl = (await _amazonSqs.GetQueueUrlAsync(queueName)).QueueUrl;

        return _queueUrl;
    }
}