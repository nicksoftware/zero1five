using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Zero1Five.Categories;
using Zero1Five.Common;
using Zero1Five.Gigs;

namespace Zero1Five.Products
{
    public interface IProductAppService : ICrudAppService<
            ProductDto,
            Guid,
            PagedProductRequestDto,
            CreateUpdateProductDto,
            CreateUpdateProductDto>
    {
        Task<ListResultDto<CategoryDto>> GetLookUpCategoriesAsync();
        Task<ListResultDto<GigLookUpDto>> GetGigLookUpAsync();
        Task<ProductDto> ChangeCoverAsync(Guid productId, ChangeProductCoverDto input);
        Task<Guid> PublishAsync(Guid id);
        Task<Guid> UnPublishAsync(Guid id);
    }
}