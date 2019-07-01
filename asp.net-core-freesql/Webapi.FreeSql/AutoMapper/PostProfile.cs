using AutoMapper;
using Webapi.FreeSql.Domain;
using Webapi.FreeSql.Models.Posts;

namespace Webapi.FreeSql.AutoMapper
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostDto,Post>();
        }
    }
}