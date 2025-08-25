using Microsoft.EntityFrameworkCore;
using ShoppingCartDemo.Data;

var builder = WebApplication.CreateBuilder(args);

// �[�J��Ʈw
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ���U MVC
builder.Services.AddControllersWithViews();

// ���U IHttpContextAccessor (�� CartController �i�H�`�J)
builder.Services.AddHttpContextAccessor();

// �ҥ� Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session �L���ɶ�
    options.Cookie.HttpOnly = true;                 // Cookie �u��� HTTP Ū��
    options.Cookie.IsEssential = true;              // �קK GDPR ����
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

// �ҥ� Session
app.UseSession();

app.UseAuthorization();

// �w�]����
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
