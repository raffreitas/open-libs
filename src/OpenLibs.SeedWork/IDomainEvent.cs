namespace OpenLibs.SeedWork;

public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}
