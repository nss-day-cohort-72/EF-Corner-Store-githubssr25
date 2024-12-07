namespace CornerStore.Models.DTOs;

public class CashierDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";

    public List<int> OrderIds { get; set; } = new(); // List of associated order IDs
}
