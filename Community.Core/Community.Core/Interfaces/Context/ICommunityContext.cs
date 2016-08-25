using System.Data.Entity;

namespace Community.Core.Interfaces.Context
{
    public interface ICommunityContext
    {
        DbSet<Core.Community> Communitys { get; set; }
        DbSet<CommunityTag> CommunityTags { get; set; }
        DbSet<User> Users { get; set; }
    }
}