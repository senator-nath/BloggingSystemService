using BloggingSystemService.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Persistence.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {

            builder.HasKey(p => p.Id);


            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(p => p.DatePublished)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");


            builder.HasOne(p => p.Blog)
                .WithMany(b => b.Posts)
                .HasForeignKey(p => p.BlogId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
