using AutoMapper;
using Volo.Abp.Application.Dtos;
using Zero1Five.Categories;
using Zero1Five.Gigs;
using Zero1Five.Products;

namespace Zero1Five
{
    public class Zero1FiveApplicationAutoMapperProfile : Profile
    {
        public Zero1FiveApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateUpdateCategoryDto, Category>();

            CreateMap<Product, ProductDto>();
            CreateMap<CreateUpdateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Gig, GigDto>();
            CreateMap<CreateUpdateGigDto, Gig>();
            CreateMap<UpdateGigDto, Gig>();
        }
    }
}
