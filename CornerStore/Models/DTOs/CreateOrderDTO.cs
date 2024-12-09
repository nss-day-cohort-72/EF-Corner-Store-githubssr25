namespace CornerStore.Models.DTOs;
public class CreateOrderDTO
{
    public int CashierId { get; set; }

      public Dictionary<int, int> ProductsWithQuantities { get; set; } = new();
      //productId and then quantity 

    public DateTime? PaidOnDate { get; set; }

}
