using System.ComponentModel.DataAnnotations;

namespace CornerStore.Models;

public class Category
{
    public int Id { get; set; } // Primary key
    public string CategoryName { get; set; } = string.Empty; // Not nullable

    public List<Product> Products { get; set;} = new List<Product>();
 }
