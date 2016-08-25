using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Community.Core;
using Community.Core.Interfaces.Context;
using Community = Community.Core.Community;

namespace Community.Repository.Context
{
 public   class CommunityContext : DbContext, ICommunityContext
    {
        public CommunityContext()
            : base("name=CommunityContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<CommunityTag> CommunityTags { get; set; }
        public virtual DbSet<Core.Community> Communitys { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<User>()
                .HasMany(s => s.Communitys)
                .WithMany(c => c.Users);
        }

    }
}
