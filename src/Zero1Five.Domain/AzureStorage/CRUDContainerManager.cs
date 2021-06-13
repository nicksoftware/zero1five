using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Services;
using Zero1Five.AzureStorage.Gig;

namespace Zero1Five.AzureStorage
{
    public class CRUDContainerManager<T> where T: class  
    {
        private readonly IBlobContainer _gigPictureContainer;
        private readonly IOptions<AzureStorageAccountOptions> _azureStorageAccountOptions;

        public CRUDContainerManager(
            IBlobContainer<T> gigPictureContainer,
            IOptions<AzureStorageAccountOptions> azureStorageAccountOptions)
        {
            _azureStorageAccountOptions = azureStorageAccountOptions;
            _gigPictureContainer = gigPictureContainer;
        }
        public virtual async Task<string> SaveAsync(string fileName, byte[] byteArray, bool overrideExisting = false)
        {
            var extension = Path.GetExtension(fileName);
            string storageFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{Guid.NewGuid()}{extension}";
            await _gigPictureContainer.SaveAsync(storageFileName, byteArray, overrideExisting);
            return storageFileName;
        }

        public virtual  async Task<string> UpdateAsync(string oldFilename,string fileName,byte[] byteArray, bool overrideExisting = false)
        {
            await DeleteAsync(oldFilename);
            return await SaveAsync(fileName, byteArray, overrideExisting);
        }

        public virtual async Task<bool> DeleteAsync(string gigCoverImage)
        {
            return  await _gigPictureContainer.DeleteAsync(gigCoverImage);
        }
    }
}