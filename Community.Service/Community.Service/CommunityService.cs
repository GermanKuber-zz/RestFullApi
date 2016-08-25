using Community.Core.Interfaces.Repositorys;
using Community.Core.Interfaces.Services;

namespace Community.Service
{
    public class CommunityService : GenericService<Core.Community>, ICommunityService
    {
        public CommunityService(ICommunityRepository communityRepository) : base(communityRepository)
        {
        }
    }
}