namespace ShoppingCartDemo.Models
{
    public class product
    {
        public int Id { get; set; }          // 主鍵
        public string Name { get; set; } = ""; // 商品名稱
        public decimal Price { get; set; }   // 價格
        public string? lmageUrl { get; set; } // 圖片 (可選)
    }
}
