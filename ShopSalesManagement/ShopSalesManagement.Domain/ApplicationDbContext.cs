using Microsoft.EntityFrameworkCore;

namespace ShopSalesManagement.Domain;

/// <summary>
/// Контекст базы данных для управления продажами и запасами магазина.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductGroup> ProductGroups { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Store> Stores { get; set; }

    /// <summary>
    /// Конструктор для контекста базы данных с опциями.
    /// </summary>
    /// <param name="options">Опции для конфигурации контекста.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Настройка модели базы данных с помощью Fluent API.
    /// </summary>
    /// <param name="modelBuilder">Построитель моделей.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Пример настройки сущностей. Можно добавлять ограничения, индексы, связи и т.д.

        // Настройка связи между Product и ProductGroup
        modelBuilder.Entity<Product>()
            .HasOne<ProductGroup>()
            .WithMany()
            .HasForeignKey(p => p.ProductGroupId);

        // Настройка связи между Purchase и Sale
        modelBuilder.Entity<Purchase>()
            .HasOne<Sale>()
            .WithMany()
            .HasForeignKey(p => p.SaleId);

        // Настройка связи между Purchase и Product
        modelBuilder.Entity<Purchase>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(p => p.ProductId);

        // Настройка связи между Sale и Customer
        modelBuilder.Entity<Sale>()
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey(s => s.CustomerId);

        // Настройка связи между Stock и Product
        modelBuilder.Entity<Stock>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(s => s.ProductId);

        // Настройка связи между Stock и Store
        modelBuilder.Entity<Stock>()
            .HasOne<Store>()
            .WithMany()
            .HasForeignKey(s => s.StoreId);

        // Настройка связи между Sale и Store
        modelBuilder.Entity<Sale>()
            .HasOne<Store>()
            .WithMany()
            .HasForeignKey(s => s.StoreId);
    }
}
