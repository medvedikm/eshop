using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItems>
    {
            public void Configure(EntityTypeBuilder<OrderItems> builder)
            {
                builder.Property(nameof(OrderItems.DateTimeCreated))
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("GETDATE()");
            }
    }
}
