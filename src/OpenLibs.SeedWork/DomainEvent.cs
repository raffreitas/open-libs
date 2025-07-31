namespace OpenLibs.SeedWork;

public abstract record DomainEvent : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
    public Guid EventId { get; } = Guid.NewGuid();
    public string Type => GetType().Name;

    public abstract Guid AggregateId { get; }
}
