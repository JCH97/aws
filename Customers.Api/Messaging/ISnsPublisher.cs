using Amazon.SimpleNotificationService.Model;

namespace Customers.Api.Messaging;

public interface ISnsPublisher
{
    Task<PublishResponse> PublishMessageAsync<T>(T message);
}
