# ShoppingCartDemo

一個使用 **ASP.NET Core MVC** 與 **Entity Framework Core** 建立的線上購物車專案，示範如何管理商品、購物車及結帳流程。  
此專案主要功能為 **商品瀏覽與購物車管理**，提供商品列表、搜尋、加入購物車、修改數量、刪除商品與結帳。

---

## 專案功能

- 商品列表顯示 (Products/Index)
- 商品搜尋功能
- 加入商品到購物車 (Ajax 支援)
- 修改購物車商品數量
- 刪除購物車商品
- 結帳功能，結帳後自動清空購物車

---

## 資料庫設計 (MSSQL)

資料表名稱：`Products`、`CartItems`

### Products

| 欄位      | 型別          | 描述       |
|-----------|---------------|------------|
| Id        | int (PK)      | 商品編號   |
| Name      | nvarchar(50)  | 商品名稱   |
| Price     | decimal       | 價格       |
| lmageUrl  | nvarchar(255) | 商品圖片URL (可選) |

### CartItems

| 欄位      | 型別          | 描述                  |
|-----------|---------------|-----------------------|
| Id        | int (PK)      | 購物車項目編號       |
| ProductId | int (FK)      | 對應商品編號         |
| Quantity  | int           | 購買數量，預設 1     |
| SessionId | nvarchar(50)  | 使用者 Session ID     |

---

## 技術

- ASP.NET Core MVC
- Razor View
- Entity Framework Core
- MSSQL (SQL Server)
- jQuery + Ajax
- Bootstrap 5
- Session 管理購物車

---

## 開始使用

1. **建立資料庫**
   ```sql
   CREATE DATABASE ShoppingCartDemoDB;
