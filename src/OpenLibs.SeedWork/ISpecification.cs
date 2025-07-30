namespace OpenLibs.SeedWork;
public interface ISpecification<T> where T : Entity
{
    bool IsSatisfiedBy(T entity);
}
