using Community.Core.Interfaces.Repositorys;
using Community.Repository.Context;

namespace Community.Repository
{
    public class CommunityRepository : GenericRepository<Core.Community>, ICommunityRepository
    {
        public CommunityRepository(CommunityContext context) : base(context)
        {
        }
    }

}