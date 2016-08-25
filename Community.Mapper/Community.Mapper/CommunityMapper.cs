using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Community.Core;
using Community.ViewModel.Request;
using Community.Helper;

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
        public static object MapObject(CommunityViewModel community, List<string> lstOfFields)
        {
            List<string> lstOfFieldsToWorkWith = new List<string>(lstOfFields);

            if (!lstOfFieldsToWorkWith.Any())
                return community;
            else

            {
                var lstOfTagsFields = lstOfFieldsToWorkWith.Where(f => f.Contains("tags")).ToList();

        
                bool partialTags = lstOfTagsFields.Any() && !lstOfTagsFields.Contains("tags");

  
                if (partialTags)
                    lstOfTagsFields = lstOfTagsFields.Select(f => f.Substring(f.IndexOf(".", StringComparison.Ordinal) + 1)).ToList();
                else
                {
                    lstOfTagsFields.Remove("tags");
                    lstOfTagsFields.RemoveRange(lstOfFieldsToWorkWith);
                }
                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFieldsToWorkWith)
                {
                    var fieldValue = community.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(community, null);

                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }
                if (partialTags)
                {
                    List<object> tags = new List<object>();
                    foreach (var tag in community.Tags)
                        tags.Add(CommunityTagMapper.MapObject(tag, lstOfTagsFields));

                    ((IDictionary<String, Object>)objectToReturn).Add("tags", tags);
                }

                return objectToReturn;
            }
        }
    }
}