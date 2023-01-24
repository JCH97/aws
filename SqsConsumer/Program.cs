using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

var cts = new CancellationTokenSource();

var credentials = new BasicAWSCredentials("AKIAXPGQ73HA3F62LPH6", "hk+keLq5JWUr6Ea/9iuxLmnynSrVcxnQM8bR5rKZ");
var awsClient = new AmazonSQSClient(credentials, RegionEndpoint.USEast1);

var queueUrl = await awsClient.GetQueueUrlAsync("customers");

var receiveMessageRequest = new ReceiveMessageRequest
{
    QueueUrl = queueUrl.QueueUrl,
    AttributeNames = new List<string> { "All" },
    MessageAttributeNames = new List<string>
        { "All" } // retrieve all attributes, it could be util in order to facilitate events in consumers
};

while (!cts.IsCancellationRequested)
{
    var response = await awsClient.ReceiveMessageAsync(receiveMessageRequest, cts.Token);

    foreach (var message in response.Messages)
    {
        Console.WriteLine($"Message Id: {message.MessageId}");
        Console.WriteLine($"Message Body: {message.Body}");

        // await awsClient.DeleteMessageAsync(queueUrl.QueueUrl, message.ReceiptHandle);
    }

    // await Task.Delay(3000);
}

public record CustomerCreated(string name, string lastName, int age);