using Microsoft.Extensions.Options;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Services;
using Zero1Five.AzureStorage;
using Zero1Five.AzureStorage.Gig;

namespace Zero1Five.Products
{
    public class ProductPictureManager : CRUDContainerManager<GigPictureContainer>,IDomainService
    {
        public ProductPictureManager(IBlobContainer<GigPictureContainer> gigPictureContainer, IOptions<AzureStorageAccountOptions> azureStorageAccountOptions) : base(gigPictureContainer, azureStorageAccountOptions)
        {
        }
    }
}