// 引入系統中用來做驗證（Validation）的命名空間
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models // 定義命名空間，讓 User 類別歸類在 ShoppingCart 專案的 Models 資料夾底下
{
    // 定義一個 User 類別，對應資料庫的 Users 資料表
    public class User
    {
        // 主鍵 (Primary Key)，自動遞增的使用者 ID
        public int UserId { get; set; }

        // 帳號名稱，必填（Required），最大長度 50
        [Required]                 // 必填，不可為空
        [StringLength(50)]         // 限制最大字元數為 50
        public string Username { get; set; }

        // Email 欄位，必填，並且要符合 Email 格式
        [Required]                 // 必填
        [EmailAddress]             // 驗證是否為合法 Email 格式
        public string Email { get; set; }

        // 密碼的雜湊值（注意：不能存明碼！這裡存經過加密/雜湊的結果）
        [Required]                 // 必填
        public string PasswordHash { get; set; }

        // 使用者建立的時間，預設值為現在 (DateTime.Now)
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
