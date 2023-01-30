namespace LearningAWS.Domain.Interfaces;

public interface IMessage
{
    public Events.Events EventType { get; init; }
}