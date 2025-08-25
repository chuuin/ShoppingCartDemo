using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCartDemo.Data;
using ShoppingCartDemo.Models; // 確認有 CartItem 與 Product 模型
using Microsoft.AspNetCore.Http;

namespace ShoppingCartDemo.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // 建構子注入 DbContext 與 HttpContextAccessor
        public CartController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // 🔹 取得使用者 SessionId，用於區分不同使用者的購物車
        private string GetSessionId()
        {
            // 如果 SessionId 不存在，先建立新的 GUID
            if (!_httpContextAccessor.HttpContext.Session.Keys.Contains("SessionId"))
            {
                _httpContextAccessor.HttpContext.Session.SetString("SessionId", Guid.NewGuid().ToString());
            }

            // 回傳 SessionId
            return _httpContextAccessor.HttpContext.Session.GetString("SessionId");
        }

        // 🔹 顯示購物車頁面
        public async Task<IActionResult> Index()
        {
            string sessionId = GetSessionId();

            // 取得目前使用者購物車所有項目，並包含 Product 資料
            var cart = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.SessionId == sessionId)
                .ToListAsync();

            return View(cart); // 傳給 View 顯示
        }

        // 🔹 加入商品到購物車

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            string sessionId = GetSessionId();

            // 找出目前使用者購物車是否已經有該商品
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.ProductId == productId && c.SessionId == sessionId);

            if (cartItem != null)
            {
                // 已存在 → 增加數量
                cartItem.Quantity += quantity;
            }
            else
            {
                // 不存在 → 新增購物車項目
                _context.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    SessionId = sessionId
                });
            }

            await _context.SaveChangesAsync();

            // 回傳 JSON 成功訊息給前端 AJAX 使用
            return Ok(new { success = true, productId = productId, quantity = quantity });
        }



        // 🔹 刪除購物車項目
        public async Task<IActionResult> Remove(int id)
        {
            string sessionId = GetSessionId();

            // 依據 ProductId 與 SessionId 找到購物車項目
            var item = await _context.CartItems
                .FirstOrDefaultAsync(c => c.ProductId == id && c.SessionId == sessionId);

            if (item != null)
            {
                _context.CartItems.Remove(item); // 移除該項目
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index"); // 回到購物車頁面
        }

        // 🔹 更新購物車商品數量（來自表單提交）
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int id, int quantity)
        {
            if (quantity < 1) quantity = 1; // 最小數量 1

            string sessionId = GetSessionId();

            // 找到購物車項目
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.ProductId == id && c.SessionId == sessionId);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity; // 更新數量
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index"); // 回到購物車頁面
        }

        // 🔹 結帳功能
        public async Task<IActionResult> Checkout()
        {
            string sessionId = GetSessionId();

            // 取得目前使用者購物車所有項目
            var items = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.SessionId == sessionId)
                .ToListAsync();

            if (!items.Any())
            {
                // 購物車空的提示
                TempData["Message"] = "購物車是空的";
                return RedirectToAction("Index");
            }

            // 這裡可加入實際結帳邏輯 (建立訂單、扣庫存、付款等)
            // 範例：簡單清空購物車
            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();

            TempData["Message"] = "結帳完成，感謝您的購買！";
            return RedirectToAction("Index"); // 回到購物車頁面
        }
    }
}
