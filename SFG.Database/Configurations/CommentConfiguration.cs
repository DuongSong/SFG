using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFG.Core.Entities.Posts;

namespace SFG.Database.Configurations
{
	public class CommentConfiguration : IEntityTypeConfiguration<Comment>
	{
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            //builder.HasMany<SubComment>()
            //    .WithOne()
            //    .HasForeignKey(x => x.CommentId)
            //    .IsRequired();
        }
    }
}

