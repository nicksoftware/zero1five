using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Zero1Five.Permissions;

namespace Zero1Five.Categories
{
    public  class CategoryAppService :
        CrudAppService<Category, CategoryDto, Guid, PagedAndSortedResultRequestDto,
        CreateUpdateCategoryDto, CreateUpdateCategoryDto>, ICategoryAppService
    {
        private readonly ICategoryManager _categoryManager;
        public CategoryAppService(
            ICategoryRepository repository,
            ICategoryManager categoryManager) :
            base(repository)
        {
            SetPermissions();
            this._categoryManager = categoryManager;
        }

        private void SetPermissions()
        {
            CreatePolicyName = Zero1FivePermissions.Categories.Create;
            UpdatePolicyName = Zero1FivePermissions.Categories.Edit;
            DeletePolicyName = Zero1FivePermissions.Categories.Delete;
        }

        [Authorize(Zero1FivePermissions.Categories.Default)]
        public override async Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
        {
            return await MapToGetOutputDtoAsync(await _categoryManager.CreateAsync(input.Name, input.Description));
        }
    }
}