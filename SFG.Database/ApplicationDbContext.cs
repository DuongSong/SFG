
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SFG.Core.Entities.Posts;
using SFG.Core.Entities.Accounts;
using Microsoft.EntityFrameworkCore.Proxies;

namespace SFG.Database
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
	{
		public ApplicationDbContext() { }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.Development.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("defaultConnection");
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseSqlServer(connectionString);
                
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<SubComment> SubComment { get; set; }
        public virtual DbSet<UserLikePosts> UserLikePosts { get; set; }
    }
}

