namespace OpenLibs.SeedWork;

public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
    Guid EventId { get; }
    string Type { get; }
    Guid AggregateId { get; }
}
