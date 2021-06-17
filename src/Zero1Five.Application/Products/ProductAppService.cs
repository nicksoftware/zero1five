using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Zero1Five.Categories;
using Zero1Five.Common;
using Zero1Five.Gigs;
using Zero1Five.Permissions;

namespace Zero1Five.Products
{
    public class ProductAppService :
        CrudAppService<
            Product, ProductDto, Guid,
            PagedProductRequestDto,
            CreateUpdateProductDto,
            CreateUpdateProductDto>,
        IProductAppService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IGigRepository _gigRepository;
        private readonly IProductManager _productManager;
        private readonly IProductPictureManager _productPictureManager;

        public ProductAppService(
            IProductRepository repository,
            ICategoryRepository categoryRepository,
            IGigRepository gigRepository,
            IProductManager productManager, IProductPictureManager productPictureManager) : base(repository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _gigRepository = gigRepository;
            _productManager = productManager;
            _productPictureManager = productPictureManager;

            CreatePolicyName = Zero1FivePermissions.Products.Create;
            UpdatePolicyName = Zero1FivePermissions.Products.Edit;
            DeletePolicyName = Zero1FivePermissions.Products.Delete;
        }

        [Authorize(Zero1FivePermissions.Products.Create)]
        public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            var cover = input.Cover;

            var pictureStorageName = await _productPictureManager.SaveAsync(cover.FileName, cover.Content, true);

            var product = await _productManager
                .CreateAsync(
                    title: input.Title,
                    gigId: input.GigId,
                    categoryId: input.CategoryId,
                    cover: pictureStorageName,
                    description: input.Description);

            if (input.IsPublished)
            {
                await _productManager.PublishAsync(product);
            }

            return await MapToGetOutputDtoAsync(product);
        }
        public override async Task<ProductDto> GetAsync(Guid id)
        {
            await TryGetProductAsync(id);

            var queryable = await Repository.GetQueryableAsync();
            var query =
                from product in queryable
                join category in _categoryRepository on product.CategoryId equals category.Id
                join gig in _gigRepository on product.GigId equals gig.Id
                where product.Id == id
                select new {product, category, gig};

            var result = await AsyncExecuter.FirstOrDefaultAsync(query);

            if (result == null) throw new EntityNotFoundException(typeof(Product), id);

            var dto = await MapToGetOutputDtoAsync(result.product);

            dto.CategoryName = result.category.Name;
            dto.GigName = result.gig.Title;

            return dto;
        }
        public override async Task<PagedResultDto<ProductDto>> GetListAsync(PagedProductRequestDto input)
        {
            var filter = input.Filter;
            var canFilterByKeyword = !string.IsNullOrEmpty(filter);

            var queryable = await Repository.GetQueryableAsync();

            queryable = queryable.WhereIf(canFilterByKeyword,
                x => x.Title.Contains(filter) || x.Description.Contains(filter));

            var query =
                from product in queryable
                join category in _categoryRepository on product.CategoryId equals category.Id
                join gig in _gigRepository on product.GigId equals gig.Id
                select new {product, category, gig};

            //if input CategoryIdFilter is not null also Filter by Category  
            query = query.WhereIf(input.CategoryId != null, x => x.product.CategoryId == input.CategoryId);

            query = query
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var queryResult = await AsyncExecuter.ToListAsync(query);

            var productDtoList = queryResult.Select(x =>
            {
                var outputDto = MapToGetOutputDto(x.product);
                outputDto.CategoryName = x.category.Name;
                outputDto.GigName = x.gig.Title;
                return outputDto;
            }).ToList();

            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<ProductDto>()
            {
                TotalCount = totalCount,
                Items = productDtoList,
            };
        }

        public override async Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            var product =await TryGetProductAsync(id);
            if (input.Cover != null)
            {
                var imageFilename =await _productPictureManager.UpdateAsync(
                    product.CoverImage,
                    input.Cover.FileName,
                    input.Cover.Content);
               await _productManager.ChangeCoverImageAsync(product, imageFilename);
            }
            product.Title = input.Title;
            product.Description = input.Description;

          return  await MapToGetOutputDtoAsync( await Repository.UpdateAsync(product));
        }

        [Authorize(Zero1FivePermissions.Products.Edit)]
        public async Task<ProductDto> ChangeCoverASync(Guid productId, ChangeProductCoverDto input)
        {
            var product = await TryGetProductAsync(productId);
            var imageFileName = await _productPictureManager
                .UpdateAsync(
                    product.CoverImage,
                    input.CoverImage.FileName,
                    input.CoverImage.Content);

            var result = await _productManager.ChangeCoverImageAsync(product, imageFileName);

            return await MapToGetOutputDtoAsync(result);
        }

        public async Task<ListResultDto<GigLookUpDto>> GetGigLookUpAsync()
        {
            var gigs = _gigRepository.Where(x => x.CreatorId == CurrentUser.Id).ToList();
            var gigsDtoList = gigs.Select(g => new GigLookUpDto
            {
                Id = g.Id,
                Title = g.Title,
            }).ToList();

            await Task.Yield();

            return new ListResultDto<GigLookUpDto>
            {
                Items = gigsDtoList,
            };
        }
        public async Task<ListResultDto<CategoryDto>> GetLookUpCategoriesAsync()
        {
            var categories = await _categoryRepository.GetListAsync();

            var categoryDtos = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();

            return new ListResultDto<CategoryDto>
            {
                Items = categoryDtos
            };
        }

        [Authorize(Zero1FivePermissions.Products.Publish)]
        public async Task<Guid> PublishAsync(Guid id)
        {
            var product = await TryGetProductAsync(id);
            await _productManager.PublishAsync(product);
            return product.Id;
        }

        [Authorize(Zero1FivePermissions.Products.Publish)]
        public async Task<Guid> UnPublishAsync(Guid id)
        {
            var product = await TryGetProductAsync(id);

            await _productManager.UnPublishAsync(product);
            return product.Id;
        }

        private async Task<Product> TryGetProductAsync(Guid id)
        {
            var product = await Repository.FindAsync(id);
            if (product != null)
            {
                return product;
            }

            throw new EntityNotFoundException(typeof(Product), id);
        }

        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty()) return $"product.{nameof(Product.Title)}";

            if (sorting.Contains("categoryName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace("categoryName", "category.Name", StringComparison.OrdinalIgnoreCase);
            }

            return $"product.{sorting}";
        }
    }
}