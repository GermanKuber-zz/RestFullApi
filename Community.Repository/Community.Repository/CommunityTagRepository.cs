using Community.Core;
using Community.Core.Interfaces.Repositorys;
using Community.Repository.Context;

namespace Community.Repository
{
    public class CommunityTagRepository : GenericRepository<CommunityTag>, ICommunityTagRepository
    {
        public CommunityTagRepository(CommunityContext context) : base(context)
        {
        }
    }

}