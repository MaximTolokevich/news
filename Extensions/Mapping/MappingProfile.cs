using AutoMapper;

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

            CreateMap<Controllers.Models.GetOptionsListsViewcs, Services.Models.News>()
                .ReverseMap()
                .ForMember(x => x.SelectedAuthors, opt => opt.Ignore())
                .ForMember(x => x.CategoryList, opt => opt.Ignore())
                .ForMember(x=>x.NewsAuthors,opt=>opt.Ignore())
                .ForMember(x=>x.Category,opt=>opt.Ignore());
                
                
            CreateMap<Controllers.Models.Author, Services.Models.Author>()
                .ReverseMap();
            CreateMap<Controllers.Models.Category, Services.Models.Category>()
                .ReverseMap();
            
        }
    }
}
