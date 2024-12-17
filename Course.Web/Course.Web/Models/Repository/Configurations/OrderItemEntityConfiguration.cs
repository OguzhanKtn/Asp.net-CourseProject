using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Udemy.Web.Models.Repository.Entities;

namespace Udemy.Web.Models.Repository.Configurations
{
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CourseTitle).HasMaxLength(100).IsRequired();
            builder.Property(x => x.CoursePrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.CourseId).IsRequired();
            builder.Property(x => x.OrderId).IsRequired();
            builder.HasOne(x => x.Order).WithMany(x => x.Items).HasForeignKey(x => x.OrderId);
        }
    }
}
