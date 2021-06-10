using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Zero1Five.Categories
{
    public interface ICategoryManager : IDomainService
    {
        Task<Category> CreateAsync(string name, string description);

    }
}