using AutoMapper;
using Community.Core;
using Community.ViewModel.Request;

namespace Community.Mapper
{
    public static class CommunityMapper
    {
        public static CommunityViewModel Map(Core.Community data)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Core.CommunityTag, CommunityTagViewModel>();
                cfg.CreateMap<Core.Community, CommunityViewModel>();
            });
            var mapper = new AutoMapper.Mapper(config);
            CommunityViewModel map = mapper.DefaultContext.Mapper.Map<CommunityViewModel>(data);
            return map;
        }
        public static Core.Community Map(CommunityViewModel data)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CommunityViewModel, Core.Community>();
            });
            var mapper = new AutoMapper.Mapper(config);
            Core.Community map = mapper.DefaultContext.Mapper.Map<Core.Community>(data);
            return map;
        }
        public static Core.Community Map(CommunityViewModel from, Core.Community to)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CommunityViewModel, Core.Community>();
                cfg.CreateMap<CommunityTagViewModel, CommunityTag>();
            });
            var mapper = new AutoMapper.Mapper(config);
            Core.Community user = mapper.DefaultContext.Mapper.Map<CommunityViewModel, Core.Community>(from, to);
            return to;
        }

    }
}