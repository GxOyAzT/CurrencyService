using CurrencyService.Domain.Models;
using CurrencyService.Persistance.Context.ContextSettings;
using Microsoft.EntityFrameworkCore;

namespace CurrencyService.Persistance.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
        }

        public DbSet<ExchangeRateModel> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ExchangeRateConfiguration().Configure(modelBuilder.Entity<ExchangeRateModel>());
        }
    }
}
