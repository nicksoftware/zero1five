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
    public class GigPictureContainerManager : CRUDContainerManager<GigPictureContainer>,IDomainService
    {

        // public async Task<string> SaveAsync(string fileName, byte[] byteArray, bool overrideExisting = false)
        // {
        //     var extension = Path.GetExtension(fileName);
        //     string storageFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{Guid.NewGuid()}{extension}";
        //     await _gigPictureContainer.SaveAsync(storageFileName, byteArray, overrideExisting);
        //     return storageFileName;
        // }
        //
        // public async Task<string> UpdateAsync(string oldFilename,string fileName,byte[] byteArray, bool overrideExisting = false)
        // {
        //     await DeleteAsync(oldFilename);
        //    return await SaveAsync(fileName, byteArray, overrideExisting);
        // }
        //
        // public async Task<bool> DeleteAsync(string gigCoverImage)
        // {
        //    return  await _gigPictureContainer.DeleteAsync(gigCoverImage);
        // }
        public GigPictureContainerManager(IBlobContainer<GigPictureContainer> gigPictureContainer, IOptions<AzureStorageAccountOptions> azureStorageAccountOptions) : base(gigPictureContainer, azureStorageAccountOptions)
        {
        }
    }
}