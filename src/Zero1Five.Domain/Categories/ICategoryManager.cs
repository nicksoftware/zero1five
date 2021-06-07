using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Zero1Five.Categories
{
    public interface ICategoryManager : IDomainService
    {
        Task<Category> CreateCategoryAsync(string name, string descrption);

    }
}