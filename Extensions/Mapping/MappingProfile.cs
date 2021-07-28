using AutoMapper;
using System.Linq;

namespace news.Extensions.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap <Repositories.Models.News, Services.Models.News>()
                .ReverseMap();
            CreateMap<Repositories.Models.Author, Services.Models.Author>()
                .ReverseMap();
            CreateMap<Repositories.Models.Category, Services.Models.Category>()
                .ReverseMap();

            CreateMap<Controllers.Models.NewsViewModel, Services.Models.News>()
                .ReverseMap()
                .ForMember(vm=>vm.CategoryList,o=>o.MapFrom(src=>src.Category.Id))
                .ForMember(vm=>vm.NewsAuthors,o=>o.MapFrom(src=>src.NewsAuthors.Select(x=>x.Id)));       
            CreateMap<Controllers.Models.Author, Services.Models.Author>()
                .ReverseMap();
            CreateMap<Controllers.Models.Category, Services.Models.Category>()
                .ReverseMap();

        }
    }
}
