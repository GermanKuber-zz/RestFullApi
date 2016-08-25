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
        public static object MapObject(Core.Community community, List<string> lstOfFields)
        {
            //TODO: Paso 10 - 1 - Seleccionar Campos individuales
            List<string> lstOfFieldsToWorkWith = new List<string>(lstOfFields);

            if (!lstOfFieldsToWorkWith.Any())
                return community;
            else
            {
                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFieldsToWorkWith)
                {
                    var fieldValue = community.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(community, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }


                return objectToReturn;
            }
        }
    }
}