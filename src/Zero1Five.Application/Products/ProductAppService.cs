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
using Zero1Five.Gigs;
using Zero1Five.Permissions;
namespace Zero1Five.Products
{
    public class ProductAppService :
        CrudAppService<
            Product, ProductDto, Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductDto,
            CreateUpdateProductDto>,
            IProductAppService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IGigRepository _gigRepository;
        private readonly IProductManager _productManager;

        public ProductAppService(
            IProductRepository repository,
            ICategoryRepository categoryRepository,
            IGigRepository gigRepository,
            IProductManager productManager) : base(repository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _gigRepository = gigRepository;
            _productManager = productManager;

            CreatePolicyName = Zero1FivePermissions.Products.Create;
            UpdatePolicyName = Zero1FivePermissions.Products.Edit;
            DeletePolicyName = Zero1FivePermissions.Products.Delete;
        }
        
        [Authorize(Zero1FivePermissions.Products.Create)]
        public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            var product = await _productManager
            .CreateAsync(
                input.Title,
                input.GigId,
                input.CategoryId,
                input.CoverImage,
                input.Description);

            if (input.IsPublished)
            {
                await _productManager.PublishAsync(product);
            }
            return MapToGetOutputDto(product);
        }
        public async Task<ListResultDto<GigLookUpDto>> GetGigLookUpAsync()
        {
            var gigs = _gigRepository.Where(x => x.CreatorId == CurrentUser.Id).ToList();
            var gigsDtoList = gigs.Select(g =>
                {
                    return new GigLookUpDto
                    {
                        Id = g.Id,
                        Title = g.Title,
                    };
                }).ToList();

            await Task.Yield();

            return new ListResultDto<GigLookUpDto>
            {
                Items = gigsDtoList,
            };
        }
        
        [Authorize(Zero1FivePermissions.Products.Edit)]
        public async Task<ProductDto> ChangeCoverASync(Guid productId, ChangeProductCoverDto input)
        {
            var product = await _repository.FindAsync(productId);
            
            if (product == null) throw new EntityNotFoundException(typeof(Product), productId);
            
            var result = await _productManager.ChangeCoverImageAsync(product, input.CoverImage);
          
            return await MapToGetOutputDtoAsync(result);
        }
        
        [Authorize(Zero1FivePermissions.Products.Edit)]
        public async Task<ListResultDto<CategoryDto>> GetLookUpCategoriesAsync()
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