using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Udemy.Web.Models.Repository.Entities;

namespace Udemy.Web.Models.Repository.Configurations
{
    public class BasketEntityConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.UserId).IsRequired();
        }
    }
}
