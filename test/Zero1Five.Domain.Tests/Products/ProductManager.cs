using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Zero1Five.Categories;

namespace Zero1Five.Products
{
    public class ProductManagerTests : Zero1FiveDomainTestBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductManager _productManager;
        private readonly ICategoryRepository _categoryRepository;
        public ProductManagerTests()
        {
            _productRepository = GetRequiredService<IProductRepository>();
            _productManager = GetRequiredService<IProductManager>();
            _categoryRepository = GetRequiredService<ICategoryRepository>();

        }

        [Fact]
        public async Task Create_Should_Create_And_Return_NewProduct()
        {
            //Given
            Guid id = Guid.NewGuid();

            var category = (await _categoryRepository.GetListAsync()).First();
            string title = "New Product";
            string description = "New Product";
            string cover = "coverImage.jpg";
            var productManger = new ProductManager();
            Product result = null;

            await WithUnitOfWorkAsync(async () =>
            {
                var newProduct = await _productManager.CreateAsync(title, category.Id, cover);
                newProduct.Description = description;
                result = await _productRepository.InsertAsync(newProduct, true);
            });

            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(Guid.Empty);
            result.CategoryId.ShouldBe(category.Id);
            result.Title.ShouldBe(title);
            result.CoverImage.ShouldBe(cover);
            result.Description.ShouldBe(description);

        }
    }
}