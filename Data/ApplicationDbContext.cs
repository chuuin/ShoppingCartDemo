using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using ShoppingCartDemo.Models;

namespace ShoppingCartDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        
    }
}
