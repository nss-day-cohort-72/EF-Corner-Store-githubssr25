using CornerStore.Models;
using CornerStore.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CornerStore.Mapping;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));



// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core and provides dummy value for testing
builder.Services.AddNpgsql<CornerStoreDbContext>(builder.Configuration["CornerStoreDbConnectionString"] ?? "testing");


// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//endpoints go here

app.MapPost("/cashiers", async (CornerStoreDbContext db, CreateCashierDTO createCashierDTO) =>
{
    // Validate the input
    if (string.IsNullOrWhiteSpace(createCashierDTO.FirstName) || string.IsNullOrWhiteSpace(createCashierDTO.LastName))
    {
        return Results.BadRequest("FirstName and LastName are required.");
    }

    try
    {
        // Map the DTO to the Cashier entity
        var newCashier = new Cashier
        {
            FirstName = createCashierDTO.FirstName,
            LastName = createCashierDTO.LastName
        };

        // Add the new cashier to the database
        db.Cashiers.Add(newCashier);
        await db.SaveChangesAsync();

        // Return the created cashier with 201 Created
        return Results.Created($"/cashiers/{newCashier.Id}", newCashier);
    }
    catch (Exception ex)
    {
        // Handle unexpected errors
        return Results.Problem("An error occurred while adding the cashier: " + ex.Message);
    }
});

//public class Cashier
//     public int Id { get; set; } // Primary key
//     public string FirstName { get; set; } = string.Empty; // Not nullable
//     public string LastName { get; set; } = string.Empty; // Not nullable
//     public string FullName => $"{FirstName} {LastName}"; // Computed property
//     public List<Order> Orders { get; set; } = new List<Order>(); // Navigation property

// public class Order
//     public int Id { get; set; } // Primary key
//     public int CashierId { get; set; } // Foreign key
//     public Cashier Cashier { get; set; } = null!; // Navigation property

//     public decimal Total => OrderProducts.Sum(op => op.Product.Price * op.Quantity); // Computed property

//     public DateTime? PaidOnDate { get; set; } // Nullable
//     public List<OrderProduct> OrderProducts { get; set; } = new(); // Navigation property

    // public class OrderProduct
    //     public int OrderId { get; set; } // Composite Key Part 1
    //     public int ProductId { get; set; } // Composite Key Part 2
    //     public int Quantity { get; set; }
    //     public Order Order { get; set; } = null!; // Navigation Property
    //     public Product Product { get; set; } = null!; // Navigation Property

// public class Product
//     public int Id { get; set; } // Primary key
//     public string ProductName { get; set; } = string.Empty; // Not nullable
//     public decimal Price { get; set; } // Not nullable (default for value types)
//     public string Brand { get; set; } = string.Empty; // Not nullable
//     public int CategoryId { get; set; } // Not nullable foreign key
//     public Category Category { get; set; } = null!; // Navigation property, nullability handled
//     public List<OrderProduct> OrderProducts { get; set; } = new(); // Navigation property



