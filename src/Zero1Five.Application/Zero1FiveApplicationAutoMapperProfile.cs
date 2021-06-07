using AutoMapper;
using Zero1Five.Categories;

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
        }
    }
}
