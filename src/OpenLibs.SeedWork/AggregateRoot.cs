namespace OpenLibs.SeedWork;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot() : base() { }

    private readonly List<DomainEvent> _domainEvents = [];
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(DomainEvent @event) => _domainEvents.Add(@event);
    public void ClearDomainEvent() => _domainEvents.Clear();
}