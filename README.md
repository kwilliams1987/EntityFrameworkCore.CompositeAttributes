# EntityFrameworkCore.CompositeAttributes
Adds support for Composite Entity Keys using attributes to Entity Framework Core.

## Stability
This library is super early access and may not be fully functional.
If you have something to contribute, please do!

## Examples

### DbContext
```csharp
public class ExampleDbContext : CompositeKeyDbContext
{
    public DbSet<CompositeEntity> CompositeEnitities { get; set; }
    public DbSet<CompositeSubEntity> CompositeSubEnitities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(ModelBuilder modelBuilder);
    }
}
```

### Base Entity
```csharp
public class CompositeEnitity
{
    [CompositeKey]
    public Int32 KeyPart1 { get; set; }

    [CompositeKey]
    public Int32 KeyPart2 { get; set; }

    [CompositeInverseProperty(nameof(CompositeSubEntity.CompositeEnitity))]
    public virtual HashSet<CompositeSubEntity> CompositeSubEnitities { get; set; }
}
```

### Dependant Enitity
```csharp
public class CompositeSubEntity
{
    [Key]
    public Int32 Id { get; set; }

    public Int32 ParentKeyPart1 { get; set; }

    public Int32 ParentKeyPart2 { get; set; }

    [CompositeForeignKey(nameof(ParentKeyPart1), nameof(ParentKeyPart2))]
    public virtual CompositeEnitity CompositeEnitity { get; set; }
}
```
