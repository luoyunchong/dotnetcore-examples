using AutoMapper;
using OvOv.Core.Domain;
using OvOv.Core.Models.Blogs;

namespace OvOv.Core.AutoMapper
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