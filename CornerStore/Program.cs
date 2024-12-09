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



//3rd endpoint in assginment 

app.MapGet("/products", async (CornerStoreDbContext db, string? search) => 
{
    var products = await db.Products
        .Include(p => p.Category) // Ensure category details are available
        .Where(p => 
            string.IsNullOrWhiteSpace(search) || 
            p.ProductName.ToLower().Contains(search!.ToLower()) || 
            p.Category.CategoryName.ToLower().Contains(search!.ToLower())
        )
        .Select(p => new ProductDTO
        {
            Id = p.Id,
            ProductName = p.ProductName,
            Price = p.Price,
            Brand = p.Brand,
            CategoryId = p.CategoryId,
            CategoryName = p.Category.CategoryName
        })
        .ToListAsync();

    return Results.Ok(products);
});


app.MapPost("/products", async (CornerStoreDbContext db, CreateProductDTO newProductDTO, IMapper mapper) => 
{
    // Step 1: Validate input
    if (string.IsNullOrWhiteSpace(newProductDTO.ProductName) || newProductDTO.Price <= 0 || newProductDTO.Quantity < 0)
    {
        return Results.BadRequest("ProductName is required, Price must be greater than 0, and Quantity cannot be negative.");
    }

    try
    {
        var newProduct = mapper.Map<Product>(newProductDTO);

        db.Products.Add(newProduct);
        await db.SaveChangesAsync();

        // Step 4: Return the newly created product as a DTO
        var createdProductDTO = mapper.Map<ProductDTO>(newProduct);
        return Results.Created($"/products/{newProduct.Id}", createdProductDTO);
    }
    catch (Exception ex)
    {
        return Results.Problem("An error occurred while adding the product: " + ex.Message);
    }
});


app.MapPut("/products/{id}", async (CornerStoreDbContext db, int id, UpdateProductDTO updateDTO, IMapper mapper) =>
{
    var existingProduct = await db.Products.FindAsync(id);
    if (existingProduct == null)
    {
        return Results.NotFound($"Product with id {id} not found.");
    }

    try
    {
        mapper.Map(updateDTO, existingProduct);
        await db.SaveChangesAsync();

        var updatedProductDTO = mapper.Map<ProductDTO>(existingProduct);
        return Results.Ok(updatedProductDTO);
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred while updating the product: {ex.Message}");
    }
});


// /orders
// Get an order details, including the cashier, order products, and products on the order with their category.

app.MapGet("/orders/{orderId}", async (CornerStoreDbContext db, int orderId, IMapper mapper) =>
{

var ourOrder = await db.Orders
    .Include(o => o.Cashier)
        .ThenInclude(c => c.Orders) // Include the Orders of the Cashier
            .ThenInclude(co => co.OrderProducts) // Include the OrderProducts of the Cashier's Orders
    .Include(o => o.OrderProducts) // Include the OrderProducts for the main order
        .ThenInclude(op => op.Product) // Include the Product for the OrderProducts
        .ThenInclude(p => p.Category) // category was null befoer including this 
    .FirstOrDefaultAsync(o => o.Id == orderId);

//structure returned this is also why you dont need to do a list for op.Product evne if multiple products only 1 per orderProduct
// { "Id": 1,
//     "CashierId": 1,
//     "PaidOnDate": "2024-12-09T10:00:00",
//     "OrderProducts": [
//         {
//             "OrderId": 1,
//             "ProductId": 1,
//             "Quantity": 2,
//             "Product": {
//                 "Id": 1,
//                 "ProductName": "Laptop",
//                 "Price": 999.99,
//                 "Brand": "BrandA",
//                 "CategoryId": 1
//             }   }, {
//             "OrderId": 1,
//             "ProductId": 2,
//             "Quantity": 1,
//             "Product": {
//                 "Id": 2,
//                 "ProductName": "Smartphone",
//                 "Price": 799.99,
//                 "Brand": "BrandB",
//                 "CategoryId": 1
//             }    }  ] }


// WRONG:   .ThenInclude(oProducts => await db.Products.FindAsync(eachProduct => eachProduct.Id == oProducts.ProductId).ToListAsync());

var ourOrderDTO = new {
        OrderId = ourOrder.Id,
        CashierId = ourOrder.CashierId, 
        PaidOnDate = ourOrder.PaidOnDate,
        OrderProductDTO = ourOrder.OrderProducts.Select(op => new {
            OrderId = op.OrderId,
            ProductId = op.ProductId,
            Quantity = op.Quantity,
            ProductDTO = new {
                Id = op.Product.Id,
                ProductName = op.Product.ProductName,
                Price = op.Product.Price,
                Brand = op.Product.Brand,
                CategoryId = op.Product.CategoryId,
                CategoryName = op.Product.Category != null ? op.Product.Category.CategoryName : null
            }
        }).ToList(),
        CashierDTO = mapper.Map<CashierDTO>(ourOrder.Cashier),
        ProductDTOs = mapper.Map<List<ProductDTO>>(ourOrder.OrderProducts.Select(op => op.Product).ToList()),
        };

return Results.Ok(ourOrderDTO);  

});

