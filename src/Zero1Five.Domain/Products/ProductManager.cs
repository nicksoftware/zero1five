using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;

namespace Zero1Five.Products
{
    public class ProductManager : DomainService, IProductManager
    {
        private readonly IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<Product> CreateAsync(string title, Guid categoryId, string cover, string description)
        {
            var product = Product.Create(GuidGenerator.Create(), title, categoryId, cover);
            product.Description = description;
            return _productRepository.InsertAsync(product, true);
        }
        public async Task<Guid> PublishAsync(Product product)
        {
            if (product.IsPublished)
                throw new ProductAlreadyPublishedException(product.Title);
            product.IsPublished = true;

            return (await _productRepository.UpdateAsync(product)).Id;
        }

        public async Task<Guid> UnPublishAsync(Product product)
        {
            if (!product.IsPublished)
                throw new ProductAlreadyUnpublishedException(product.Title);

            product.IsPublished = false;
            return (await _productRepository.UpdateAsync(product)).Id;

        }
    }
}