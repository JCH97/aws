using System.Globalization;
using System.Text.Json;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using SnsPublisher;

var credential = new BasicAWSCredentials("", "");
var snsClient = new AmazonSimpleNotificationServiceClient(credential, Amazon.RegionEndpoint.USEast1);

var arnTopicResponse = await snsClient.FindTopicAsync("weather");

var msg = new WeatherCreated()
{
    Date = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
    Id = 0,
    Region = "USEast1",
    Summary = "",
    TemperatureC = 20
};

var publishRequest = new PublishRequest
{
    TopicArn = arnTopicResponse.TopicArn,
    Message = JsonSerializer.Serialize(msg),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType",
            new MessageAttributeValue
            {
                DataType = "String",
                StringValue = nameof(WeatherCreated)
            }
        }
    }
};

var response = await snsClient.PublishAsync(publishRequest);