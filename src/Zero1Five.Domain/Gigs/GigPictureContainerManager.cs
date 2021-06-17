using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Services;
using Zero1Five.AzureStorage;
using Zero1Five.AzureStorage.Gig;

namespace Zero1Five.Gigs
{
    public interface IGigPictureContainerManager:ICrudContainerManager, IDomainService
    {
    }
    public class GigPictureContainerManager : CrudContainerManager<GigPictureContainer>,IGigPictureContainerManager
    {
        public GigPictureContainerManager(IBlobContainer<GigPictureContainer> productPictureContainer, IOptions<AzureStorageAccountOptions> azureStorageAccountOptions) : base(productPictureContainer, azureStorageAccountOptions)
        {
        }
    }
}