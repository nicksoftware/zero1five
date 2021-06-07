using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Zero1Five.Products
{
    public class ProductManager : DomainService, IProductManager
    {
        public Task<Product> CreateAsync(string title, Guid categoryId, string cover)
        {
            return Task.FromResult<Product>(Product.Create(GuidGenerator.Create(), title, categoryId, cover));
        }
    }
}