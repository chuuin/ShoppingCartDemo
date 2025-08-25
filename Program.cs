using Microsoft.EntityFrameworkCore;
using ShoppingCartDemo.Data;

var builder = WebApplication.CreateBuilder(args);

// 加入資料庫
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 註冊 MVC
builder.Services.AddControllersWithViews();

// 註冊 IHttpContextAccessor (讓 CartController 可以注入)
builder.Services.AddHttpContextAccessor();

// 啟用 Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session 過期時間
    options.Cookie.HttpOnly = true;                 // Cookie 只能由 HTTP 讀取
    options.Cookie.IsEssential = true;              // 避免 GDPR 阻擋
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 啟用 Session
app.UseSession();

app.UseAuthorization();

// 預設路由
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
