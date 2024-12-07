namespace CornerStore.Models;

public class Cashier
{
    public int Id { get; set; } // Primary key
    public string FirstName { get; set; } = string.Empty; // Not nullable
    public string LastName { get; set; } = string.Empty; // Not nullable
    public string FullName => $"{FirstName} {LastName}"; // Computed property

    public List<Order> Orders { get; set; } = new List<Order>(); // Navigation property
}
