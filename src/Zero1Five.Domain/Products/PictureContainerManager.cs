using Microsoft.Extensions.Options;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Services;
using Zero1Five.AzureStorage;
using Zero1Five.AzureStorage.Gig;
using Zero1Five.AzureStorage.Products;

namespace Zero1Five.Products
{
    public interface IProductPictureManager:ICrudContainerManager,IDomainService
    {
        
    }
    public class ProductPictureManager : CrudContainerManager<ProductPictureContainer>,IProductPictureManager
    {
        public ProductPictureManager(
            IBlobContainer<ProductPictureContainer> productPictureContainer,
            IOptions<AzureStorageAccountOptions> azureStorageAccountOptions) :
            base(productPictureContainer, azureStorageAccountOptions)
        {
        }
    }
}