using Community.Core;
using Community.Core.Interfaces.Repositorys;
using Community.Core.Interfaces.Services;

namespace Community.Service
{
    public class CommunityTagService : GenericService<CommunityTag>, ICommunityTagService
    {
        public CommunityTagService(ICommunityTagRepository communityTagRepository) : base(communityTagRepository)
        {
        }
    }
}