using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Zero1Five.Categories;

namespace Zero1Five.Products
{
    public interface IProductAppService : ICrudAppService<
            ProductDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateProductDto,
            UpdateProductDto>
    {
        Task<ListResultDto<CategoryDto>> GetLookUpCategories();
        Task<Guid> PublishAsync(Guid id);
        Task<Guid> UnPublishAsync(Guid id);
    }
}