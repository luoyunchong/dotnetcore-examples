using AutoMapper;
using Webapi.FreeSql.Domain;
using Webapi.FreeSql.Models.Blogs;

namespace Webapi.FreeSql.AutoMapper
{
    public class BlogProfile : Profile
    {
        public BlogProfile() 
        {
            CreateMap<CreateBlogDto, Blog>();
            CreateMap<UpdateBlogDto, Blog>();
        }
    }
}