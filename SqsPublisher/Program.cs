using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;
using Amazon;
using Amazon.Runtime;


var credentials = new BasicAWSCredentials("AKIAXPGQ73HA3F62LPH6", "hk+keLq5JWUr6Ea/9iuxLmnynSrVcxnQM8bR5rKZ");

var awsClient = new AmazonSQSClient(credentials, RegionEndpoint.USEast1);

var queueUrl = await awsClient.GetQueueUrlAsync("customers");

var customer = new CustomerCreated("Jose", "Hdez", 21);

var sendMessage = new SendMessageRequest
{
    QueueUrl = queueUrl.QueueUrl,
    MessageBody = JsonSerializer.Serialize(customer),

    // attributes => this field be able to indicate the event-type, it could be util to filter the messages in consumers 
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType", new MessageAttributeValue() { DataType = "String", StringValue = "CustomerCreated" }
        }, // key - value
    },
};

await awsClient.SendMessageAsync(sendMessage);

Console.WriteLine("Message sent");

public record CustomerCreated(string name, string lastName, int age);