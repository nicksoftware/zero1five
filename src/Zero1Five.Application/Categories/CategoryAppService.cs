using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Zero1Five.Permissions;

namespace Zero1Five.Categories
{
    public class CategoryAppService :
        CrudAppService<Category, CategoryDto, Guid, PagedAndSortedResultRequestDto,
        CreateUpdateCategoryDto, CreateUpdateCategoryDto>, ICategoryAppService
    {
        private readonly ICategoryManager categoryManager;
        public CategoryAppService(
            ICategoryRepository repository,
            ICategoryManager categoryManager) :
            base(repository)
        {
            GetPolicyName = Zero1FivePermissions.Category.Default;
            GetListPolicyName = Zero1FivePermissions.Category.Default;
            CreatePolicyName = Zero1FivePermissions.Category.Create;
            UpdatePolicyName = Zero1FivePermissions.Category.Update;
            DeletePolicyName = Zero1FivePermissions.Category.Delete;
            this.categoryManager = categoryManager;
        }

        [Authorize(Zero1FivePermissions.Category.Default)]
        public override async Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
        {
            return MapToGetOutputDto(await categoryManager.CreateAsync(input.Name, input.Description));
        }

        public override Task<CategoryDto> UpdateAsync(Guid id, CreateUpdateCategoryDto input)
        {
            return base.UpdateAsync(id, input);
        }
    }
}