//Get all orders. Check for a query string param orderDate that only returns orders from a particular day. If it is not present, return all orders.

app.MapGet("/orders", async (CornerStoreDbContext db, string? orderDate, IMapper mapper) =>
{
    var ourOrder = await db.Orders
    .Include(o => o.Cashier)
        .ThenInclude(c => c.Orders) // Include the Orders of the Cashier
            .ThenInclude(co => co.OrderProducts) // Include the OrderProducts of the Cashier's Orders
    .Include(o => o.OrderProducts) // Include the OrderProducts for the main order
        .ThenInclude(op => op.Product) // Include the Product for the OrderProducts
        .ThenInclude(p => p.Category) 
        .ToListAsync();

    if(!string.IsNullOrWhiteSpace(orderDate) && DateTime.TryParse(orderDate, out DateTime parsedDate)){
        ourOrder.Where(o => o.PaidOnDate.HasValue && o.PaidOnDate.Value.Date == parsedDate.Date).ToList();

    }

               
});

app.MapDelete("/orders/{id}", async (CornerStoreDbContext db, int id, IMapper mapper) =>
{

var ourOrder = await db.Orders
    .Include(o => o.Cashier)
        .ThenInclude(c => c.Orders) // Include the Orders of the Cashier
            .ThenInclude(co => co.OrderProducts) // Include the OrderProducts of the Cashier's Orders
    .Include(o => o.OrderProducts) // Include the OrderProducts for the main order
        .ThenInclude(op => op.Product) // Include the Product for the OrderProducts
        .ThenInclude(p => p.Category) // category was null befoer including this 
    .FirstOrDefaultAsync(o => o.Id == id);

    if (ourOrder == null) return Results.NotFound();

    // Map the order to a DTO before deletion (if you have a DTO setup)
var deletedOrderDTO = mapper.Map<OrderDTO>(ourOrder);

//Get all products associated with the specified OrderId then remove them 
var products = await db.Products.Where(p => p.OrderProducts.Any(op => op.OrderId == id)).ToListAsync();
products.ForEach(product => product.OrderProducts.RemoveAll(op => op.OrderId == id));

var cashier = await db.Cashiers.Include(c => c.Orders)
                                .Where(cOrders => cOrders.Orders.Any(co => co.Id == id))
                                .FirstOrDefaultAsync();

cashier?.Orders.Remove(cashier.Orders.FirstOrDefault(order => order.Id == id));

//wont work cashier?.Orders.Remove(order => order.Id == id); 
//Remove() expects a single item (an object) to be passed as an argument.order => order.Id == id is a predicate (condition), not an object.
// Remove() only works when you FirstOrDefault():
// FirstOrDefault finds the specific order in the list of orders that matches the condition (order.Id == id).
// If it finds it, it returns the specific order object.pass the exact instance (object reference) you want to remove.

ourOrder.OrderProducts.Remove(ourOrder.OrderProducts.FirstOrDefault(opJoin => opJoin.OrderId == id));

//.Remove wont work need removeRange anytime want to remove multiple items 
db.OrderProducts.RemoveRange(db.OrderProducts.Where(op => op.OrderId == ourOrder.Id));

db.Orders.Remove(ourOrder);

// Save once
await db.SaveChangesAsync();


return Results.Ok(new {
    Message = "Order deleted successfully",
    DeletedOrder = deletedOrderDTO
});

});


