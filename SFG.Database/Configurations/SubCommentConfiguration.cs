using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFG.Core.Entities.Posts;

namespace SFG.Database.Configurations
{
    public class SubCommentConfiguration : IEntityTypeConfiguration<SubComment>
    {
        public void Configure(EntityTypeBuilder<SubComment> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);
        }
    }
}

