# 購物車網站 (ShoppingCartDemo)

一個使用 **ASP.NET Core MVC** 與 **Entity Framework Core** 建立的購物車網站專案，示範如何進行 CRUD、購物車管理與資料庫連線。  
此專案主要功能為 **商品瀏覽與購物車管理**，提供商品的新增、編輯、刪除與購物車操作。

---

## 專案功能
- 商品列表顯示 (Index)  
- 商品詳細資訊 (Details)  
- 新增商品 (Create)  
- 編輯商品 (Edit)  
- 刪除商品 (Delete)  
- 加入購物車 / 移除購物車  
- 購物車總價計算  

---

## 資料庫設計 (MSSQL)
資料表名稱：`Products`、`Carts`  

**Products**

| 欄位       | 型別           | 描述          |
|------------|----------------|---------------|
| Id         | int (PK)       | 商品編號      |
| Name       | nvarchar(100)  | 商品名稱      |
| Price      | decimal(18,2)  | 商品價格      |
| Description| nvarchar(500)  | 商品描述      |
| Stock      | int             | 庫存數量     |

**Carts**

| 欄位      | 型別           | 描述            |
|-----------|----------------|----------------|
| Id        | int (PK)       | 購物車項目編號 |
| ProductId | int (FK)       | 商品編號       |
| Quantity  | int             | 數量           |

---

## 技術
- ASP.NET Core MVC  
- Razor Pages  
- Entity Framework Core  
- MSSQL (SQL Server)  
- Bootstrap (前端樣式)  

---

## 開始使用
1. **建立資料庫**

```sql
CREATE DATABASE ShoppingCartDB;
