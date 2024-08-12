using BloggingSystemService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSystemService.Persistence.Configuration
{
    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {

        public void Configure(EntityTypeBuilder<Author> builder)
        {
            // Configure the primary key
            builder.HasKey(e => e.Id);

            // Configure properties
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            // Configure the one-to-many relationship with Blogs
            builder.HasMany(e => e.Blogs)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);


             


        }
    }
}
