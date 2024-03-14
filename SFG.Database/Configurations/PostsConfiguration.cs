using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFG.Core.Entities.Posts;

namespace SFG.Database.Configurations
{
    public class PostsConfiguration : IEntityTypeConfiguration<Posts>
    {
        public void Configure(EntityTypeBuilder<Posts> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            //builder.HasMany(x => x.Comments)
            //    .WithOne()
            //    .HasForeignKey(x => x.PostId)
            //    .IsRequired();
        }
    }
}

