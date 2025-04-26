using Microsoft.EntityFrameworkCore;

namespace Tabeekh.Models
{
    public class TabeekhDBContext : DbContext
    {
        public TabeekhDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Chief> Chiefs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Delivery_Cust_Meal_Order> Delivery_Cust_Meal_Orders { get; set; }
        public DbSet<Cust_Chief_Review> Cust_Chief_Reviews { get; set; }
        public DbSet<Cust_Meal_Review> Cust_Meal_Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Meal_Category> Meals_Categories { get; set; }

        // Auth
        public DbSet<EndUser> EndUsers { get; set; }

    }
}
