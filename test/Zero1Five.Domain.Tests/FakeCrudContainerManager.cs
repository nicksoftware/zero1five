using System;
using System.Threading.Tasks;
using Zero1Five.AzureStorage;

namespace Zero1Five
{
    public class FakeCrudContainerManager:ICrudContainerManager
    {
        public Task<string> SaveAsync(string fileName, byte[] byteArray, bool overrideExisting = false)
        {
            return Task.FromResult(fileName + Guid.NewGuid() + ".jpg");
        }

        public Task<string> UpdateAsync(string oldFilename, string fileName, byte[] byteArray,
            bool overrideExisting = false)
        {
            return Task.FromResult(fileName + Guid.NewGuid() + ".jpg");
        }

        public Task<bool> DeleteAsync(string gigCoverImage)
        {
            return Task.FromResult(true);
        }
    }
}