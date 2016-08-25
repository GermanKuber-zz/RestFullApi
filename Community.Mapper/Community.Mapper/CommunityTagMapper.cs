using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Community.Core;
using Community.ViewModel.Request;

namespace Community.Mapper
{
    public static class CommunityTagMapper
    {
        public static CommunityTagViewModel Map(CommunityTag data)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CommunityTag, CommunityTagViewModel>();
            });
            var mapper = new AutoMapper.Mapper(config);
            CommunityTagViewModel map = mapper.DefaultContext.Mapper.Map<CommunityTagViewModel>(data);
            return map;
        }
        public static CommunityTag Map(CommunityTagViewModel data)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CommunityTagViewModel, CommunityTag>();
            });
            var mapper = new AutoMapper.Mapper(config);
            CommunityTag map = mapper.DefaultContext.Mapper.Map<CommunityTag>(data);
            return map;
        }
        public static CommunityTag Map(CommunityTagViewModel from, CommunityTag to)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CommunityTagViewModel, CommunityTag>();
                cfg.CreateMap<CommunityTagViewModel, CommunityTag>();
            });
            var mapper = new AutoMapper.Mapper(config);
            CommunityTag user = mapper.DefaultContext.Mapper.Map<CommunityTagViewModel, CommunityTag>(from, to);
            return to;
        }



    }
}