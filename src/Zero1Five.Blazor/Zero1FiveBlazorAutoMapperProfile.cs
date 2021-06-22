using AutoMapper;
using Zero1Five.Categories;
using Zero1Five.Gigs;
using Zero1Five.Products;

namespace Zero1Five.Blazor
{
    public class Zero1FiveBlazorAutoMapperProfile : Profile
    {
        public Zero1FiveBlazorAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Blazor project.
            CreateMap<CategoryDto, CreateUpdateCategoryDto>().ReverseMap();
            CreateMap<ProductDto, CreateUpdateProductDto>();
            CreateMap<GigDto, CreateUpdateGigDto>();
        }
    }
}
