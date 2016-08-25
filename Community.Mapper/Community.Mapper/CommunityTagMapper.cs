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

        public static object MapObject(CommunityTagViewModel tag, List<string> lstOfFields)
        {
            if (!lstOfFields.Any())
                return tag;
            else
            {
                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {

                    var fieldValue = tag.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(tag, null);

                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }
    }
}