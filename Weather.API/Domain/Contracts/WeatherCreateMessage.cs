using LearningAWS.Application.Dtos;
using LearningAWS.Domain.Entities;
using LearningAWS.Domain.Interfaces;

namespace LearningAWS.Domain.Contracts;

public class WeatherCreateMessage : WeatherDto, IMessage
{
    public Events.Events EventType { get; init; } = Events.Events.WeatherCreated;
}