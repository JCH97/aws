using System.Text.Json;
using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using SqsPublisher;

var credentials = new BasicAWSCredentials("", "");

var awsClient = new AmazonSQSClient(credentials, RegionEndpoint.USEast1);

var queueUrl = await awsClient.GetQueueUrlAsync("customers");

var customer = new CustomerCreated("Jose", "Hdez", 21);

var sendMessage = new SendMessageRequest
{
    QueueUrl = queueUrl.QueueUrl,
    MessageBody = JsonSerializer.Serialize(customer),

    // attributes => By using this field we be able to indicate the event-type, it could be util to filter the messages in consumers 
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType", new MessageAttributeValue() { DataType = "String", StringValue = "CustomerCreated" }
        }, // key - value
    },
};

await awsClient.SendMessageAsync(sendMessage);

Console.WriteLine("Message sent");

namespace SqsPublisher
{
    public record CustomerCreated(string name, string lastName, int age);
}