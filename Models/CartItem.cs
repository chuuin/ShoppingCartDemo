namespace ShoppingCartDemo.Models
{
    public class CartItem
    {
        public int Id { get; set; }             // CartItem 主鍵
        public int ProductId { get; set; }      // 對應的商品 Id
        public int Quantity { get; set; } = 1;  // 購買數量，預設 1

        public product? Product { get; set; }   // 導覽屬性，連結商品

        public string SessionId { get; set; } = string.Empty; // 使用者 SessionId，用來區分不同使用者的購物車
    }
}
