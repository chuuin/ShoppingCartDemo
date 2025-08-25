using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCartDemo.Data;
using ShoppingCartDemo.Models;
using Microsoft.AspNetCore.Http;

namespace ShoppingCartDemo.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // 🔹 顯示商品清單 + 搜尋功能
        public async Task<IActionResult> Index(string searchTerm)
        {
            var products = from p in _context.Products select p;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.Name.Contains(searchTerm));
            }

            return View(await products.ToListAsync());
        }

        // 🔹 取得使用者 SessionId（用於區分不同使用者的購物車）
        private string GetSessionId()
        {
            if (!_httpContextAccessor.HttpContext.Session.Keys.Contains("SessionId"))
            {
                _httpContextAccessor.HttpContext.Session.SetString("SessionId", Guid.NewGuid().ToString());
            }
            return _httpContextAccessor.HttpContext.Session.GetString("SessionId");
        }

        // 🔹 加入購物車（支援選擇數量，不跳轉頁面可改用 Ajax）
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            string sessionId = GetSessionId();

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.ProductId == productId && c.SessionId == sessionId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity; // 加上使用者選擇的數量
            }
            else
            {
                _context.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    SessionId = sessionId
                });
            }

            await _context.SaveChangesAsync();

            // 如果不想跳轉頁面，可回 JSON
            return Json(new { success = true, message = "已加入購物車" });
        }
    }
}
