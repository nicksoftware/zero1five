using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Services;
using Zero1Five.AzureStorage.Gig;

namespace Zero1Five.AzureStorage
{
    public interface ICrudContainerManager
    {
        Task<string> SaveAsync(string fileName, byte[] byteArray, bool overrideExisting = false);
        Task<string> UpdateAsync(string oldFilename, string fileName, byte[] byteArray, bool overrideExisting = false);
        Task<bool> DeleteAsync(string gigCoverImage);
    }
    public class CrudContainerManager<T> :ICrudContainerManager where T : class
    {
        private readonly IBlobContainer _productPictureContainer;
        private readonly IOptions<AzureStorageAccountOptions> _azureStorageAccountOptions;

        public CrudContainerManager(
            IBlobContainer<T> productPictureContainer,
            IOptions<AzureStorageAccountOptions> azureStorageAccountOptions)
        {
            _azureStorageAccountOptions = azureStorageAccountOptions;
            _productPictureContainer = productPictureContainer;
        }
        public virtual async Task<string> SaveAsync(string fileName, byte[] byteArray, bool overrideExisting = false)
        {
            var extension = Path.GetExtension(fileName);
            string storageFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{Guid.NewGuid()}{extension}";
            await _productPictureContainer.SaveAsync(storageFileName, byteArray, overrideExisting);
            return storageFileName;
        }

        public virtual  async Task<string> UpdateAsync(string oldFilename,string fileName,byte[] byteArray, bool overrideExisting = false)
        {
            await DeleteAsync(oldFilename);
            return await SaveAsync(fileName, byteArray, overrideExisting);
        }

        public virtual async Task<bool> DeleteAsync(string gigCoverImage)
        {
            return  await _productPictureContainer.DeleteAsync(gigCoverImage);
        }
    }
}