using AutoMapper;
using RESTful.Web.Core.Domain;
using RESTful.Web.Core.Models.Posts;

namespace RESTful.Web.Core.AutoMapper
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostDto,Post>();
        }
    }
}