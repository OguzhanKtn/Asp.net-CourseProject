using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Udemy.Web.Models.Repository.Entities;

namespace Udemy.Web.Models.Repository.Configurations
{
    public class BasketItemEntityConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CourseId).IsRequired();
            builder.Property(x => x.CourseTitle).HasMaxLength(100).IsRequired();
            builder.Property(x => x.CoursePrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.HasOne(x => x.Basket).WithMany(x => x.Items).HasForeignKey(x => x.BasketId);
        }
    }
}
