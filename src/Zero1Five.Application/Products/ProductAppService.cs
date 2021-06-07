using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
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

        public ProductAppService(
            IProductRepository repository,
            ICategoryRepository categoryRepository) : base(repository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;

            CreatePolicyName = Zero1FivePermissions.Products.Create;
            UpdatePolicyName = Zero1FivePermissions.Products.Edit;
            DeletePolicyName = Zero1FivePermissions.Products.Delete;
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
    }
}