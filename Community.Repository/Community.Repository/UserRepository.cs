using Community.Core;
using Community.Core.Interfaces.Context;
using Community.Core.Interfaces.Repositorys;

namespace Community.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ICommunityContext context) : base(context)
        {
        }

 
    
    }
}