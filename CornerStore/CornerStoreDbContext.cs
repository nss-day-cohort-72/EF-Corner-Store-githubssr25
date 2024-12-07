using Microsoft.EntityFrameworkCore;
using CornerStore.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
public class CornerStoreDbContext : DbContext
{

    public DbSet<Order> Orders { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Cashier> Cashiers { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<OrderProduct> OrderProducts { get; set; }

    public CornerStoreDbContext(DbContextOptions<CornerStoreDbContext> context) : base(context)
    {


    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }


    //allows us to configure the schema when migrating as well as seed data

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, CategoryName = "Electronics" },
            new Category { Id = 2, CategoryName = "Clothing" },
            new Category { Id = 3, CategoryName = "Home Appliances" }
        );

        modelBuilder.Entity<Product>().HasData(
         new Product { Id = 1, ProductName = "Laptop", Price = 999.99M, Brand = "BrandA", CategoryId = 1 },
         new Product { Id = 2, ProductName = "Smartphone", Price = 799.99M, Brand = "BrandB", CategoryId = 1 },
         new Product { Id = 3, ProductName = "T-Shirt", Price = 19.99M, Brand = "BrandC", CategoryId = 2 },
         new Product { Id = 4, ProductName = "Jeans", Price = 39.99M, Brand = "BrandD", CategoryId = 2 },
         new Product { Id = 5, ProductName = "Blender", Price = 49.99M, Brand = "BrandE", CategoryId = 3 },
         new Product { Id = 6, ProductName = "Microwave", Price = 89.99M, Brand = "BrandF", CategoryId = 3 },
         new Product { Id = 7, ProductName = "Tablet", Price = 399.99M, Brand = "BrandG", CategoryId = 1 },
         new Product { Id = 8, ProductName = "Sweater", Price = 29.99M, Brand = "BrandH", CategoryId = 2 },
         new Product { Id = 9, ProductName = "Headphones", Price = 149.99M, Brand = "BrandI", CategoryId = 1 },
         new Product { Id = 10, ProductName = "Vacuum Cleaner", Price = 129.99M, Brand = "BrandJ", CategoryId = 3 }
     );

        //     // Seed Cashiers
        modelBuilder.Entity<Cashier>().HasData(
            new Cashier { Id = 1, FirstName = "John", LastName = "Doe" },
            new Cashier { Id = 2, FirstName = "Jane", LastName = "Smith" },
            new Cashier { Id = 3, FirstName = "Paul", LastName = "Johnson" },
            new Cashier { Id = 4, FirstName = "Anna", LastName = "Brown" },
            new Cashier { Id = 5, FirstName = "Emily", LastName = "Davis" },
            new Cashier { Id = 6, FirstName = "Mark", LastName = "Wilson" },
            new Cashier { Id = 7, FirstName = "Linda", LastName = "Martinez" }
        );

        //     // Seed Orders
        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, CashierId = 1, PaidOnDate = DateTime.Now },
            new Order { Id = 2, CashierId = 2, PaidOnDate = DateTime.Now.AddMinutes(-30) },
            new Order { Id = 3, CashierId = 3, PaidOnDate = DateTime.Now.AddMinutes(-60) },
            new Order { Id = 4, CashierId = 4, PaidOnDate = DateTime.Now.AddMinutes(-90) },
            new Order { Id = 5, CashierId = 5, PaidOnDate = DateTime.Now.AddMinutes(-120) },
            new Order { Id = 6, CashierId = 6, PaidOnDate = DateTime.Now.AddMinutes(-150) },
            new Order { Id = 7, CashierId = 7, PaidOnDate = DateTime.Now.AddMinutes(-180) }
        );

           modelBuilder.Entity<OrderProduct>().HasData(
    // Products for Order 1
    new OrderProduct { OrderId = 1, ProductId = 1, Quantity = 2 },
    new OrderProduct { OrderId = 1, ProductId = 2, Quantity = 1 },

    // Products for Order 2
    new OrderProduct { OrderId = 2, ProductId = 3, Quantity = 3 },
    new OrderProduct { OrderId = 2, ProductId = 4, Quantity = 2 },

    // Products for Order 3
    new OrderProduct { OrderId = 3, ProductId = 5, Quantity = 1 },
    new OrderProduct { OrderId = 3, ProductId = 6, Quantity = 1 },

    // Products for Order 4
    new OrderProduct { OrderId = 4, ProductId = 9, Quantity = 1 },
    new OrderProduct { OrderId = 4, ProductId = 10, Quantity = 1 },

    // Products for Order 5 (new entries)
    new OrderProduct { OrderId = 5, ProductId = 1, Quantity = 1 },
    new OrderProduct { OrderId = 5, ProductId = 7, Quantity = 2 },

    // Products for Order 6 (new entries)
    new OrderProduct { OrderId = 6, ProductId = 3, Quantity = 2 },
    new OrderProduct { OrderId = 6, ProductId = 8, Quantity = 1 },

    // Products for Order 7 (new entries)
    new OrderProduct { OrderId = 7, ProductId = 4, Quantity = 3 },
    new OrderProduct { OrderId = 7, ProductId = 6, Quantity = 1 }
);



        modelBuilder.Entity<OrderProduct>()
        .HasKey(op => new { op.OrderId, op.ProductId });  // Composite primary key

        modelBuilder.Entity<OrderProduct>()
        .HasOne(oP => oP.Product)
        .WithMany(p => p.OrderProducts)
        .HasForeignKey(oP => oP.ProductId);










    }
}