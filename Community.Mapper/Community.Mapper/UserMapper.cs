using AutoMapper;
using Community.Core;
using Community.ViewModel.Request;

namespace Community.Mapper
{
    public static class UserMapper
    {
        public static UserViewModel Map(User data)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<Core.Community, CommunityViewModel>();
            });
            var mapper = new AutoMapper.Mapper(config);
            UserViewModel user = mapper.DefaultContext.Mapper.Map<UserViewModel>(data);
            return user;
        }
        public static User Map(UserViewModel data)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserViewModel, User>();
                cfg.CreateMap<CommunityViewModel, Core.Community>();
            });
            var mapper = new AutoMapper.Mapper(config);
            User user = mapper.DefaultContext.Mapper.Map<User>(data);
            return user;
        }
        public static User Map(UserViewModel from, User to)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserViewModel, User>();
                cfg.CreateMap<CommunityViewModel, Core.Community>();
            });
            var mapper = new AutoMapper.Mapper(config);
            User user = mapper.DefaultContext.Mapper.Map<UserViewModel,User>(from,to);
            return to;
        }
    }
}
