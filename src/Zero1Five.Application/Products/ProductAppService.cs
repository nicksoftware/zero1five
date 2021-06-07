using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Zero1Five.Categories;
using Zero1Five.Permissions;
namespace Zero1Five.Products
{
    public class ProductAppService :
        CrudAppService<
            Product, ProductDto, Guid,
            PagedAndSortedResultRequestDto,
            CreateProductDto,
            UpdateProductDto>,
            IProductAppService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductManager _productManager;

        public ProductAppService(
            IProductRepository repository,
            ICategoryRepository categoryRepository,
            IProductManager productManager) : base(repository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _productManager = productManager;

            CreatePolicyName = Zero1FivePermissions.Products.Create;
            UpdatePolicyName = Zero1FivePermissions.Products.Edit;
            DeletePolicyName = Zero1FivePermissions.Products.Delete;
        }

        public override async Task<ProductDto> CreateAsync(CreateProductDto input)
        {

            var product = await _productManager
            .CreateAsync(
                input.Title,
                input.CategoryId,
                input.CoverImage,
                input.Description);

            if (input.IsPublished)
            {
                await _productManager.PublishAsync(product);
            }
            return MapToGetOutputDto(product);
        }


        public async Task<ListResultDto<CategoryDto>> GetLookUpCategories()
        {
            var categories = await _categoryRepository.GetListAsync();

            var categoryDtos = categories.Select(c =>
            {
                return new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                };
            }).ToList();

            return new ListResultDto<CategoryDto>
            {
                Items = categoryDtos
            };
        }

        [Authorize(Zero1FivePermissions.Products.Publish)]
        public async Task<Guid> PublishAsync(Guid id)
        {
            var product = await Repository.FindAsync(id);
            if (product is null)
                throw new EntityNotFoundException(typeof(Product), id);
            await _productManager.PublishAsync(product);

            return product.Id;
        }
        [Authorize(Zero1FivePermissions.Products.Publish)]
        public async Task<Guid> UnPublishAsync(Guid id)
        {
            var product = await Repository.FindAsync(id);
            if (product is null)
                throw new EntityNotFoundException(typeof(Product), id);

            await _productManager.UnPublishAsync(product);

            return product.Id;
        }

    }
}