//    public int CashierId { get; set; }
    // public DateTime? PaidOnDate { get; set; }

app.MapPost("/orders", async (CornerStoreDbContext db, CreateOrderDTO newOrderDTO, IMapper mapper) => 
{

// Step 1: Validate input
    if (!newOrderDTO.PaidOnDate.HasValue || newOrderDTO.PaidOnDate.Value == default)
    {
        return Results.BadRequest("PaidOnDate is required and must be a valid date.");
    }
// newOrderDTO.PaidOnDate.Value == default:
// If PaidOnDate has a value, this checks whether the value is the "default" value for DateTime, i.e., 01/01/0001 00:00:00.
// The purpose of this line is to ensure that:
// PaidOnDate is not null, and
// The value of PaidOnDate is not an invalid "default" date like 01/01/0001, which could occur if the value was initialized but not set correctly.

//int productId = ProductsWithQuantities.Keys.First();
//int quantity = ProductsWithQuantities.Values.First();



var cashier = db.Cashiers.FirstOrDefault(eachCashier => eachCashier.Id == newOrderDTO.CashierId);

var ourOrder = new Order {
    CashierId = newOrderDTO.CashierId,
    Cashier = cashier,
    PaidOnDate = newOrderDTO.PaidOnDate,
};

db.Orders.Add(ourOrder); 
await db.SaveChangesAsync();

// Get all products at once (batch query)
var productIds = newOrderDTO.ProductsWithQuantities.Keys.ToList();
var products = await db.Products.Include(product =>product.Category).Where(product => productIds.Contains(product.Id)).ToListAsync();

// Create OrderProducts
var ourOPJoinTable = newOrderDTO.ProductsWithQuantities.Select(
    eachProductQuantity => new OrderProduct {
        OrderId = ourOrder.Id,
        ProductId = eachProductQuantity.Key,
        Quantity = eachProductQuantity.Value,
        Order = ourOrder, // This tells EF Core to associate the Order
        Product = products.FirstOrDefault(product => product.Id == eachProductQuantity.Key) 
    }).ToList();

// foreach(var eachOP in ourOPJoinTable){
//     db.OrderProducts.Add(eachOP);
// };

    db.OrderProducts.AddRange(ourOPJoinTable);
    await db.SaveChangesAsync(); // Save all changes

    // Return the created order as an OrderDTO
    var orderDTO = mapper.Map<OrderDTO>(ourOrder);
    return Results.Created($"/orders/{ourOrder.Id}", orderDTO); 



// var products = db.Products.Select(eachProduct => eachProduct.OrderProducts.Where(eachOP => eachOP.ProductId.Contains()).ToList());

// var productsForJoin = db.Products.Where(eachProduct => eachProduct.OrderProducts.Any(
//     eachOP => ourOPJoinTable.Any(eachOPJoin => eachOPJoin.ProductId == eachOP.ProductId)));

// foreach(var product in productsForJoin){
//     product.OrderProducts.AddRange(ourOPJoinTable.Where(
//         ourOPJoin => ourOPJoin.ProductId == product.Id));
// }


});





app.Run();

//don't move or change this!
public partial class Program { }