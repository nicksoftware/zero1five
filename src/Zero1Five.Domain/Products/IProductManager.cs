using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Zero1Five.Products
{
    public interface IProductManager : IDomainService
    {
        Task<Product> CreateAsync(string title, Guid categoryId, string cover, string description);
        Task<Guid> PublishAsync(Product product);
        Task<Guid> UnPublishAsync(Product product);
    }
}