using AutoMapper;
using RESTful.Web.Core.Domain;
using RESTful.Web.Core.Models.Blogs;

namespace RESTful.Web.Core.AutoMapper
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