using BudgetTracking.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracking.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                // Gelir kategorileri
                new Category { Id = 1,  Name = "Maaş", IsIncome = true },
                new Category { Id = 2,  Name = "Prim", IsIncome = true },
                new Category { Id = 3,  Name = "Bonus", IsIncome = true },
                new Category { Id = 4,  Name = "Kira Geliri", IsIncome = true },
                new Category { Id = 5,  Name = "Faiz Geliri", IsIncome = true },
                new Category { Id = 6,  Name = "Hisse Senedi", IsIncome = true },
                new Category { Id = 7,  Name = "Kripto Para", IsIncome = true },
                new Category { Id = 8,  Name = "Ek İş", IsIncome = true },
                new Category { Id = 9,  Name = "Satış Geliri", IsIncome = true },
                new Category { Id = 10, Name = "Diğer Gelir", IsIncome = true },

                // Gider kategorileri
                new Category { Id = 11, Name = "Yemek", IsIncome = false },
                new Category { Id = 12, Name = "Ulaşım", IsIncome = false },
                new Category { Id = 13, Name = "Faturalar", IsIncome = false },
                new Category { Id = 14, Name = "Kira", IsIncome = false },
                new Category { Id = 15, Name = "Sağlık", IsIncome = false },
                new Category { Id = 16, Name = "Eğitim", IsIncome = false },
                new Category { Id = 17, Name = "Giyim", IsIncome = false },
                new Category { Id = 18, Name = "Eğlence", IsIncome = false },
                new Category { Id = 19, Name = "Seyahat", IsIncome = false },
                new Category { Id = 20, Name = "Diğer Gider", IsIncome = false }
            );
        }
    }
}
