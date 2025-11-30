using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class FinalLabContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        // Default constructor for design-time services
        public FinalLabContext() { }

        public FinalLabContext(DbContextOptions<FinalLabContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Server=localhost;Database=Database;User=root;Password=12345678;";
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
    }
}

