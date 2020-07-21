using AutoMapper;
using OvOv.Core.Domain;
using OvOv.Core.Models.Posts;

namespace OvOv.Core.AutoMapper
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostDto,Post>();
        }
    }
}