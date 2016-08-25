using Community.Core;
using Community.Core.Interfaces.Context;
using Community.Core.Interfaces.Repositorys;
using Community.Repository.Context;

namespace Community.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ICommunityContext context) : base(context)
        {
        }

 
    
    }
}