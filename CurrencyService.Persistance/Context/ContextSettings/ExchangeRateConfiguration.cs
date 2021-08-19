using CurrencyService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyService.Persistance.Context.ContextSettings
{
    public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRateModel>
    {
        public void Configure(EntityTypeBuilder<ExchangeRateModel> modelBuilder)
        {
            modelBuilder.HasKey(e => new { e.FromDate, e.Currency, e.SourceCurrency });
        }
    }
}
