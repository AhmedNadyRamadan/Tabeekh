namespace Tabeekh.Models
{
    public class MealOrderItem
    {
        public Guid MealId { get; set; }
        public int Quantity { get; set; }
    }

    public class StripeMealOrderRequest
    {
        public List<MealOrderItem> Items { get; set; } = new();
    }
}