app.MapGet("/cashiers", async (CornerStoreDbContext db, IMapper mapper) => 
{
    // Step 1: Load cashiers with their related orders, order products, and products
    var cashiers = await db.Cashiers
        .Include(c => c.Orders)
            .ThenInclude(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                    .ThenInclude(p => p.Category) // Single chain for product & category
        .ToListAsync();

    // Step 2: Create an anonymous object, following the CashierDTO-like structure but also including Orders and Products
    var anonymousDTOs = cashiers.Select(c => new 
    {
        Id = c.Id,
        FirstName = c.FirstName,
        LastName = c.LastName,
        FullName = $"{c.FirstName} {c.LastName}",
        OrderIds = c.Orders.Select(o => o.Id).ToList(), // Match the List<int> OrderIds required by CashierDTO

        // Orders with products directly included
        Orders = c.Orders.Select(o => new OrderDTO 
        {
            Id = o.Id,
            CashierId = o.CashierId,
            PaidOnDate = o.PaidOnDate,
            Total = o.OrderProducts.Sum(op => op.Product.Price * op.Quantity),

            // Combine Product Info directly into the Products list
            Products = o.OrderProducts.Select(op => new ProductDTO 
            {
                Id = op.Product.Id,
                ProductName = op.Product.ProductName,
                Price = op.Product.Price,
                Brand = op.Product.Brand,
                CategoryId = op.Product.CategoryId,
                CategoryName = op.Product.Category.CategoryName,
                Quantity = op.Quantity // Attach the quantity directly into the product details
            }).ToList()
        }).ToList()
    });

    return Results.Ok(anonymousDTOs);
});



// app.MapGet("/cashiers", async (CornerStoreDbContext db, IMapper mapper) => 
// {
//     // Step 1: Load cashiers with their related orders, order products, and products
//     var cashiers = await db.Cashiers
//         .Include(c => c.Orders)
//             .ThenInclude(o => o.OrderProducts)
//                 .ThenInclude(op => op.Product)
//         .Include(c => c.Orders)
//             .ThenInclude(o => o.OrderProducts)
//                 .ThenInclude(op => op.Product.Category)
//         .ToListAsync();

//     // Step 2: Create an anonymous object, following the CashierDTO-like structure but also including Orders and Products
//     var anonymousDTOs = cashiers.Select(c => new 
//     {
//         Id = c.Id,
//         FirstName = c.FirstName,
//         LastName = c.LastName,
//         FullName = $"{c.FirstName} {c.LastName}",
//         OrderIds = c.Orders.Select(o => o.Id).ToList(), // Match the List<int> OrderIds required by CashierDTO
//         Orders = c.Orders.Select(o => new OrderDTO 
//         {
//             Id = o.Id,
//             CashierId = o.CashierId,
//             PaidOnDate = o.PaidOnDate,
//             Total = o.OrderProducts.Sum(op => op.Product.Price * op.Quantity),
//             OrderProducts = o.OrderProducts.Select(op => new OrderProduct 
//             {
//                 OrderId = op.OrderId,
//                 ProductId = op.ProductId,
//                 Quantity = op.Quantity
//             }).ToList()
//         }).ToList(),
//         Products = c.Orders
//             .SelectMany(o => o.OrderProducts)
//             .Select(op => new ProductDTO 
//             {
//                 Id = op.Product.Id,
//                 ProductName = op.Product.ProductName,
//                 Price = op.Product.Price,
//                 Brand = op.Product.Brand,
//                 CategoryId = op.Product.CategoryId,
//                 CategoryName = op.Product.Category.CategoryName
//             })
//             .Distinct()
//             .ToList()
//     });

//     return Results.Ok(anonymousDTOs);
// });


// app.MapGet("/cashiers", async (CornerStoreDbContext db, CashierDTO cashierDTO) => {

// var cashiers = db.Cashiers.Include(c => c.Orders)
//                            .ThenInclude(cO => cO.OrderProducts);

// var cashierWithProduct = db.Cashiers.Select(c => new {
//     Id = c.Id,
//     FirstName = c.FirstName,
//     LastName = c.LastName,
//     FullName = c.FullName,
//     Orders = c.Orders.Select( o => new Order {
//         Id = o.Id,
//         CashierId = o.CashierId,
//         Cashier = o.Cashier,
//         PaidOnDate = o.PaidOnDate,
//         OrderProducts = o.OrderProducts
//         }),
//     Products = c.Orders.SelectMany(op => op.OrderProducts)
//                 .Select( oPJoin => new Product {
//                     Id = oPJoin.ProductId,
//                     ProductName = db.Products.FirstOrDefault(p => p.Id == oPJoin.ProductId).ProductName,
//                     Price = db.Products.FirstOrDefault(p => p.Id == oPJoin.ProductId).Price,
//                     Brand = db.Products.FirstOrDefault(p => p.Id == oPJoin.ProductId).Brand,
//                     CategoryId = db.Products.FirstOrDefault(p => p.Id == oPJoin.ProductId).CategoryId,
//                     Category = db.Products.FirstOrDefault(p => p.Id == oPJoin.ProductId).Category,
//                 }).ToList()
//     });

// }
// );


                  

app.Run();

//don't move or change this!
public partial class Program { }