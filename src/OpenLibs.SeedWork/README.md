# OpenLibs.SeedWork

[![NuGet](https://img.shields.io/nuget/v/OpenLibs.SeedWork.svg)](https://www.nuget.org/packages/OpenLibs.SeedWork/)
[![Downloads](https://img.shields.io/nuget/dt/OpenLibs.SeedWork.svg)](https://www.nuget.org/packages/OpenLibs.SeedWork/)

Base implementations and building blocks for Domain-Driven Design (DDD) applications including entities, value objects, domain events, and repositories.

## 🚀 Installation

```bash
dotnet add package OpenLibs.SeedWork
```

## 📋 Features

### Entity Base Class

The `Entity` abstract class provides a foundation for all domain entities with common properties and behavior.

```csharp
public class Product : Entity
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    
    protected Product() { } // For EF Core
    
    public Product(string name, decimal price)
    {
        DomainException.ThrowIfNullOrWhitespace(name, nameof(name));
        DomainException.ThrowIfNegative(price, nameof(price));
        
        Name = name;
        Price = price;
    }
    
    public void UpdatePrice(decimal newPrice)
    {
        DomainException.ThrowIfNegative(newPrice, nameof(newPrice));
        Price = newPrice;
    }
}
```

**Features:**
- ✅ Unique `Id` (Guid) generation
- ✅ `CreatedAt` and `UpdatedAt` timestamps
- ✅ Immutable identity

### Aggregate Root

The `AggregateRoot` class extends `Entity` and provides domain event management capabilities.

```csharp
public class Order : AggregateRoot
{
    private readonly List<OrderItem> _items = [];
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    
    public decimal Total => _items.Sum(x => x.Total);
    
    public void AddItem(string productName, decimal price, int quantity)
    {
        var item = new OrderItem(productName, price, quantity);
        _items.Add(item);
        
        // Raise domain event
        AddDomainEvent(new OrderItemAddedEvent(Id, item.ProductName, item.Quantity));
    }
    
    public void Complete()
    {
        // Business logic here
        AddDomainEvent(new OrderCompletedEvent(Id, Total));
    }
}
```

**Features:**
- ✅ Domain event collection and management
- ✅ `AddDomainEvent()` method for raising events
- ✅ `ClearDomainEvent()` method for cleanup
- ✅ `DomainEvents` readonly collection

### Domain Events

Create domain events by inheriting from the `DomainEvent` record:

```csharp
public record OrderItemAddedEvent(Guid OrderId, string ProductName, int Quantity) : DomainEvent;

public record OrderCompletedEvent(Guid OrderId, decimal Total) : DomainEvent;

public record ProductPriceChangedEvent(Guid ProductId, decimal OldPrice, decimal NewPrice) : DomainEvent;
```

**Features:**
- ✅ Automatic `OccurredOn` timestamp
- ✅ Immutable record structure
- ✅ Implements `IDomainEvent` interface

### Repository Pattern

Define repositories using the provided interfaces:

```csharp
public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task<Order?> GetOrderWithItemsAsync(Guid orderId, CancellationToken cancellationToken = default);
}

public class OrderRepository : IOrderRepository
{
    private readonly DbContext _context;
    
    public OrderRepository(DbContext context)
    {
        _context = context;
    }
    
    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Order>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    
    public async Task AddAsync(Order entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<Order>().AddAsync(entity, cancellationToken);
    }
    
    // ... other implementations
}
```

### Specifications Pattern

Implement business rules using the specification pattern:

```csharp
public class ExpensiveProductSpecification : ISpecification<Product>
{
    private readonly decimal _threshold;
    
    public ExpensiveProductSpecification(decimal threshold = 1000m)
    {
        _threshold = threshold;
    }
    
    public bool IsSatisfiedBy(Product product)
    {
        return product.Price >= _threshold;
    }
}

// Usage
var expensiveSpec = new ExpensiveProductSpecification(500m);
if (expensiveSpec.IsSatisfiedBy(product))
{
    // Handle expensive product logic
}
```

### Domain Exceptions

Use the `DomainException` class for domain validation:

```csharp
public class Product : Entity
{
    public void UpdateName(string newName)
    {
        DomainException.ThrowIfNullOrWhitespace(newName, nameof(newName));
        Name = newName;
    }
    
    public void UpdatePrice(decimal newPrice)
    {
        DomainException.ThrowIfNegative(newPrice, nameof(newPrice));
        Price = newPrice;
    }
    
    public void SetQuantity(int quantity)
    {
        DomainException.ThrowIfNegative(quantity, nameof(quantity));
        Quantity = quantity;
    }
}
```

**Available validation methods:**
- ✅ `ThrowIfNullOrEmpty(string, string)`
- ✅ `ThrowIfNullOrWhitespace(string, string)`
- ✅ `ThrowIfNegative(int, string)`
- ✅ `ThrowIfNegative(decimal, string)`

## 🏗️ Complete Example

Here's a complete example showing how to use the SeedWork components together:

```csharp
// Domain Events
public record ProductCreatedEvent(Guid ProductId, string Name, decimal Price) : DomainEvent;
public record ProductPriceChangedEvent(Guid ProductId, decimal OldPrice, decimal NewPrice) : DomainEvent;

// Specifications
public class AvailableProductSpecification : ISpecification<Product>
{
    public bool IsSatisfiedBy(Product product) => product.Quantity > 0;
}

// Aggregate Root
public class Product : AggregateRoot
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    
    protected Product() { } // For EF Core
    
    public Product(string name, decimal price, int quantity)
    {
        DomainException.ThrowIfNullOrWhitespace(name, nameof(name));
        DomainException.ThrowIfNegative(price, nameof(price));
        DomainException.ThrowIfNegative(quantity, nameof(quantity));
        
        Name = name;
        Price = price;
        Quantity = quantity;
        
        AddDomainEvent(new ProductCreatedEvent(Id, name, price));
    }
    
    public void ChangePrice(decimal newPrice)
    {
        DomainException.ThrowIfNegative(newPrice, nameof(newPrice));
        
        var oldPrice = Price;
        Price = newPrice;
        
        AddDomainEvent(new ProductPriceChangedEvent(Id, oldPrice, newPrice));
    }
    
    public bool IsAvailable()
    {
        var spec = new AvailableProductSpecification();
        return spec.IsSatisfiedBy(this);
    }
}

// Repository
public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetAvailableProductsAsync(CancellationToken cancellationToken = default);
}

// Usage in Application Service
public class ProductService
{
    private readonly IProductRepository _repository;
    
    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Product> CreateProductAsync(string name, decimal price, int quantity)
    {
        var product = new Product(name, price, quantity);
        await _repository.AddAsync(product);
        
        // Domain events can be published here
        foreach (var domainEvent in product.DomainEvents)
        {
            // Publish event to event bus
        }
        
        product.ClearDomainEvent();
        return product;
    }
}
```

## 🎯 Key Benefits

- ✅ **Clean Architecture**: Provides clear separation of domain concerns
- ✅ **Domain Events**: Built-in support for domain event patterns
- ✅ **Validation**: Centralized domain validation with meaningful exceptions
- ✅ **Specifications**: Reusable business rule implementations
- ✅ **Repository Pattern**: Standard interfaces for data access
- ✅ **Immutability**: Encourages immutable domain design
- ✅ **Testability**: Easy to unit test domain logic

## 📚 Documentation

For more information about Domain-Driven Design patterns and best practices, check out:

- [Domain-Driven Design by Eric Evans](https://www.domainlanguage.com/ddd/)
- [.NET Application Architecture Guides](https://docs.microsoft.com/en-us/dotnet/architecture/)
- [Clean Architecture by Robert Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

## 🔗 Related Packages

- **OpenLibs.Extensions** - Configuration and dependency injection extensions

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](../../LICENSE) file for details.
