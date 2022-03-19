using backend.Models;
using Microsoft.EntityFrameworkCore;


namespace backend.Data;

public class CatalogDBContext : DbContext
{

    public CatalogDBContext(DbContextOptions<CatalogDBContext> options) : base(options) { }
    //Models
    public DbSet<CatalogCategories>? CatalogCategories { get; set; }
    public DbSet<CatalogSubCategories>? CatalogSubCategories { get; set; }
    public DbSet<DiscountCodes>? DiscountCodes { get; set; }
    public DbSet<News>? News { get; set; }
    public DbSet<Order>? Order { get; set; }
    public DbSet<OrderProducts>? OrderProducts { get; set; }
    public DbSet<Product>? Product { get; set; }
    public DbSet<ProductColor>? ProductColor { get; set; }
    public DbSet<ProductImages>? ProductImages { get; set; }
    public DbSet<ProductProperties>? ProductProperties { get; set; }
    public DbSet<ProductSize>? ProductSize { get; set; }
    public DbSet<ProductType>? ProductType { get; set; }
}