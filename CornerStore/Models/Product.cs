namespace CornerStore.Models;

public class Product
{
    public int Id { get; set; } // Primary key
    public string ProductName { get; set; } = string.Empty; // Not nullable
    public decimal Price { get; set; } // Not nullable (default for value types)
    public string Brand { get; set; } = string.Empty; // Not nullable
    public int CategoryId { get; set; } // Not nullable foreign key
    public Category Category { get; set; } = null!; // Navigation property, nullability handled

    public List<OrderProduct> OrderProducts { get; set; } = new(); // Navigation property
    //having = new{} is important MEANS WHEN THIS IS INSTNATIATED ORDERPRODUCTS WILL DEFAULT TO EMPTY
    //NOT NULL EVNE IF WE DONT SPECIFY IT 

}
