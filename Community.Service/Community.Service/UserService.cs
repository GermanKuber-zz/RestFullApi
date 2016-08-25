using Community.Core;
using Community.Core.Interfaces.Repositorys;
using Community.Core.Interfaces.Services;

namespace Community.Service
{
    public class UserService : GenericService<User>, IUserService
    {
        public UserService(IUserRepository userRepository) : base(userRepository)
        {
        }
    }
}
