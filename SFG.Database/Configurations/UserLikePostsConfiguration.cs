using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFG.Core.Entities.Posts;

namespace SFG.Database.Configurations
{
    public class UserLikePostsConfiguration : IEntityTypeConfiguration<UserLikePosts>
    {
        public void Configure(EntityTypeBuilder<UserLikePosts> builder)
        {
            builder.HasKey(x => new { x.UserId, x.PostsId});

            builder.HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Posts>()
                .WithMany(x => x.UserLikePosts)
                .HasForeignKey(x => x.PostsId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

