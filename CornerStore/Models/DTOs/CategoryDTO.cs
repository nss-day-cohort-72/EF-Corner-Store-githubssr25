namespace CornerStore.Models.DTOs;

public class CategoryDTO
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;

    public List<int> ProductIds { get; set; } = new(); // List of product IDs in the category
}
