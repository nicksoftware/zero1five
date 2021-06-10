using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Zero1Five.Categories;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Npgsql.Replication;
using Volo.Abp.Domain.Entities;
using Zero1Five.Common;
using Zero1Five.TestBase;

namespace Zero1Five.Products
{
    public class ProductAppServiceTests : Zero1FiveApplicationTestBase
    {
        private readonly IProductAppService _productAppService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductAppServiceTests()
        {
            _productAppService = GetRequiredService<IProductAppService>();
            _categoryRepository = GetRequiredService<ICategoryRepository>();
            _productRepository = GetRequiredService<IProductRepository>();
        }

        [Fact]
        public async Task GetListAsync_Should_returnProductsList()
        {
            var input = new PagedProductRequestDto();
            var result =await _productAppService.GetListAsync(input);
            
            result.Items.Count.ShouldBeGreaterThanOrEqualTo(0);
            result.Items.ShouldNotBeEmpty(nameof(ProductDto.CategoryName));
            result.Items.ShouldNotBeEmpty(nameof(ProductDto.GigName));
        }

        [Fact]
        public async Task GetListAsync_WhenFilteredByKeyword_ShouldReturnFilteredList()
        {
            var input = new PagedProductRequestDto
            {
                Filter = "1"
            };

            var result =await _productAppService.GetListAsync(input);
            
            result.Items.Count.ShouldBeGreaterThanOrEqualTo(1);
            result.Items.ShouldContain(x=>x.Title.Contains(input.Filter));
        }

        [Fact]
        public async Task GetListAsync_WhenFilteredByCategory_ShouldReturnFilteredList()
        {
            var input = new PagedProductRequestDto
            {
                CategoryId =Guid.Parse(  Zero1FiveTestData.CategoryId)
            };

            var result =await _productAppService.GetListAsync(input);
            
            result.Items.Count.ShouldBeGreaterThanOrEqualTo(1); 
            result.Items.ShouldContain(x=>x.CategoryId == input.CategoryId);
        }

        [Fact]
        public async Task GetAsync_ShouldGetProductWithDetail()
        {
            var productId =(await _productRepository.GetListAsync()).First().Id;
            var result =await _productAppService.GetAsync(productId);
            
            result.CategoryName.ShouldNotBeEmpty();
            result.CategoryId.ShouldNotBe(Guid.Empty);
            result.GigId.ShouldNotBe(Guid.Empty);
            result.GigName.ShouldNotBeEmpty();

        }
        [Fact]
        public async Task GetLookUpCategories_Should_GetCategories()
        {
            var result = await _productAppService.GetLookUpCategoriesAsync();
            result.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GetGigLookUpAsync_should_returnUserGigs()
        {
            var gigs = await _productAppService.GetGigLookUpAsync();
            gigs.Items.ShouldNotBeNull();
            gigs.Items.Count.ShouldBeGreaterThanOrEqualTo(0);
        }
        
        [Fact]
        public async Task CreateAsync_Should_UnPublishedProduct()
        {
            ProductDto result = null;
            //Given
            var input = new CreateUpdateProductDto
            {
                Title = "New Product",
                Description = "New Product Description",
                CoverImage = "demo.jpg",
                GigId = Guid.Parse(Zero1FiveTestData.GigId),
                IsPublished = false
            };
            await WithUnitOfWorkAsync(async () =>
                {
                    var categories = await _categoryRepository.GetListAsync();

                    input.CategoryId = categories.First().Id;
                    //When
                    result = await _productAppService.CreateAsync(input);
                });
            //Then
            result.Id.ShouldNotBe(Guid.Empty);
            result.CategoryId.ShouldNotBe(Guid.Empty);
            result.Title.ShouldBe(input.Title);
            result.Description.ShouldBe(input.Description);
            result.CoverImage.ShouldBe(input.CoverImage);
            result.IsPublished.ShouldBe(false);
        }

        [Fact]
        public async Task DeleteAsync_Should_DeleteProduct()
        {
            var productId = (await _productRepository.GetListAsync()).First().Id;

            await _productAppService.DeleteAsync(productId);

            ProductDto product = null;
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            {
               product =  await _productAppService.GetAsync(productId);
            });
            
            product.ShouldBeNull();
        }
        
        [Fact]
        public async Task UpdateAsync_Should_UpdateProduct()
        {
            var productId = (await _productRepository.GetListAsync()).First().Id;
            var product =await _productRepository.FindAsync(productId);
            var input = new CreateUpdateProductDto()
            {
                Id =  productId,
                CategoryId  = product.CategoryId,
                GigId =  product.GigId,
                Title =  "newTitle",
                CoverImage = "someImage.jph",
                Description = "new product Description ",
                IsPublished = true
            };
            var result = await _productAppService.UpdateAsync(productId,input);
            
            result.Id.ShouldBe(productId);
            result.Title.ShouldBe(input.Title);
            result.Description.ShouldBe(input.Description);
            result.CategoryId.ShouldBe(input.CategoryId);
            result.GigId.ShouldBe(input.GigId);
        }
        [Fact]
        public async Task Publish_ShouldPublish()
        {
            //Given
            Product uPublishedProduct = null;
            await WithUnitOfWorkAsync(async () =>
            {
                uPublishedProduct = await _productRepository.FirstOrDefaultAsync(x => !x.IsPublished);
            });

            //When
            var result = await _productAppService.PublishAsync(uPublishedProduct.Id);

            Product publishedProduct = null;
            await WithUnitOfWorkAsync(async () =>
            {
                publishedProduct = await _productRepository.FindAsync(uPublishedProduct.Id);
            });
            //Than
            result.ShouldNotBe(Guid.Empty);
            result.ShouldBe(uPublishedProduct.Id);
            publishedProduct.ShouldNotBeNull();
            publishedProduct.IsPublished.ShouldBe(true);
        }
        [Fact]
        public async Task UnPublish_ShouldUnPublish()
        {
            //Given
            Product uPublishedProduct = null;
            await WithUnitOfWorkAsync(async () =>
            {
                uPublishedProduct = await _productRepository.FirstOrDefaultAsync(x => x.IsPublished);
            });
            //When
            var result = await _productAppService.UnPublishAsync(uPublishedProduct.Id);

            Product publishedProduct = null;
            await WithUnitOfWorkAsync(async () =>
            {
                publishedProduct = await _productRepository.FindAsync(uPublishedProduct.Id);
            });
            //Than
            result.ShouldNotBe(Guid.Empty);
            result.ShouldBe(uPublishedProduct.Id);
            publishedProduct.ShouldNotBeNull();
            publishedProduct.IsPublished.ShouldBe(false);
        }
        [Fact]
        public async Task ChangeCoverASync()
        {
            var product = (await _productRepository.GetListAsync()).First();
            var input = new ChangeProductCoverDto
            {
                CoverImage = "changeProductImage.jpg"
            };

            var result =await _productAppService.ChangeCoverASync(product.Id, input);
            
            result.ShouldNotBeNull();
            
            result.CoverImage.ShouldBe(input.CoverImage);

        }
    }
}